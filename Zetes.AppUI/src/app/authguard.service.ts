import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthguardService {
  canActivate(): boolean {
    if (!window.localStorage.getItem('username') && window.location.href.indexOf('/admin') > -1) {      
      window.location.href = '/login';
      return false;
    }
    return true;
  }
}
