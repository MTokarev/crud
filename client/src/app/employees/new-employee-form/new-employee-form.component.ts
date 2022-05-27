import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
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
  public newEmployeeForm: FormGroup;

  constructor(private employeeService: EmployeeService) { 
    this.newEmployeeForm = new FormGroup({
      'name': new FormControl('', [Validators.required, Validators.minLength(1)]),
      'age': new FormControl('', [Validators.required, Validators.min(18), Validators.max(200)])
    })
  }

  ngOnInit(): void {
  }

  onSubmit()
  {
    console.log(this.newEmployeeForm);
    this
      .employeeService
      .createEmployee(<Employee>this.newEmployeeForm.value)
      .subscribe(
        {
          next: (response: Employee) => {
            this.newEmployeeForm.reset();
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
