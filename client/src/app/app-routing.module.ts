import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { EmployeesComponent } from './employees/employees.component';
import { NewEmployeeFormComponent } from './employees/new-employee-form/new-employee-form.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  // TODO: Better organize routes using child
  { path: 'employees', children: [
    { path: '', component: EmployeesComponent, pathMatch: 'full' },
    { path: 'create', component: NewEmployeeFormComponent},
    { path: ':pageIndex', component: EmployeesComponent}
  ]
  },
  { path: '', component: HomeComponent, pathMatch: 'full' }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes),
    CommonModule
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
