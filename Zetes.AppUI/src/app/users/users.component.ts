import { Component, OnInit, ViewChild} from '@angular/core';
import { User } from '../models/user';
import { AuthService } from '../auth.service';  
import { NgFor, NgIf } from '@angular/common';
import {Config} from 'datatables.net';
import {DataTableDirective, DataTablesModule} from 'angular-datatables'

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [NgFor, NgIf, DataTablesModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent implements OnInit  {
  users: User[] = [];
  selectedUser?: User;

  @ViewChild(DataTableDirective, {static: false})
  dtElement:any= DataTableDirective;

  dtOptions: Config = {};

  constructor(private authService:AuthService) { }

  ngOnInit(): void {
    this.getUsers();
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      columnDefs: [        
        {
          targets: 2,
          orderable: false,
          searchable: false,
        },
        {
          targets: [0,1],
          orderable: true,
          searchable: true,
          type: 'string'
        }
      ]
    };        
  }

  getUsers(): void {
    this.authService.getUsers().subscribe(users => 
    {
        this.users = users;
        this.dtElement.dtInstance.then((dtInstance: any) => {
          dtInstance.clear();
          for (let user of this.users) {
              let options = `<div id="optionsUserContainer${user.userId}"></div>`;
              let row = [user.email, user.username, options];
              dtInstance.row.add(row).draw();
              this.bindTableRowOptions(user);
            }
        });
    });
  }

  bindTableRowOptions(user:User): void {        
    let cont = document.getElementById('optionsUserContainer'+user.userId) as HTMLElement;        
    let deleteButton = document.createElement('button');

    deleteButton.className = 'btn btn-sm btn-danger ms-1';
    deleteButton.innerHTML = '<i class="bi bi-trash"></i>';
    deleteButton.addEventListener('click',() => this.deleteUser(user));

    cont.appendChild(deleteButton);
  }

  deleteUser(user: User): void {
    if(confirm('Are you sure you want to delete this user?')){
      this.authService.deleteUser(user).subscribe(
        () => this.getUsers()
      );      
    }    
  } 
}
