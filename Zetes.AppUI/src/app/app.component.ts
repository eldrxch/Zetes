import { Component, OnInit } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import {EventqueueService, EventType, EventData} from './eventqueue.service';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,  RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',    
})
export class AppComponent implements OnInit{
  title = 'Customer Portal';
  isLoggedin: boolean = false;

  constructor(private broadcaster: EventqueueService) {}

  logout() {
    this.isLoggedin = false;
    window.localStorage.removeItem('username');
    window.location.href = '/login';    
  }

  ngOnInit() {
    this.broadcaster.onEvent(EventType.LOGIN).subscribe((data: EventData) => {
      this.isLoggedin = true;
    });
    
    if (window.localStorage.getItem('username')) {
      this.isLoggedin = true;
    }
  }
}
