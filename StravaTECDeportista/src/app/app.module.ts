import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { app_routing } from './app.routes';

import { AgmCoreModule } from '@agm/core';

   
import { AppComponent } from './app.component';
import { RegistroDeportistaComponent } from './componentes/registroDeportista/registro-deportista.component';
import { InicioSesionComponent } from './componentes/inicio-sesion/inicio-sesion.component';
import { MiMuroComponent } from './componentes/miMuro/mi-muro.component';
import { RegistroActividadComponent } from './componentes/registroActividad/registro-actividad.component';
import { SeguirDeportistasComponent } from './componentes/seguirDeportistas/seguir-deportistas.component';
import { BuscarCarrerasRetosComponent } from './componentes/buscarCarrerasRetos/buscar-carreras-retos.component';
import { BuscarGruposComponent } from './componentes/buscarGrupos/buscar-grupos.component';
import { MisCarrerasRetosComponent } from './componentes/misCarrerasRetos/mis-carreras-retos.component';
import { ModificarCuentaComponent } from './componentes/modificarCuenta/modificar-cuenta.component';


import { HttpClientModule } from '@angular/common/http';
import { NgxChildProcessModule } from 'ngx-childprocess';

  
@NgModule({
  declarations: [
    AppComponent,
    RegistroDeportistaComponent,
    InicioSesionComponent,
    MiMuroComponent,
    RegistroActividadComponent,
    SeguirDeportistasComponent,
    BuscarCarrerasRetosComponent,
    BuscarGruposComponent,
    MisCarrerasRetosComponent,
    ModificarCuentaComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    app_routing,
    AgmCoreModule.forRoot({apiKey: 'AIzaSyBqgTdzyVbf_20Jjsmno8lyGwHMgFMr-Bs'})
  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { 
  
}
