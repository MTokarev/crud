import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
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

export class EmployeesComponent implements OnInit {
  public employees?: PaginatedResponse<Employee> = {
    data: [],
    itemsReturned: 0,
    pageIndex: 0,
    pageSize: 0,
    pageSizeMaxAllowed: 0,
    totalItems: 0,
    totalPages: 0
  };
  private pageParams: PageParams = new PageParams(ConfigLoader.config.Pagination.defaultPageSize, 1);

  constructor(private employeeService: EmployeeService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    
    this.route.params.subscribe(
      (params: Params) => {
        // If exist use from the route, otherwise default
        this.pageParams.pageIndex = params['pageIndex'] ? +params['pageIndex'] : this.pageParams.pageIndex;
      }
    )

    this.employeeService.loadEmployees(this.pageParams).subscribe(
      {
        next: (response: PaginatedResponse<Employee>) => this.employees = response
      }
    );
  }
}
