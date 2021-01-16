import { TestBed } from '@angular/core/testing';
import { SalesService } from './sales.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FindSalespersonCriteria } from './sales.model';
import { HttpClient } from '@angular/common/http';

describe('SalesService', () => {
  let service: SalesService;
  let http: HttpClient;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      providers: [
        SalesService
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    service = TestBed.inject(SalesService);
    http = TestBed.inject(HttpClient);
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
