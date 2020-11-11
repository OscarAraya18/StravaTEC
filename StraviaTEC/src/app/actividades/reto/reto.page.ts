import { Component, OnInit } from '@angular/core';
import {ViewChild, ElementRef } from '@angular/core';
import { Geolocation } from '@ionic-native/geolocation/ngx';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { RetoService } from 'src/app/servicios/reto.service';
import { ToastController } from '@ionic/angular';
import { AlertController } from '@ionic/angular';
import { DatabaseService, DeportistaReto} from 'src/app/servicios/database.service';
import { UsuarioService } from 'src/app/servicios/usuario.service';
import 'xml-writer';
import { unwatchFile } from 'fs';

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

  //Contador para controlar que solo hayan 2 marcadores
  //Salida y Entrada
  markerCount = 0;

  //me dice cuantos marcadores me quedan
  availableMarkers = 10;
  //Servicios para direcciones
  directionsService = new google.maps.DirectionsService();
  directionsDisplay = new google.maps.DirectionsRenderer();
  distanceMatrixService = new google.maps.DistanceMatrixService();

  //Variable que va a almacenar la distancia entre los puntos
  distance:number = 0;

  //waypoints de la ruta dibujada
  waypoints:any  = [];

  //Nombre del reto
  nombreReto: string;

  //Nombre del administrador
  admin: string;

  //el tipo de actividad
  tipoActividad: string;

  //Duración de la actividad
  duracion = {};

  //El nombre de usuario actual
  nombreUsuario: string;


  constructor(public alertController: AlertController, private usuarioService: UsuarioService, private db: DatabaseService,public toastController: ToastController, private geolocation: Geolocation, private router: Router, private route: ActivatedRoute, private retoService: RetoService) { }
  
  async presentarAlertaGuardado() {
    if(this.waypoints.length > 1){
      const alert = await this.alertController.create({
        cssClass: 'alerta-k',
        header: 'Guardar nueva actividad',
        message: 'Escriba el nombre de la actividad',
        inputs: [
          {
            name: 'nombreA',
            type: 'text',
            placeholder: '',
            attributes: {
              maxlength: 30,
            }
          }
        ],
        buttons: [
          {
            text: 'Cancelar',
            role: 'cancel',
            cssClass: 'secondary',
            handler: () => {
              console.log('Confirm Cancel');
            }
          }, {
            text: 'Aplicar',
            handler: (alertData) => {
              this.addDeportistaReto(alertData.nombreA);
            }
          }
        ]
      });
  
      await alert.present();
    }
    else{
      this.presentToast('No se ha seleccionado el recorrido');
    }
   
  }

  async presentToast(mensaje: string) {
    const toast = await this.toastController.create({
      message: mensaje,
      cssClass: 'alerta-k',
      duration: 3000,
      position: 'top'
    });
    toast.present();
  }


  ngOnInit() {
    this.nombreUsuario = this.usuarioService.getNombreUsuarioActual();
    this.showMap();
    this.route.params.forEach((urlParams) => {
      this.nombreReto = urlParams['nombreReto'];
      this.admin = urlParams['admin'];
      this.tipoActividad = urlParams['tipoActividad'];
    });
  }
  addDeportistaReto(nombreActividad) {
    if(this.waypoints.length > 1){
      let gpx = this.writeGPX();
      var fechaHora = new Date();
      var fechaHoraString = fechaHora.toLocaleDateString();
      let duracion = this.duracion['horas'] + ':' + this.duracion['minutos'] + ':' + this.duracion['segundos'];
      this.db.addDeportistaReto(this.nombreUsuario, this.admin, nombreActividad, this.tipoActividad, this.nombreReto, this.distance, duracion, gpx, fechaHoraString).then(_ => {
        this.presentToast('Actividad guardada localmente');
        this.gotoInicio();
      });
    }
    else{
      this.presentToast('Se necesitan al menos dos puntos');
    }

  }

    //Este método se ejecuta cada vez que se abra esta página
    ionViewDidEnter(){
      this.nombreUsuario = this.usuarioService.getNombreUsuarioActual();
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
     this.map.addListener("dblclick", (e) => {
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
          
          map: map,
        });
        this.mapMarkers.push(mapMarker);
        this.waypoints.push(mapMarker.getPosition());
        
        this.markerCount++;
        this.availableMarkers--;
      }
      if(this.markerCount > 1){
        this.clearMarkers();
        this.calculateAndDisplayRoute(this.directionsService, this.directionsDisplay);
        this.calculateDistance(this.distanceMatrixService);
      }
      if(this.markerCount === 10){
        this.presentToast('Has llegado al límite de marcadores');
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
          travelMode: google.maps.DirectionsTravelMode.WALKING
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
      travelMode: google.maps.TravelMode.WALKING ,
      unitSystem: google.maps.UnitSystem.METRIC,
      avoidHighways: false,
      avoidTolls: false
  }, (response, status) => {
      if (status == google.maps.DistanceMatrixStatus.OK && response.rows[0].elements[0].status != "ZERO_RESULTS") {
          const distance = response.rows[0].elements[0].distance.value;
          this.distance = distance/1000;
          
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
    this.availableMarkers = 10;
    this.distance = 0;
    this.mapMarkers = [];
    this.directionsDisplay = new google.maps.DirectionsRenderer();
    this.directionsDisplay.setMap(this.map);
  }
  
  writeGPX(){
    var XMLWriter = require('xml-writer');
    var xw = new XMLWriter(true);
    xw.startDocument();

    xw.startElement('gpx').writeAttribute('creator', 'Kevin Acevedo Rodríguez');

    xw.startElement('metadata','').writeElement('time','2020-10-16T18:47:01Z');
    xw.endElement();

    xw.startElement('trk')
    xw.writeElement('name', 'Los relevos del diablo');
    xw.writeElement('type', '1');

    xw.startElement('trkseg', '');

    //Se colocan los puntos de la ruta
    for(let i = 0; i < this.waypoints.length ; i++){
      xw.startElement('trkpt', '');
      xw.writeAttribute('lat', this.waypoints[i].lat().toString());
      xw.writeAttribute('lon', this.waypoints[i].lng().toString());
      xw.writeElement('ele', '1446.6');
      xw.writeElement('time', '020-10-16T18:47:01Z')
      xw.endElement();
    }

    xw.endElement();

    xw.endDocument();
 
    console.log(xw.toString());
    return xw.toString();
  }
  gotoInicio(){
    this.router.navigate(['/inicio']);
   }

}
