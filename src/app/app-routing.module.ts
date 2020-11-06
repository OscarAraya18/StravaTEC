import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { OrganizadorComponent } from './vistas/organizador/organizador.component';
import { CarrerasComponent } from './vistas/organizador/carreras/carreras.component';
import { InscripcionesComponent } from './vistas/organizador/inscripciones/inscripciones.component';
import { RetosComponent } from './vistas/organizador/retos/retos.component';
import { GruposComponent } from './vistas/organizador/grupos/grupos.component';
import { ReportesComponent } from './vistas/organizador/reportes/reportes.component';


const routes: Routes = [
 {
    path: '',
    component: OrganizadorComponent
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
