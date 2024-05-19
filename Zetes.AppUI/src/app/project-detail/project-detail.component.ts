import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NgFor, NgIf, UpperCasePipe } from '@angular/common';
import { Project } from '../models/project';
import { FormsModule } from '@angular/forms';
import { Customer } from '../models/customer';

@Component({
  selector: 'app-project-detail',
  standalone: true,
  imports: [FormsModule, NgFor , NgIf, UpperCasePipe],
  templateUrl: './project-detail.component.html',
  styleUrl: './project-detail.component.css'
})
export class ProjectDetailComponent {
  @Input() project?: Project;
  @Input() customers?: Customer[];
  @Output() cancel = new EventEmitter<void>();
  @Output() save = new EventEmitter<Project>();

  onCancel(): void {
    this.cancel.emit();
  }

  onSave(): void {
    if (this.project) {
      this.save.emit(this.project);
    }
  }
}
