import { Component, OnInit, TemplateRef } from '@angular/core';
import { AuthService } from '../auth.service';
import { NgFor, NgIf } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { catchError } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { Account } from '../models/account';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, NgFor, NgIf, RouterLink, RouterLinkActive],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  account: Account = {email: '', password: '', confirmPassword: ''};
  constructor(private authService: AuthService,private router: Router) { }

  onRegister(account: Account) {
    console.log(account); 
    this.authService.register(account.email, account.password).pipe(
      catchError((error) => {
        console.log(error);
        let errorMessage = "Account creation failed.";
        if ("error" in error && "errors" in error.error) 
        {
            for(const key in error.error.errors) {
                errorMessage += "\n" + error.error.errors[key];
            }
        }
        alert(errorMessage);
        return [];
      }
    )).subscribe(_ => {
        alert("Account created successfully.");
        this.router.navigate(['/login']);
    });
  }
}
