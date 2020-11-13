import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ActividadLibrePage } from './actividad-libre.page';

const routes: Routes = [
  {
    path: '',
    component: ActividadLibrePage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ActividadLibrePageRoutingModule {}
