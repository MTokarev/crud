import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Employee } from "../models/employee.model";
import { PageParams } from "../models/page-params";
import { PaginatedResponse } from "../models/paginated-Response.model";
import { ConfigLoader } from "./config-loader.service";

@Injectable()
export class EmployeeService {
  private params = new HttpParams();

  constructor(private httpClient: HttpClient) {
    
  }

  loadEmployees(pageParams: PageParams){
    let params = new HttpParams()
      .append("pageIndex", pageParams.pageIndex)
      .append("pageSize", pageParams.pageSize);

    return this
      .httpClient
      .get<PaginatedResponse<Employee>>(ConfigLoader.config.Urls.base + ConfigLoader.config.Urls.employeeGet, {
        params: params
      })
  }
}