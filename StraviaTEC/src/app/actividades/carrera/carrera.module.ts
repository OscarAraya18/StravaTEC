import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Geolocation } from '@ionic-native/geolocation/ngx';

import { IonicModule } from '@ionic/angular';

import { CarreraPageRoutingModule } from './carrera-routing.module';

import { CarreraPage } from './carrera.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    CarreraPageRoutingModule
  ],
  declarations: [CarreraPage],
  providers: [Geolocation]
})
export class CarreraPageModule {}
