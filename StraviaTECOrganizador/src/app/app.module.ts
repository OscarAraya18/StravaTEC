import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import {MatTabsModule} from '@angular/material/tabs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';


import { PdfViewerModule } from 'ng2-pdf-viewer';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OrganizadorComponent } from './vistas/organizador/organizador.component';
import { CarrerasComponent } from './vistas/organizador/carreras/carreras.component';
import { InscripcionesComponent } from './vistas/organizador/inscripciones/inscripciones.component';
import { RetosComponent } from './vistas/organizador/retos/retos.component';
import { GruposComponent } from './vistas/organizador/grupos/grupos.component';
import { ReportesComponent } from './vistas/organizador/reportes/reportes.component';
import { LogInComponent } from './vistas/organizador/log-in/log-in.component';

@NgModule({
  declarations: [
    AppComponent,
    OrganizadorComponent,
    CarrerasComponent,
    InscripcionesComponent,
    RetosComponent,
    GruposComponent,
    ReportesComponent,
    LogInComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatTabsModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    PdfViewerModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
