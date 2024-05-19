import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NgIf, UpperCasePipe } from '@angular/common';
import { Customer } from '../models/customer';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-customer-detail',
  standalone: true,
  imports: [FormsModule, NgIf, UpperCasePipe],
  templateUrl: './customer-detail.component.html',
  styleUrl: './customer-detail.component.css'
})
export class CustomerDetailComponent {
  @Input() customer?: Customer;
  @Output() cancel = new EventEmitter<void>();
  @Output() save = new EventEmitter<Customer>();

  onCancel(): void {
    this.cancel.emit();
  }

  onSave(): void {
    if (this.customer) {
      this.save.emit(this.customer);
    }
  }
}
