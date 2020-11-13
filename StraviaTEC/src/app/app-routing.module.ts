import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'home',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'login',
    loadChildren: () => import('./login/login.module').then( m => m.LoginPageModule)
  },
  {
    path: 'reto/:nombreReto/:admin/:tipoActividad',
    loadChildren: () => import('./actividades/reto/reto.module').then( m => m.RetoPageModule)
  },
  {
    path: 'carrera/:nombreCarrera/:admin/:tipoActividad',
    loadChildren: () => import('./actividades/carrera/carrera.module').then( m => m.CarreraPageModule)
  },
  {
    path: 'inicio',
    loadChildren: () => import('./inicio/inicio.module').then( m => m.InicioPageModule)
  },
  {
    path: 'actividad-libre',
    loadChildren: () => import('./actividad-libre/actividad-libre.module').then( m => m.ActividadLibrePageModule)
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
