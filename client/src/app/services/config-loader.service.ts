import { Injectable } from "@angular/core";
import { AppConfig } from "../models/app-config.model";
import { HttpClient } from "@angular/common/http"
import { environment } from "src/environments/environment";

@Injectable()
export class ConfigLoader {
  static config: AppConfig;

  constructor(private httpClient: HttpClient){

  }

  load() {
    const configFile = `assets/config/config.${environment.name}.json`;
    return new Promise<void>((resolve, reject) => {
      this.httpClient.get(configFile).subscribe(
        {
          next: (response) => {
            ConfigLoader.config = <AppConfig>response;
            resolve();
          },
          error: (error: any) => { 
            reject(`Could not load file '${configFile}': ${JSON.stringify(error)}`);
          }
        }
      )
    });
  }
}