import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Geolocation } from '@ionic-native/geolocation/ngx';

import { IonicModule } from '@ionic/angular';

import { RetoPageRoutingModule } from './reto-routing.module';

import { RetoPage } from './reto.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RetoPageRoutingModule
  ],
  declarations: [RetoPage],
  providers: [Geolocation]
})

export class RetoPageModule {}
