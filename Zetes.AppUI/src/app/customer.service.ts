import { Injectable } from '@angular/core';
import { Customer } from './models/customer';
import { Observable } from 'rxjs';  
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  customerUrl = environment.apiUrl + '/customers';
  
  constructor(private http:HttpClient) { }

  getCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.customerUrl);
  }

  getCustomer(id: number): Observable<Customer> {
    return this.http.get<Customer>(`${this.customerUrl}/${id}`);
  }

  addCustomer(customer: Customer): Observable<Customer> {
    return this.http.post<Customer>(this.customerUrl, customer);
  }

  updateCustomer(customer: Customer): Observable<any> {
    return this.http.put(`${this.customerUrl}/${customer.customerId}`, customer);
  }

  deleteCustomer(customer: Customer): Observable<any> {
    return this.http.delete(`${this.customerUrl}/${customer.customerId}`);
  }
}
