import { HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { ConfigLoader } from './services/config-loader.service';
import { EmployeeService } from './services/employee.service';
import { NavbarComponent } from './navbar/navbar.component';
import { EmployeesComponent } from './employees/employees.component';
import { AppRoutingModule } from './app-routing.module';

export function initializeApp(configLoader: ConfigLoader){
  return () => configLoader.load();
}

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    EmployeesComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule
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
