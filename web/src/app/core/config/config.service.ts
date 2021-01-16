import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

export interface Config {
  apiUrl: string;
}

@Injectable()
export class ConfigProvider {
  value: Config;

  constructor(private http: HttpClient) { }

  init(): Promise<void> {
    return this.http.get<Config>('assets/config.json')
      .toPromise()
      .then(res => {
        this.value = res;
      });
  }
}
