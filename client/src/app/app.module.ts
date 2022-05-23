import { HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { ConfigLoader } from './services/config-loader.service';
import { EmployeeService } from './services/employee.service';
import { NavbarComponent } from './navbar/navbar.component';
import { EmployeesComponent } from './employees/employees.component';
import { AppRoutingModule } from './app-routing.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NewEmployeeFormComponent } from './employees/new-employee-form/new-employee-form.component';
import { FormsModule } from '@angular/forms';
import { ModalComponent } from './modal/modal.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';

export function initializeApp(configLoader: ConfigLoader){
  return () => configLoader.load();
}

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    EmployeesComponent,
    NewEmployeeFormComponent,
    ModalComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FontAwesomeModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [
    EmployeeService,
    ConfigLoader,
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [ConfigLoader], 
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
