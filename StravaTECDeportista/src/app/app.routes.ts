import { Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InicioSesionComponent } from './componentes/inicio-sesion/inicio-sesion.component';
import { RegistroDeportistaComponent } from './componentes/registroDeportista/registro-deportista.component';
import { MiMuroComponent } from './componentes/miMuro/mi-muro.component';
import { RegistroActividadComponent } from './componentes/registroActividad/registro-actividad.component';
import { SeguirDeportistasComponent } from './componentes/seguirDeportistas/seguir-deportistas.component';
import { BuscarCarrerasRetosComponent } from './componentes/buscarCarrerasRetos/buscar-carreras-retos.component';
import { BuscarGruposComponent } from './componentes/buscarGrupos/buscar-grupos.component';
import { MisCarrerasRetosComponent } from './componentes/misCarrerasRetos/mis-carreras-retos.component';
import { ModificarCuentaComponent } from './componentes/modificarCuenta/modificar-cuenta.component';


const app_routes: Routes = [ 
    { path: 'registro-deportista', component:RegistroDeportistaComponent},
    { path: 'inicio-sesion', component:InicioSesionComponent},
    { path: 'mi-muro', component: MiMuroComponent},
    { path: 'registro-actividad', component: RegistroActividadComponent},
    { path: 'seguir-deportistas', component: SeguirDeportistasComponent},
    { path: 'buscar-carreras-retos', component: BuscarCarrerasRetosComponent},
    { path: 'buscar-grupos', component: BuscarGruposComponent},
    { path: 'mis-carreras-retos', component: MisCarrerasRetosComponent},
    { path: 'modificar-cuenta', component: ModificarCuentaComponent},
    { path: '', component: InicioSesionComponent}
    
]

export const app_routing = RouterModule.forRoot(app_routes);