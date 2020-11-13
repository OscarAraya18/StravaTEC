import { Component, OnInit } from '@angular/core';
import { CarreraService } from 'src/app/servicios/carrera.service';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { DatabaseService, DeportistaActividadLibre, DeportistaCarrera, DeportistaReto } from 'src/app/servicios/database.service';
import { UsuarioService } from 'src/app/servicios/usuario.service';
import { AlertController, ToastController } from '@ionic/angular';
import { LoadingController } from '@ionic/angular';
import { HTTP } from '@ionic-native/http/ngx';
import { Actividad } from 'src/app/modelos/actividad';


@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.page.html',
  styleUrls: ['./inicio.page.scss'],
})


export class InicioPage implements OnInit {
  //GPX PARSER
  xhttp = new XMLHttpRequest();
  parser = new DOMParser();
  

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

  constructor(public toastController: ToastController, private http: HTTP, public loadingController: LoadingController, public alertController: AlertController, private usuarioService: UsuarioService, private db: DatabaseService,private carreraService: CarreraService, private router: Router, private route: ActivatedRoute) { 
  }
  
  //Lista de carreas local
  listaCarreras: any = [];

  //Lista de retos local
  listaRetos: any = [];

  //DeportistasCarrera de la base de datos empotrada
  deportistasCarrera: DeportistaCarrera[] = [];

  //DeportistasReto de la base de datos empotrada
  deportistasReto: DeportistaReto[] = [];

  //DeportistasActividadLibre de la base de datos empotrada
  deportistasActividadLibre: DeportistaActividadLibre[] = [];

  //Vista seleccionada para el primer slide
  selectedView = 'retos';

  nombreUsuario: string;

  ngOnInit() {
    this.nombreUsuario = this.usuarioService.getNombreUsuarioActual();
    //Recuperar los datos de la base de datos empotrada
    this.db.getDatabaseState().subscribe(rdy => {
      if (rdy) {
        this.db.loadDeportistasCarrera(this.nombreUsuario);
        this.db.loadDeportistasReto(this.nombreUsuario);
        this.db.loadDeportistasActividadLibre(this.nombreUsuario);

        this.db.getDeportistasCarrera().subscribe(devs => {
          this.deportistasCarrera = devs;
          //Guardar esta lista de carreras en la lista de carreras del servicio
          this.carreraService.setListaCarreras(devs);
        })
      }
    });
    //Se piden los 
    this.db.getDeportistasReto().subscribe(dep => {
      this.deportistasReto = dep;
    });

    this.db.getDeportistasActividadLibre().subscribe(dep => {
      this.deportistasActividadLibre = dep;
    });

    //cargar las listas con datos del servidor
    this.http.setServerTrustMode('nocheck');
    this.loadRetos();
    this.loadCarreras();


  }
  loadRetos(){
    let url = 'https://' + this.usuarioService.getIp() + ':' + this.usuarioService.getPuerto() + '/api/user/retos?';
    //Se le pide la lista de retos al servidor
    this.http.get(url,
    {'usuario': this.nombreUsuario}, 
    {}).then(
     data => {
       this.listaRetos = JSON.parse(data.data);
     })
    .catch(err => {
     console.log(err);
    });
  }

  loadCarreras(){
    let url = 'https://' + this.usuarioService.getIp() + ':' + this.usuarioService.getPuerto() + '/api/user/carreras?';
   //Se le pide la lista de carreas al servidor
   this.http.get(url,
    {'usuario': this.nombreUsuario}, 
   {}).then(
     data => {
       this.listaCarreras = JSON.parse(data.data);
     })
   .catch(err => {
     console.log(err);
   });
  }


  async presentToast(mensaje: string) {
    const toast = await this.toastController.create({
      message: mensaje,
      duration: 3000,
      position: 'top',
      cssClass: 'toast-k'
    });
    toast.present();
  }

  async presentLoading() {
    const loading = await this.loadingController.create({
      message: 'Sincronizando con el servidor...',
      spinner: "bubbles",
      cssClass: 'loading-k',
      backdropDismiss: false,
      showBackdrop: true
    });
    return await loading.present();
  }

  sincronizarReto(d){
    this.presentLoading();
    //Se empaqueta la actividad en un modelo de clase
    let actividadReto = new Actividad(d.Usuario, d.NombreActividad, d.FechaHora, d.Duracion, d.Distancia, d.TipoActividad, d.RecorridoGPX, d.NombreReto, d.AdminDeportista, 1);
    //se le envía a la api
    this.usuarioService.setNombreUsuarioActual(d.Usuario);
    this.http.setServerTrustMode('nocheck');
    this.http.setDataSerializer('json');
    let url = 'https://' + this.usuarioService.getIp() + ':' + this.usuarioService.getPuerto() + '/api/user/registrar/actividad';
    this.http.post(url,
    actividadReto, {'Content-Type': 'application/json'}).then(
     data => {
       console.log(data.data);
       //Si la petición fue exitosa entonces se elimina la actividad de la base de datos empotrada
       this.db.deleteDeportistaReto(d.Usuario, d.NombreReto, d.NombreActividad);
       this.loadingController.dismiss();
       this.presentToast('La actividad se ha sincronizado con éxito');
     })
   .catch(err => {
     console.log(err);
     this.loadingController.dismiss();
     this.presentToast('Error al sincronizar la actividad');
   });
  }

  sincronizarCarrera(d){
    this.presentLoading();
    //Se empaqueta la actividad en un modelo de clase
    let actividadReto = new Actividad(d.Usuario, d.NombreActividad, d.FechaHora, d.Duracion, 10, d.TipoActividad, d.RecorridoGPX, d.NombreCarrera, d.AdminDeportista, 0);
    //se le envía a la api
    this.usuarioService.setNombreUsuarioActual(d.Usuario);
    this.http.setServerTrustMode('nocheck');
    this.http.setDataSerializer('json');
    let url = 'https://' + this.usuarioService.getIp() + ':' + this.usuarioService.getPuerto() + '/api/user/registrar/actividad';
    this.http.post(url,
    actividadReto, {'Content-Type': 'application/json'}).then(
     data => {
       console.log(data.data);
       //Si la petición fue exitosa entonces se elimina la actividad de la base de datos empotrada
       this.db.deleteDeportistaCarrera(d.Usuario, d.NombreCarrera);
       this.loadingController.dismiss();
       this.presentToast('La actividad se ha sincronizado con éxito');
     })
   .catch(err => {
     console.log(err);
     this.loadingController.dismiss();
     this.presentToast('Error al sincronizar la actividad');
   });
  }

  
  sincronizarActividadLibre(d){
    this.presentLoading();
    //Se empaqueta la actividad en un modelo de clase
    let actividadReto = new Actividad(d.Usuario, d.NombreActividad, d.FechaHora, d.Duracion, d.Distancia, d.TipoActividad, d.RecorridoGPX, null, null, 2);
    //se le envía a la api
    this.usuarioService.setNombreUsuarioActual(d.Usuario);
    this.http.setServerTrustMode('nocheck');
    this.http.setDataSerializer('json');
    let url = 'https://' + this.usuarioService.getIp() + ':' + this.usuarioService.getPuerto() + '/api/user/registrar/actividad';
    this.http.post(url,
    actividadReto, {'Content-Type': 'application/json'}).then(
     data => {
       console.log(data.data);
       //Si la petición fue exitosa entonces se elimina la actividad de la base de datos empotrada
       this.db.deleteDeportistaActividadLibre(d.Usuario, d.NombreActividad, d.FechaHora);
       this.loadingController.dismiss();
       this.presentToast('La actividad se ha sincronizado con éxito');
     })
   .catch(err => {
     console.log(err);
     this.loadingController.dismiss();
     this.presentToast('Error al sincronizar la actividad');
   });
  }

  eliminarReto(d){
    this.db.deleteDeportistaReto(d.Usuario, d.NombreReto, d.NombreActividad);
    this.presentToast('La actividad se ha eliminado con éxito');
  }

  eliminarCarrera(d){
    this.db.deleteDeportistaCarrera(d.Usuario, d.NombreCarrera);
    this.presentToast('La actividad se ha eliminado con éxito');
  }

  eliminarActividadLibre(d){
    this.db.deleteDeportistaActividadLibre(d.Usuario, d.NombreActividad, d.FechaHora);
    this.presentToast('La actividad se ha eliminado con éxito');
  }

  retoClick(nombreReto: string, admin: string, tipoActividad: string){
    this.router.navigate(['/reto', nombreReto, admin, tipoActividad]);
  }

  carreraClick(nombreCarrera: string, admin: string, tipoActividad: string){
    this.router.navigate(['/carrera', nombreCarrera, admin, tipoActividad]);
  }

  actividadLibreClick(){
    this.router.navigate(['/actividad-libre']);
  }
}
