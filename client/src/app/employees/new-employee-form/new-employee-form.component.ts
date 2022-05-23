import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormGroup, NgForm } from '@angular/forms';
import { Employee } from 'src/app/models/employee.model';
import { EmployeeService } from 'src/app/services/employee.service';

@Component({
  selector: 'app-new-employee-form',
  templateUrl: './new-employee-form.component.html',
  styleUrls: ['./new-employee-form.component.css']
})
export class NewEmployeeFormComponent implements OnInit {
  public modalTitle: string = '';
  public modalMessage: string = '';
  public hasError: boolean = false;
  public displayStyle = "none";
  constructor(private employeeService: EmployeeService) { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm)
  {
    this
      .employeeService
      .createEmployee(<Employee>form.value)
      .subscribe(
        {
          next: (response: Employee) => {
            form.resetForm();
            this.hasError = false;
            this.modalTitle = "Employee has been created";
            this.modalMessage = `Id: '${response.id}', name: '${response.name}', age: '${response.age}'`;
            this.displayStyle = "block";
          },
          error: (error) => {
            this.hasError = true;
            this.modalTitle = "Error occurred on employee creation"
            this.modalMessage = error.error.title;
            this.displayStyle = "block";
          }
        }
      );
  }

  onModalClosed()
  {
    // Handle event from child modal component where user might close the window
    this.displayStyle = "none";
  }
}
