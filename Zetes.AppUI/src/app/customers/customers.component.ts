import { Component,  OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Customer } from '../models/customer';
import { CustomerService } from '../customer.service';
import { NgFor, NgIf } from '@angular/common';
import { CustomerDetailComponent } from '../customer-detail/customer-detail.component';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {Config} from 'datatables.net';
import {DataTableDirective, DataTablesModule} from 'angular-datatables'


@Component({
  selector: 'app-customers',
  standalone: true,
  imports: [NgFor, NgIf, CustomerDetailComponent, DataTablesModule],
  templateUrl: './customers.component.html',
  styleUrl: './customers.component.css'
})
export class CustomersComponent implements OnInit {
  @ViewChild('viewUserTemplate', {static:true}) viewUserTemplate?: TemplateRef<any>;
  customers: Customer[] = [];
  selectedCustomer?: Customer;
  modalRef?: BsModalRef;
  
  
  @ViewChild(DataTableDirective, {static: false})
  dtElement:any= DataTableDirective;

  dtOptions: Config = {};

  constructor(private customerService:CustomerService, private modalService:BsModalService) { }

  ngOnInit(): void {
    this.getCustomers();
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      columnDefs: [        
        {
          targets: 4,
          orderable: false,
          searchable: false,
        },
        {
          targets: [0,1,2,3],
          orderable: true,
          searchable: true,
          type: 'string'
        }
      ]
    };
  }

  onSelect(customer: Customer): void {
    this.selectedCustomer = customer;
  }

  getCustomers(): void {
    this.customerService.getCustomers().subscribe(customers => 
    {
        this.customers = customers;
        this.dtElement.dtInstance.then((dtInstance: any) => {
          dtInstance.clear();
          for (let customer of this.customers) {
              let options = `<div id="optionsContainer${customer.customerId}"></div>`;
              let row = [customer.firstName, customer.lastName, customer.phone, customer.email, options];
              dtInstance.row.add(row).draw();
              this.bindTableRowOptions(customer);             
            }
        });
    });
  }


  bindTableRowOptions(customer:Customer): void {        
    let cont = document.getElementById('optionsContainer'+customer.customerId) as HTMLElement;
    let ref = this.viewUserTemplate as TemplateRef<any>;
    let editButton = document.createElement('button');
    let deleteButton = document.createElement('button');

    editButton.className = 'btn btn-sm btn-secondary';
    editButton.innerHTML = '<i class="bi bi-pencil-square"></i>';
    editButton.addEventListener('click',() => this.showDetail(ref,customer));

    deleteButton.className = 'btn btn-sm btn-danger ms-1';
    deleteButton.innerHTML = '<i class="bi bi-trash"></i>';
    deleteButton.addEventListener('click',() => this.deleteCustomer(customer));

    cont.appendChild(editButton);
    cont.appendChild(deleteButton);
  }

  deleteCustomer(customer: Customer): void {
    if(confirm('Are you sure you want to delete this customer?')){
      this.customerService.deleteCustomer(customer).subscribe(
        () => this.getCustomers()        
      );      
    }    
  } 

  showDetail(viewUserTemplate:TemplateRef<any>,customer?: Customer): void {
    let newCustomer = {customerId: 0,  firstName: '', lastName: '', email: '', phone: ''};
    this.selectedCustomer = customer ? customer : newCustomer;
    this.modalRef = this.modalService.show(viewUserTemplate);
  }

  detailOnCancel(): void {    
    this.modalRef?.hide();
  }

  detailOnSave(customer: Customer): void {    
    this.modalRef?.hide();
    if (customer.customerId === 0) {
      this.customerService.addCustomer(customer).subscribe(() => this.getCustomers());
    } else {
      this.customerService.updateCustomer(customer).subscribe(() => this.getCustomers());    
    }    
  }
}
