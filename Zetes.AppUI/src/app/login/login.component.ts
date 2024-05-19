import { Component, OnInit, Output, TemplateRef } from '@angular/core';
import { Login } from '../models/login';
import { AuthService } from '../auth.service';
import { EventqueueService, EventType, EventData } from '../eventqueue.service'; 
import { NgFor, NgIf } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { catchError } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, NgFor, NgIf, RouterLink, RouterLinkActive],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {  
  login: Login = {username: '', password: ''};
  constructor(private authService: AuthService,private router: Router,private broadcaster:EventqueueService) { }

  onLogin() {
    this.authService.login(this.login.username, this.login.password).pipe(
      catchError((error) => {
        console.log(error);
        alert("Invalid username or password.");
        return [];
      }
    )).subscribe(_ => {
      this.broadcaster.emitEvent({type: EventType.LOGIN, data: this.login.username});
      window.localStorage.setItem('username', this.login.username);
      this.router.navigate(['/admin']);
  });
  }
}
