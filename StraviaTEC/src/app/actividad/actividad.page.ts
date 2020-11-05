import { Component, OnInit} from '@angular/core';
import {ViewChild, ElementRef } from '@angular/core';
declare var google: any;

@Component({
  selector: 'app-actividad',
  templateUrl: './actividad.page.html',
  styleUrls: ['./actividad.page.scss'],
})
export class ActividadPage implements OnInit {
  map: any;
  @ViewChild('map', {read:ElementRef, static: false}) mapRef:ElementRef;
  constructor() { }

  ngOnInit() {
  }

  //Este método se ejecuta cada vez que se abra esta página
  ionViewDidEnter(){
    this.showMap();
  }

  //Este método despliega el mapa en pantalla
  showMap(){
    const location = new google.maps.LatLng(10.262000, -85.584385);
    const options ={
      center: location,
      zoom: 15,
      disableDefaultUI: true
    }
    this.map = new google.maps.Map(this.mapRef.nativeElement, options);

    this.map.addListener("dblclick", (e) => {
      this.placeMarker(e.latLng, this.map);
    });

  }
  
  /**
   * Este método coloca marcadores en el mapa
   * Se llama cuando el usuario hace doble click en el componente
   * @param latLng Latitud y longitud (posición)
   * @param map El mapa en donde se desea colocar el marcador
   */
  placeMarker(latLng: google.maps.LatLng, map: google.maps.Map) {
    new google.maps.Marker({
      position: latLng,
      map: map,
    });
    map.panTo(latLng);
  }

}
