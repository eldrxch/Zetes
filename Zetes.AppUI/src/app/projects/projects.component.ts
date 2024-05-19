import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Project } from '../models/project';
import { ProjectsService } from '../projects.service';
import { CustomerService } from '../customer.service';
import { NgFor, NgIf } from '@angular/common';
import { ProjectDetailComponent } from '../project-detail/project-detail.component';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import { Customer } from '../models/customer';
import {Config} from 'datatables.net';
import {DataTableDirective, DataTablesModule} from 'angular-datatables'

@Component({
  selector: 'app-projects',
  standalone: true,
  imports: [NgFor, NgIf, ProjectDetailComponent,DataTablesModule],
  templateUrl: './projects.component.html',
  styleUrl: './projects.component.css'
})
export class ProjectsComponent implements OnInit {
  @ViewChild('viewUserTemplate', {static:true}) viewUserTemplate?: TemplateRef<any>;
  projects: Project[] = [];
  customers: Customer[] = [];
  selectedProject?: Project;
  modalRef?: BsModalRef;

  @ViewChild(DataTableDirective, {static: false})
  dtElement:any= DataTableDirective;

  dtOptions: Config = {};

  constructor(
    private projectService:ProjectsService, 
    private customerService:CustomerService,
    private modalService:BsModalService) { }

  ngOnInit(): void {
    this.getProjects();
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
          targets: [0,1,2],
          orderable: true,
          searchable: true,
          type: 'string'
        },
        {
          targets: [3,4],
          orderable: true,
          searchable: true,
          type: 'string'
        },
        {
          targets: 5,
          orderable: false,
          searchable: false,
        },        
      ]
    };    
  }

  onSelect(project: Project): void {
    this.selectedProject = project;
  }

  getProjects(): void {
    this.projectService.getProjects().subscribe(projects => 
    {
        this.projects = projects;        
        this.dtElement.dtInstance.then((dtInstance: any) => {
          dtInstance.clear();
          for (let project of this.projects) {
              let options = `<div id="optionsProjContainer${project.projectId}"></div>`;
              let fullname = project.customer?.firstName + ' ' + project.customer?.lastName;
              let strDate = new Date(project.startDate);
              let endDate = new Date(project.endDate);
              let strDateStr = `${strDate.getFullYear()}-${strDate.getMonth()}-${strDate.getDate()}`
              let endDateStr = `${endDate.getFullYear()}-${endDate.getMonth()}-${endDate.getDate()}`
              let row = [project.name, fullname, project.description, strDateStr, endDateStr, options];
              dtInstance.row.add(row).draw();
              this.bindTableRowOptions(project);
            }
        });        
    });
  }

  bindTableRowOptions(project:Project): void {        
    let cont = document.getElementById('optionsProjContainer'+project.projectId) as HTMLElement;
    let ref = this.viewUserTemplate as TemplateRef<any>;
    let editButton = document.createElement('button');
    let deleteButton = document.createElement('button');

    editButton.className = 'btn btn-sm btn-secondary';
    editButton.innerHTML = '<i class="bi bi-pencil-square"></i>';
    editButton.addEventListener('click',() => this.showDetail(ref,project));

    deleteButton.className = 'btn btn-sm btn-danger ms-1';
    deleteButton.innerHTML = '<i class="bi bi-trash"></i>';
    deleteButton.addEventListener('click',() => this.deleteProject(project));

    cont.appendChild(editButton);
    cont.appendChild(deleteButton);
  }


  getCustomers(): void {
    this.customerService.getCustomers().subscribe(customers => this.customers = customers);
  }

  deleteProject(project: Project): void {
    if(confirm('Are you sure you want to delete this project?')){
      this.projectService.deleteProject(project).subscribe(
        () => this.getProjects()
      );    
    }    
  } 

  showDetail(viewUserTemplate:TemplateRef<any>,project?: Project): void {
    this.getCustomers();

    let newProject = {projectId: 0, customerId:0,   name: '', description: '', startDate: new Date(), endDate: new Date()};
    this.selectedProject = project ? project : newProject;
    this.modalRef = this.modalService.show(viewUserTemplate);
  }

  detailOnCancel(): void {    
    this.modalRef?.hide();
  }

  detailOnSave(project: Project): void {    
    this.modalRef?.hide();
    if (project.projectId === 0) {
      this.projectService.addProject(project).subscribe(() => this.getProjects());
    } else {
      this.projectService.updateProject(project).subscribe(() => this.getProjects());    
    }    
  }
}
