import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { Subject } from "rxjs";
import { Employee } from "../models/employee.model";
import { PageParams } from "../models/page-params";
import { PaginatedResponse } from "../models/paginated-Response.model";
import { ConfigLoader } from "./config-loader.service";

@Injectable()
export class EmployeeService {
  public employeesChanged = new Subject<PaginatedResponse<Employee>>();
  public employees: PaginatedResponse<Employee> = {
    data: [],
    itemsReturned: 0,
    pageIndex: 0,
    pageSize: 0,
    pageSizeMaxAllowed: 0,
    totalItems: 0,
    totalPages: 0
  };

  constructor(private httpClient: HttpClient, private toastr: ToastrService) {
    
  }

  loadEmployees(pageParams: PageParams){
    let params = new HttpParams()
      .append("pageIndex", pageParams.pageIndex)
      .append("pageSize", pageParams.pageSize);

    this
      .httpClient
      .get<PaginatedResponse<Employee>>(ConfigLoader.config.Urls.base + ConfigLoader.config.Urls.employeeGet, {
        params: params
      }).subscribe(
        {
          next: (response: PaginatedResponse<Employee>) => {
            this.employees = response;
            this.employeesChanged.next(response);
          },
          error: (error) => this.toastr.error(error, 'Unable to get employee list')
        }
      )
  }

  deleteEmployee(id: number)
  {
    const deleteUrl = `${ConfigLoader.config.Urls.base}${ConfigLoader.config.Urls.employeeDelete}/${id}`;
    
    // Delete from API
    this
      .httpClient
      .delete(deleteUrl)
      .subscribe(
        {
          next: () => this.toastr.success('Employee was removed from database.', 'Delete succeeded'),
          error: (error) => this.toastr.error(JSON.stringify(error.error.errors), 'Unable to get employee list')
        }
      );
    
    // Update data array
    this.employees.data = this.employees.data.filter((x) => x.id != id);
    
    // Push change to subscribers
    this.employeesChanged.next(this.employees);
  }

  // This method return subscription unlike other methods in this service
  // The reason is to show how to handle model and modal notifications
  // Can be changed to toastr
  createEmployee(employee: Employee)
  {
    return this
      .httpClient
      .post<Employee>(ConfigLoader.config.Urls.base + ConfigLoader.config.Urls.employeeCreate, employee);
  }

  updateEmployee(employee: Employee)
  {
    this.httpClient.put(ConfigLoader.config.Urls.base + ConfigLoader.config.Urls.employeeUpdate, employee).subscribe(
      {
        next: () => this.toastr.success('Employee was updated in database.', 'Update succeeded'),
        error: (error) => this.toastr.error(JSON.stringify(error.error.errors), 'Unable to update employee')
      }
    );
  }
}