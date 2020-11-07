import { Component, OnInit, OnDestroy  } from '@angular/core';
import {ViewChild, ElementRef } from '@angular/core';
import { Geolocation } from '@ionic-native/geolocation/ngx';

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


  constructor(private geolocation: Geolocation) { }

  ngOnInit() {
    this.showMap();

  }

  ionViewDidEnter() { 
    
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
         zoom: 8,
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

         this.loadXmlFile("assets/routes/Morning_Ride.gpx");
  
        
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

        var range = Math.trunc(gpxWaypoints.length / 25);
        var newWayPoints = [];
        for(let i = 0; i < gpxWaypoints.length; i += range+1){
          newWayPoints.push(gpxWaypoints[i]);
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
            travelMode: google.maps.DirectionsTravelMode.DRIVING
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

  
}
