import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Geolocation } from '@ionic-native/geolocation/ngx';
import { IonicModule } from '@ionic/angular';

import { ActividadLibrePageRoutingModule } from './actividad-libre-routing.module';

import { ActividadLibrePage } from './actividad-libre.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ActividadLibrePageRoutingModule
  ],
  declarations: [ActividadLibrePage],
  providers: [Geolocation]
})
export class ActividadLibrePageModule {}
