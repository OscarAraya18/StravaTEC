import { DatabaseService, DeportistaCarrera} from 'src/app/servicios/database.service';
import { Component, OnInit} from '@angular/core';
import {ViewChild, ElementRef } from '@angular/core';
import { Geolocation } from '@ionic-native/geolocation/ngx';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { ToastController } from '@ionic/angular';
import { AlertController } from '@ionic/angular';
import { UsuarioService } from 'src/app/servicios/usuario.service';
import { CarreraService } from 'src/app/servicios/carrera.service';

declare var google: any;

@Component({
  selector: 'app-carrera',
  templateUrl: './carrera.page.html',
  styleUrls: ['./carrera.page.scss'],
})
export class CarreraPage implements OnInit {
   //Mapa que se está usando en la página
   map: any;

   //Geolocalización
   latitude: any;
   longitude: any;
 
   //Se le indica el elemento html correspondiente
   @ViewChild('map', {read:ElementRef, static: false}) mapRef:ElementRef;

  //Servicios para direcciones
  directionsService = new google.maps.DirectionsService();
  directionsDisplay;
  distanceMatrixService = new google.maps.DistanceMatrixService();

  distance: any = '0 km';

  //GPX PARSER
  xhttp = new XMLHttpRequest();
  parser = new DOMParser();

    //Nombre del reto
    nombreCarrera: string;

    //Nombre del administrador
    admin: string;
  
    //el tipo de actividad
    tipoActividad: string;

    //recorrido gpx
    recorridoGPX: string;
  
  duracion = {};

  //Nombre de usuario actual
  nombreUsuario: string;

  constructor(private carreraService: CarreraService, private usuarioService: UsuarioService, public alertController: AlertController, public toastController: ToastController, private db: DatabaseService, private geolocation: Geolocation, private router: Router, private route: ActivatedRoute) { }
  
  async presentarAlertaGuardado() {
    const alert = await this.alertController.create({
      cssClass: 'alerta-k',
      header: 'Guardar nueva actividad',
      message: 'Escriba el nombre de la actividad',
      inputs: [
        {
          name: 'nombreA',
          type: 'text',
          placeholder: 'Nombre de la actividad',
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
            this.addDeportistaCarrera(alertData.nombreA);
          }
        }
      ]
    });

    await alert.present();
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
      this.nombreCarrera = urlParams['nombreCarrera'];
      this.admin = urlParams['admin'];
      this.tipoActividad = urlParams['tipoActividad'];
    });
      
  }
  //Este método se ejecuta cada vez que se abra esta página
  ionViewDidEnter(){
    this.nombreUsuario = this.usuarioService.getNombreUsuarioActual();
    this.showMap();
  }

  addDeportistaCarrera(nombreActividad: string) {
    var date = new Date();
    var fecha = date.getFullYear() +'-'+(date.getMonth()+1) + '-'+date.getDate();
    var hora = date.getHours() +':' + date.getMinutes() + ':' + date.getSeconds();
    var fechaHoraString = fecha + ' ' + hora;
    let duracion = this.duracion['horas'] + ':' + this.duracion['minutos'] + ':' + this.duracion['segundos'];
    this.db.addDeportistaCarrera(this.nombreUsuario, this.admin, nombreActividad, this.tipoActividad, this.nombreCarrera, duracion, this.recorridoGPX, fechaHoraString).then(_ => {
      this.duracion = {};
      this.presentToast('Actividad guardada localmente');
      this.gotoInicio();
    });
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
       const options ={
         center: centerPos,
         zoom: 15,
         disableDefaultUI: true,
         mapTypeId: 'hybrid',
       }
       this.map = new google.maps.Map(this.mapRef.nativeElement, options);
  
       this.directionsDisplay = new google.maps.DirectionsRenderer({
        suppressMarkers: true
       });
       this.directionsDisplay.setMap(this.map);
  
         }).catch((error) => {
           console.log('Error getting location', error);
         });
      }

      loadXmlFile(path: string){
        this.xhttp.onreadystatechange = () => {
        if (this.xhttp.readyState == 4 && this.xhttp.status == 200) {
           // Typical action to be performed when the document is ready:
          this.getGpxRoute(this.xhttp.responseText);
        }
        };
        this.xhttp.open("GET", path, true);
        this.xhttp.send();
      }
    
      getGpxRoute(response: string){
        this.recorridoGPX = response;
        const xmlDoc = this.parser.parseFromString(response,"text/xml");
        const track = xmlDoc.getElementsByTagName("trkpt");
        var gpxWaypoints = [];
        var origin: any;
        var destination: any;

        for(let i = 0; i < track.length; i++){
            var stop = new google.maps.LatLng(track[i].getAttribute('lat'), track[i].getAttribute('lon'));
            gpxWaypoints.push({
            location: stop,
            stopover: true
          });
          if(i === 0){
            origin = new google.maps.LatLng(track[i].getAttribute('lat'), track[i].getAttribute('lon'));
          }
          if(i === track.length -1){
            destination = new google.maps.LatLng(track[i].getAttribute('lat'), track[i].getAttribute('lon'));
          }
        }

        this.createMarker(origin, 'START', 'A');
        this.createMarker(destination, 'END', 'B');
        var newWayPoints = [];

        if(gpxWaypoints.length > 25){
          var range = Math.trunc(gpxWaypoints.length / 25);
          for(let i = 0; i < gpxWaypoints.length; i += range+1){
            newWayPoints.push(gpxWaypoints[i]);
          }
        }
        else{
          newWayPoints = gpxWaypoints;
        }
        //max waypoints = 25
        this.calculateAndDisplayRoute(this.directionsService, this.directionsDisplay, origin, destination, newWayPoints);
      }

      calculateAndDisplayRoute(directionsService, directionsRenderer, origin, destination, waypts) {
        directionsService.route(
          {
            origin: origin,
            destination: destination,
            waypoints: waypts,
            optimizeWaypoints: true,
            travelMode: google.maps.DirectionsTravelMode.WALKING
          },
          (response, status) => {
            if (status === "OK") {
              directionsRenderer.setDirections(response);
              var route = response.routes[0];
            } else {
              window.alert("Directions request failed due to " + status);
            }
          }
        );
      }

    createMarker(latlng, title, label) {
      var marker = new google.maps.Marker({
          position: latlng,
          map: this.map,
          label: label
      });
      this.addInfoWindow(marker, title);
    }
        /**
     * Este método agrega una ventana de información a los marcadores
     * @param marker El marcador
     */
    addInfoWindow(marker, title){
      let infoWindowContent =
      '<ion-text color="primary">' +
      '<h2>' + title + '</h2>' +
      '</ion-text>';
     
     let infoWindow = new google.maps.InfoWindow(
       {
         content: infoWindowContent
     });
 
     marker.addListener('click', () => {
       infoWindow.open(this.map, marker);
     });
   } 

   gotoInicio(){
    this.router.navigate(['/inicio']);
   }

   paintGPX(){
     var gpxCarrera = this.carreraService.getCarreraGPX(this.nombreCarrera);
     console.log('GPX Type', gpxCarrera);
     if(gpxCarrera != null){
       this.getGpxRoute(gpxCarrera);
     }
   }

  
}
