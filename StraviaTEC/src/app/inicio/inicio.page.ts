import { Component, OnInit } from '@angular/core';
import {ViewChild, ElementRef } from '@angular/core';
import { Geolocation } from '@ionic-native/geolocation/ngx';

declare var google: any;

@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.page.html',
  styleUrls: ['./inicio.page.scss'],
})


export class InicioPage implements OnInit {

  
  //Se le indica el elemento html correspondiente
  @ViewChild('map', {read:ElementRef, static: false}) mapRef:ElementRef;

  //Mapa que se está usando en la página
  map: any;

  directionsService = new google.maps.DirectionsService();
  directionsDisplay;
  distanceMatrixService = new google.maps.DistanceMatrixService();

  //GPX PARSER
  xhttp = new XMLHttpRequest();
  parser = new DOMParser();
  
  //Geolocalización
  latitude: any;
  longitude: any;
   

  //Opciones Fade del slider
  slideOpts = {
    on: {
      beforeInit() {
        const swiper = this;
        swiper.classNames.push(`${swiper.params.containerModifierClass}flip`);
        swiper.classNames.push(`${swiper.params.containerModifierClass}3d`);
        const overwriteParams = {
          slidesPerView: 1,
          slidesPerColumn: 1,
          slidesPerGroup: 1,
          watchSlidesProgress: true,
          spaceBetween: 0,
          virtualTranslate: true,
        };
        swiper.params = Object.assign(swiper.params, overwriteParams);
        swiper.originalParams = Object.assign(swiper.originalParams, overwriteParams);
      },
      setTranslate() {
        const swiper = this;
        const { $, slides, rtlTranslate: rtl } = swiper;
        for (let i = 0; i < slides.length; i += 1) {
          const $slideEl = slides.eq(i);
          let progress = $slideEl[0].progress;
          if (swiper.params.flipEffect.limitRotation) {
            progress = Math.max(Math.min($slideEl[0].progress, 1), -1);
          }
          const offset$$1 = $slideEl[0].swiperSlideOffset;
          const rotate = -180 * progress;
          let rotateY = rotate;
          let rotateX = 0;
          let tx = -offset$$1;
          let ty = 0;
          if (!swiper.isHorizontal()) {
            ty = tx;
            tx = 0;
            rotateX = -rotateY;
            rotateY = 0;
          } else if (rtl) {
            rotateY = -rotateY;
          }
  
           $slideEl[0].style.zIndex = -Math.abs(Math.round(progress)) + slides.length;
  
           if (swiper.params.flipEffect.slideShadows) {
            // Set shadows
            let shadowBefore = swiper.isHorizontal() ? $slideEl.find('.swiper-slide-shadow-left') : $slideEl.find('.swiper-slide-shadow-top');
            let shadowAfter = swiper.isHorizontal() ? $slideEl.find('.swiper-slide-shadow-right') : $slideEl.find('.swiper-slide-shadow-bottom');
            if (shadowBefore.length === 0) {
              shadowBefore = swiper.$(`<div class="swiper-slide-shadow-${swiper.isHorizontal() ? 'left' : 'top'}"></div>`);
              $slideEl.append(shadowBefore);
            }
            if (shadowAfter.length === 0) {
              shadowAfter = swiper.$(`<div class="swiper-slide-shadow-${swiper.isHorizontal() ? 'right' : 'bottom'}"></div>`);
              $slideEl.append(shadowAfter);
            }
            if (shadowBefore.length) shadowBefore[0].style.opacity = Math.max(-progress, 0);
            if (shadowAfter.length) shadowAfter[0].style.opacity = Math.max(progress, 0);
          }
          $slideEl
            .transform(`translate3d(${tx}px, ${ty}px, 0px) rotateX(${rotateX}deg) rotateY(${rotateY}deg)`);
        }
      },
      setTransition(duration) {
        const swiper = this;
        const { slides, activeIndex, $wrapperEl } = swiper;
        slides
          .transition(duration)
          .find('.swiper-slide-shadow-top, .swiper-slide-shadow-right, .swiper-slide-shadow-bottom, .swiper-slide-shadow-left')
          .transition(duration);
        if (swiper.params.virtualTranslate && duration !== 0) {
          let eventTriggered = false;
          // eslint-disable-next-line
          slides.eq(activeIndex).transitionEnd(function onTransitionEnd() {
            if (eventTriggered) return;
            if (!swiper || swiper.destroyed) return;
  
            eventTriggered = true;
            swiper.animating = false;
            const triggerEvents = ['webkitTransitionEnd', 'transitionend'];
            for (let i = 0; i < triggerEvents.length; i += 1) {
              $wrapperEl.trigger(triggerEvents[i]);
            }
          });
        }
      }
    }
  };
  constructor(private geolocation: Geolocation) { }

  ngOnInit() {
    this.showMap();
  }
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
     zoom: 10,
     minZoom: 10,
     maxZoom: 10,
     disableDefaultUI: true,
     mapTypeId: 'hybrid',
     gestureHandling: "greedy",
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

  retoClick(){
    console.log("evento de click en las cartas de los retos");
  }

  carreraClick(){
    console.log("evento de click en las cartas de los retos");
  }

}
