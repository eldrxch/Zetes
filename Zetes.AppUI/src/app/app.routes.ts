import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin/admin.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthguardService } from './authguard.service';

export const routes: Routes = [
    { path: 'admin', component:AdminComponent, canActivate: [AuthguardService]},
    { path:'login', component: LoginComponent},
    { path:'register', component: RegisterComponent},
    { path: '', redirectTo: '/login', pathMatch: 'full' }
];
