import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { RetoPage } from './reto.page';

const routes: Routes = [
  {
    path: '',
    component: RetoPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class RetoPageRoutingModule {}
