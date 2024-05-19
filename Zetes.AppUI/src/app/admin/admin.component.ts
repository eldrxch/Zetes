import { Component } from '@angular/core';
import { CustomersComponent } from '../customers/customers.component';
import { ProjectsComponent } from '../projects/projects.component';
import { UsersComponent } from '../users/users.component';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CustomersComponent, ProjectsComponent, UsersComponent],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent {

}
