import { Component, OnInit } from '@angular/core';
import { CarreraService } from 'src/app/servicios/carrera.service';
import { RetoService } from 'src/app/servicios/reto.service';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { DatabaseService, DeportistaCarrera, DeportistaReto } from 'src/app/servicios/database.service';
import { UsuarioService } from 'src/app/servicios/usuario.service';
import { AlertController } from '@ionic/angular';
import { LoadingController } from '@ionic/angular';
import { Carrera } from '../modelos/carrera';
import { Reto } from '../modelos/reto';
import { Actividad } from '../modelos/actividad';

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

  constructor(public loadingController: LoadingController, public alertController: AlertController, private usuarioService: UsuarioService, private db: DatabaseService,private carreraService: CarreraService, private retoService: RetoService, private router: Router, private route: ActivatedRoute) { 
  }
  
  //Lista de carreas local
  listaCarreras: Carrera[] = [];

  //Lista de retos local
  listaRetos: Reto[] = [];

  //DeportistasCarrera de la base de datos empotrada
  deportistasCarrera: DeportistaCarrera[] = [];

  //DeportistasReto de la base de datos empotrada
  deportistasReto: DeportistaReto[] = [];

  //Vista seleccionada para el primer slide
  selectedView = 'retos';

  nombreUsuario: string;


  async presentLoading(mensaje) {
    const loading = await this.loadingController.create({
      spinner: "bubbles",
      duration: 5000,
      message: mensaje,
      cssClass: 'loading-k',
      backdropDismiss: false,
      showBackdrop: true

    });
    await loading.present();

    const { role, data } = await loading.onDidDismiss();
    console.log('Loading dismissed with role:', role);
  }

  async presentarAlertaSincronizacionReto(d) {
    const alert = await this.alertController.create({
      cssClass: 'alerta-k',
      header: 'Sincronizar Reto',
      backdropDismiss: false,
      buttons: [
        {
          text: 'Cancelar',
          role: 'cancel',
          cssClass: 'secondary',
          handler: (blah) => {
            console.log('Confirm Cancel: blah');
          }
        }, {
          text: 'Aceptar',
          handler: () => {
            this.presentLoading('Procesando');
            var fechaHora = new Date();
            var fechaHoraString = fechaHora.toLocaleDateString();
            console.log(fechaHoraString);
            //Enviar los datos en post
            //var paquete = new Actividad(d.Usuario, d.NombreActividad, fechaHoraString, d.Duracion, d.Distancia, d.TipoActividad, d.RecorridoGPX, d.NombreReto, d.AminDeportista, 1);
            this.db.deleteDeportistaReto(d.Usuario, d.NombreReto);
            
            
          }
        }
      ]
    });

    await alert.present();
  }

  async presentarAlertaSincronizacionCarrera(d) {
    const alert = await this.alertController.create({
      cssClass: 'alerta-k',
      header: 'Sincronizar Carrera',
      backdropDismiss: false,
      buttons: [
        {
          text: 'Cancelar',
          role: 'cancel',
          cssClass: 'secondary',
          handler: (blah) => {
            console.log('Confirm Cancel: blah');
          }
        }, {
          text: 'Aceptar',
          handler: () => {
            this.presentLoading('Procesando');
            var fechaHora = new Date();
            var fechaHoraString = fechaHora.toLocaleDateString();
            console.log(fechaHoraString);
            //Enviar los datos en post
            //var paquete = new Actividad(d.Usuario, d.NombreActividad, fechaHoraString, d.Duracion, 1, d.TipoActividad, d.RecorridoGPX, d.NombreCarrera, d.AminDeportista, 2);
            
            this.db.deleteDeportistaCarrera(d.Usuario, d.NombreCarrera);
          }
        }
      ]
    });

    await alert.present();
  }
  ngOnInit() {
    this.nombreUsuario = this.usuarioService.getNombreUsuarioActual();
    //Recuperar los datos de la base de datos empotrada
    this.db.getDatabaseState().subscribe(rdy => {
      if (rdy) {
        this.db.loadDeportistasCarrera(this.nombreUsuario);
        this.db.loadDeportistasReto(this.nombreUsuario);

        this.db.getDeportistasCarrera().subscribe(devs => {
          this.deportistasCarrera = devs;
        })
      }

    });
    this.db.getDeportistasReto().subscribe(dep => {
      this.deportistasReto = dep;
    })

    //this.getCarreras();
    //this.getRetos()
    //Almaceno las carreras en la variable de la página
    this.listaCarreras = this.carreraService.getListaCarreras();
    //Almaceno los retos en la variable de la página
    this.listaRetos = this.retoService.getListaRetos();
  }
  ionViewDidEnter(){
    this.nombreUsuario = this.usuarioService.getNombreUsuarioActual();
  }

  retoClick(nombreReto: string, admin: string, tipoActividad: string){
    this.router.navigate(['/reto', nombreReto, admin, tipoActividad]);
  }

  carreraClick(nombreCarrera: string, admin: string, tipoActividad: string){
    this.router.navigate(['/carrera', nombreCarrera, admin, tipoActividad]);
  }
}
