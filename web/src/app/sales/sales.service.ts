import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GroupItem, Result, FindSalespersonCriteria } from './sales.model';

@Injectable()
export class SalesService {

  constructor(private http: HttpClient) { }

  getSpecialities(): Observable<GroupItem[]> {
    return this.http.get<GroupItem[]>('http://localhost:5000/api/group?type=2');
  }

  findSalesperson(criteria: FindSalespersonCriteria): Observable<Result<string>> {
    return this.http.post<Result<string>>('http://localhost:5000/api/person/find', criteria);
  }
}
