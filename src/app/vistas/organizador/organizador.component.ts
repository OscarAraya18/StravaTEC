import { Component, OnInit } from '@angular/core';
import { CarrerasComponent } from '../../vistas/organizador/carreras/carreras.component';
import { InscripcionesComponent } from '../../vistas/organizador/inscripciones/inscripciones.component';
import { RetosComponent } from '../../vistas/organizador/retos/retos.component';
import { GruposComponent } from '../../vistas/organizador/grupos/grupos.component';
import { ReportesComponent } from '../../vistas/organizador/reportes/reportes.component';

@Component({
  selector: 'app-organizador',
  templateUrl: './organizador.component.html',
  styleUrls: ['./organizador.component.css']
})
export class OrganizadorComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
