import { NgModule } from '@angular/core';
import { SharedModule } from '../shared';
import { SalesRoutingModule } from './sales-routing.module';
import { SalesService } from './sales.service';
import { SalespersonComponent } from './salesperson';

@NgModule({
  declarations: [
    SalespersonComponent
  ],
  imports: [
    SharedModule,
    SalesRoutingModule
  ],
  providers: [
    SalesService
  ]
})
export class SalesModule { }
