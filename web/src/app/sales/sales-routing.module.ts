import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SalespersonComponent } from './salesperson';

const routes: Routes = [
  {
    path: '',
    component: SalespersonComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SalesRoutingModule { }
