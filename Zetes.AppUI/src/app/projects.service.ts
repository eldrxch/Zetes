import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Project } from './models/project';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {
  projectsUrl=environment.apiUrl+'/projects';  
  constructor(private http:HttpClient) { }

  getProjects():Observable<Project[]>{
    return this.http.get<Project[]>(this.projectsUrl);
  }

  getProject(id:number){
    return this.http.get(`${this.projectsUrl}/${id}`);
  }

  addProject(project:Project){
    return this.http.post(this.projectsUrl,project);
  }

  updateProject(project:Project){
    return this.http.put(`${this.projectsUrl}/${project.projectId}`,project);
  }

  deleteProject(project:Project){
    return this.http.delete(`${this.projectsUrl}/${project.projectId}`);
  }
}
