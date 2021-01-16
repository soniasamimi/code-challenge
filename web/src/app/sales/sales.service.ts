import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GroupItem, Result, FindSalespersonCriteria } from './sales.model';
import { ConfigProvider } from '../core';

@Injectable()
export class SalesService {

  constructor(
    private http: HttpClient,
    private config: ConfigProvider) { }

  getSpecialities(): Observable<GroupItem[]> {
    return this.http.get<GroupItem[]>(`${this.config.value.apiUrl}/api/group?type=2`);
  }

  findSalesperson(criteria: FindSalespersonCriteria): Observable<Result<string>> {
    return this.http.post<Result<string>>(`${this.config.value.apiUrl}/api/person/find`, criteria);
  }
}
