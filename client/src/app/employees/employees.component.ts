import { Component, ElementRef, NgModule, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgModel } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs';
import { Employee } from '../models/employee.model';
import { PageParams } from '../models/page-params';
import { PaginatedResponse } from '../models/paginated-Response.model';
import { ConfigLoader } from '../services/config-loader.service';
import { EmployeeService } from '../services/employee.service';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})

export class EmployeesComponent implements OnInit, OnDestroy {
  public employees = this.employeeService.employees;

  private employeesSubscription = new Subscription();
  private pageParams: PageParams = new PageParams(ConfigLoader.config.Pagination.defaultPageSize, 1);

  constructor(private employeeService: EmployeeService, private route: ActivatedRoute) { }
  
  ngOnInit(): void {
    this
      .route
      .params
      .subscribe(
        (params: Params) => {
          // If exist use from the route, otherwise default
          this.pageParams.pageIndex = params['pageIndex'] ? +params['pageIndex'] : this.pageParams.pageIndex;
          
          this.employeeService.loadEmployees(this.pageParams)
        }
      )
      
    // Subscribe for change events in employees data
    this.employeesSubscription = this
      .employeeService
      .employeesChanged
      .subscribe(
        (data: PaginatedResponse<Employee>) => {
          this.employees = data;    
        }
      )
  }
      
  ngOnDestroy(): void {
    this.employeesSubscription.unsubscribe();
  }

  deleteEmployee(id: number)
  {
    this.employeeService.deleteEmployee(id);
  }

  onFocusOut(employee: Employee)
  {
    // Find employee in current list
    const currentEmployeeIndex = this.employees.data.findIndex(
      (e) => e.id === employee.id
    );
    
    // Check if properties have changed
    if( this.employees.data[currentEmployeeIndex].name != employee.name || 
        this.employees.data[currentEmployeeIndex].age != employee.age)
    {
      this.employeeService.updateEmployee(employee);
      
      // Update current list
      this.employees.data[currentEmployeeIndex] = employee;
    }
  }
}
