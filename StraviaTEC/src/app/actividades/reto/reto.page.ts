import { Component, OnInit } from '@angular/core';
import {ViewChild, ElementRef } from '@angular/core';
import { Geolocation } from '@ionic-native/geolocation/ngx';


declare var google: any;


@Component({
  selector: 'app-reto',
  templateUrl: './reto.page.html',
  styleUrls: ['./reto.page.scss'],
})
export class RetoPage implements OnInit {

  //Mapa que se está usando en la página
  map: any;

  //Geolocalización
  latitude: any;
  longitude: any;

  //Se le indica el elemento html correspondiente
  @ViewChild('map', {read:ElementRef, static: false}) mapRef:ElementRef;

  //las dos ventanas de información
  infoWindows: any = [];
  
  //Lista de marcadores
  mapMarkers:any = [];

  //titulos de los dos marcadores
  markerTitle = ["INICIO", "FIN"];

  //letras de los marcadores
  markerLabel = ["A", "B"];

  //Contador para controlar que solo hayan 2 marcadores
  //Salida y Entrada
  markerCount = 0;

  //Servicios para direcciones
  directionsService = new google.maps.DirectionsService();
  directionsDisplay = new google.maps.DirectionsRenderer();
  distanceMatrixService = new google.maps.DistanceMatrixService();

  //Variable que va a almacenar la distancia entre los puntos
  distance:any = "0 km";

  //waypoints de la ruta dibujada
  waypoints:any  = [];

  constructor(private geolocation: Geolocation) { }

  ngOnInit() {
  }

    //Este método se ejecuta cada vez que se abra esta página
    ionViewDidEnter(){
      this.showMap();
    }
  
    //Este método despliega el mapa en pantalla
    showMap(){

      this.geolocation.getCurrentPosition().then((resp) => {
        this.latitude = resp.coords.latitude;
        this.longitude = resp.coords.longitude;
      
       const centerPos = {
        lat: this.latitude,
        lng: this.longitude
      };
     //const location = new google.maps.LatLng(this.latitude, this.longitude);
     const options ={
       center: centerPos,
       zoom: 15,
       disableDefaultUI: true,
       mapTypeId: google.maps.MapTypeId.ROADMAP
     }
     this.map = new google.maps.Map(this.mapRef.nativeElement, options);

  
     this.directionsDisplay.setMap(this.map);
     this.map.addListener("click", (e) => {
       this.placeMarker(e.latLng, this.map);
     });

       }).catch((error) => {
         console.log('Error getting location', error);
       });

      
    }

    // Sets the map on all markers in the array.
  setMapOnAll(map) {
    for (let i = 0; i < this.mapMarkers.length; i++) {
      this.mapMarkers[i].setMap(map);
    }
  }

  // Removes the markers from the map, but keeps them in the array.
  clearMarkers() {
    this.setMapOnAll(null);
}
    
    /**
     * Este método coloca marcadores en el mapa
     * Se llama cuando el usuario hace doble click en el componente
     * @param latLng Latitud y longitud (posición)
     * @param map El mapa en donde se desea colocar el marcador
     */
    placeMarker(latLng, map) {
      if(this.markerCount < 10){
        let mapMarker = new google.maps.Marker({
          position: latLng,
          title: this.markerTitle[this.markerCount],
          label: this.markerCount.toString(),
          map: map,
        });
        this.mapMarkers.push(mapMarker);
        this.waypoints.push(mapMarker.position);
        
        this.markerCount++;
      }
      if(this.markerCount > 1){
        this.clearMarkers();
        this.calculateAndDisplayRoute(this.directionsService, this.directionsDisplay);
        this.calculateDistance(this.distanceMatrixService);
        console.log(this.waypoints);
      }
    }
    // Deletes all markers in the array by removing references to them.
  deleteMarkers() {
    this.clearMarkers();
    this.mapMarkers = [];
    this.markerCount = 0;
}
  
    /**
     * Este método agrega una ventana de información a los marcadores
     * @param marker El marcador
     */
    addInfoWindow(marker){
       let infoWindowContent =
       '<ion-text color="primary">' +
       '<h2>' + marker.title + '</h2>' +
       '</ion-text>';
      
      let infoWindow = new google.maps.InfoWindow(
        {
          content: infoWindowContent
      });
  
      marker.addListener('click', () => {
        this.closeAllInfoWindows();
        infoWindow.open(this.map, marker);
      });
      this.infoWindows.push(infoWindow);
  
    }
    closeAllInfoWindows(){
      for(let window of this.infoWindows){
        window.close();
      }
    }

    calculateAndDisplayRoute(directionsService, directionsRenderer) {
      directionsService.route(
        {
          origin: this.mapMarkers[0].position,
          destination: this.mapMarkers[this.markerCount-1].position,
          travelMode: google.maps.DirectionsTravelMode.DRIVING
        },
        (response, status) => {
          if (status === "OK") {
            directionsRenderer.setDirections(response);
          } else {
            window.alert("Directions request failed due to " + status);
          }
        }
      );
    }

  calculateDistance(distanceMatrixService){
    distanceMatrixService.getDistanceMatrix({
      origins: [this.mapMarkers[0].position],
      destinations: [this.mapMarkers[this.markerCount-1].position],
      travelMode: google.maps.TravelMode.DRIVING ,
      unitSystem: google.maps.UnitSystem.METRIC,
      avoidHighways: false,
      avoidTolls: false
  }, (response, status) => {
      if (status == google.maps.DistanceMatrixStatus.OK && response.rows[0].elements[0].status != "ZERO_RESULTS") {
          const distance = response.rows[0].elements[0].distance.text;
          this.distance = distance;
          
      } else {
          alert("Unable to find the distance via road.");
      }
  });
  }

  //Resetea todos los valores
  reset(){
    this.directionsDisplay.setMap(null);
    this.waypoints = [];
    this.markerCount = 0;
    this.distance = '0 km';
    this.mapMarkers = [];
    this.directionsDisplay.setMap(this.map);
  }

}