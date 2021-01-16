import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { HttpClient } from '@angular/common/http';
import { SalesService } from './sales.service';
import { FindSalespersonCriteria } from './sales.model';
import { Config, ConfigProvider } from '../core';

describe('SalesService', () => {
  let service: SalesService;
  let http: HttpClient;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      providers: [
        SalesService,
        ConfigProvider
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    service = TestBed.inject(SalesService);
    http = TestBed.inject(HttpClient);
    const config = TestBed.inject(ConfigProvider);
    config.value = { apiUrl: 'http://localhost:5000' } as Config;
  });

  it('should call getSpecialities end point', () => {
    spyOn(http, 'get');

    service.getSpecialities();

    expect(http.get).toHaveBeenCalledWith('http://localhost:5000/api/group?type=2');
  });

  it('should call findSalesperson end point', () => {
    spyOn(http, 'post');

    const criteria = { language: 'A', speciality: 'C' } as FindSalespersonCriteria;
    service.findSalesperson(criteria);

    expect(http.post).toHaveBeenCalledWith('http://localhost:5000/api/person/find', criteria);
  });
});
