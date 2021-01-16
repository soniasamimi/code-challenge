import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { of } from 'rxjs';
import { ConfigProvider } from 'src/app/core';
import { GroupItem, Result } from '../sales.model';
import { SalesService } from '../sales.service';
import { SalespersonComponent } from './salesperson.component';

describe('SalespersonComponent', () => {
  let component: SalespersonComponent;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        ReactiveFormsModule,
        ToastrModule.forRoot()
      ],
      declarations: [
        SalespersonComponent
      ],
      providers: [
        SalesService,
        ConfigProvider
      ],
    }).compileComponents();
  });

  beforeEach(() => {
    const fixture = TestBed.createComponent(SalespersonComponent);
    component = fixture.componentInstance;
  });

  it('Should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('Should load the Specialities and create form on ngOnInit', () => {
    spyOn(component, 'createForm');

    const specialities: GroupItem[] = [{ groupId: 'A', title: 'truck' }];
    const service = TestBed.inject(SalesService);
    spyOn(service, 'getSpecialities').and.returnValue(of(specialities));

    component.ngOnInit();

    expect(component.createForm).toHaveBeenCalled();
    expect(service.getSpecialities).toHaveBeenCalledWith();
    expect(component.specialities).toEqual(specialities);
  });

  it('Should create form', () => {
    component.form = undefined;

    component.createForm();

    expect(component.form).toBeDefined();
    expect(component.form.value.language).toBeNull();
    expect(component.form.value.speciality).toBeNull();
  });

  it('Should successfully find a possible match for salesperson', () => {
    component.createForm();
    component.form.controls.language.setValue('A');
    component.form.controls.speciality.setValue('D');

    const service = TestBed.inject(SalesService);
    spyOn(service, 'findSalesperson').and.returnValue(of({ success: true, value: 'David' } as Result<string>));

    component.find();

    expect(service.findSalesperson).toHaveBeenCalledWith(component.form.value);
    expect(component.person).toBe('David');
  });

  it('Should display error messages when find fails', () => {
    component.createForm();
    const service = TestBed.inject(SalesService);
    spyOn(service, 'findSalesperson').and.returnValue(of({ success: false, errors: ['error happened'] } as Result<string>));

    component.find();

    expect(service.findSalesperson).toHaveBeenCalled();
    expect(component.person).toBeUndefined();
  });
});
