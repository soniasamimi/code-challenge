import { Component, HostBinding, OnInit, ViewEncapsulation } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { finalize } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { GroupItem, Language } from '../sales.model';
import { SalesService } from '../sales.service';

@Component({
  selector: 'app-salesperson',
  templateUrl: './salesperson.component.html',
  styleUrls: ['./salesperson.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class SalespersonComponent implements OnInit {
  @HostBinding('class') class = 'app-salesperson';
  specialities: GroupItem[] = [];
  form: FormGroup;
  processing = false;
  person: string;
  language = Language;

  constructor(
    private service: SalesService,
    private fb: FormBuilder,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.service.getSpecialities()
      .subscribe(res => {
        this.specialities = res;
      });
    this.createForm();
  }

  createForm(): void {
    this.form = this.fb.group({
      language: [null, Validators.required],
      speciality: [null]
    });
  }

  find(): void {
    this.processing = true;
    this.service.findSalesperson(this.form.value)
      .pipe(finalize(() => this.processing = false))
      .subscribe(res => {
        if (res.success) {
          this.person = res.value;
        } else {
          this.toastr.error(res.errors[0], 'Error');
        }
      });
  }
}
