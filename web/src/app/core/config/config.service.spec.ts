import { TestBed } from '@angular/core/testing';
import { HttpClient } from '@angular/common/http';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { of } from 'rxjs';
import { Config, ConfigProvider } from './config.service';

describe('ConfigProvider', () => {
  let service: ConfigProvider;
  let http: HttpClient;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      providers: [
        ConfigProvider
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    service = TestBed.inject(ConfigProvider);
    http = TestBed.inject(HttpClient);
  });

  it('should load config.json', (done) => {
    const settings = { apiUrl: 'http://localhost:5000' } as Config;
    spyOn(http, 'get').and.returnValue(of(settings));

    service.init().then(() => {
      expect(http.get).toHaveBeenCalledWith('assets/config.json');
      expect(service.value).toEqual(settings);
      done();
    });
  });
});
