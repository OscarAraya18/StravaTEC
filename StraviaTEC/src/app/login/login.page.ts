import { Component, OnInit } from '@angular/core';
import {Usuario } from '../modelos/usuario';
import { AlertController, ToastController, LoadingController } from '@ionic/angular';
import { ActivatedRoute, Router } from '@angular/router';
import { UsuarioService } from 'src/app/servicios/usuario.service';
import { HTTP } from '@ionic-native/http/ngx';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {

  //modelo de usuario login
  public usuarioL = new Usuario('', '');

  //Molde para tomar la dirección ip y el puerto de los imputs
  //Duración de la actividad
  serverConfig = {};

  constructor(private loadingController: LoadingController, private http: HTTP, public toastController: ToastController, private usuarioService: UsuarioService, public alertController: AlertController, private router: Router, private route: ActivatedRoute) { }

  
  ngOnInit() { 
  }

  /**
   * Método para mostrar una alerta en caso de que la contraseña sea incorrecta
   */
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
      message: 'Por favor espere...',
      spinner: "bubbles",
      cssClass: 'loading-k',
      backdropDismiss: false,
      showBackdrop: true
    });
    return await loading.present();
  }


  /**
   * Este método recupera los datos de los campos de texto para validar el ingreso a
   * la página home. La validación se hace por medio de la api.
   */
    submit(){
      this.presentLoading();  
      //Se le pasan los valores de configuración al servicio
      this.usuarioService.setNombreUsuarioActual(this.usuarioL.Usuario);
      this.usuarioService.setIp(this.serverConfig['ip']);
      this.usuarioService.setPuerto(this.serverConfig['puerto'].toString());

      //Se forma la url con las configuraciones optenidas
      
      let url = 'https://' + this.serverConfig['ip'] + ':' + this.serverConfig['puerto'].toString() + '/api/user/login';
    
      //Se configura native HTTP
      this.http.setServerTrustMode('nocheck');
      this.http.setDataSerializer('json');
      this.http.post(url,
      this.usuarioL, {'Content-Type': 'application/json'}).then(
       data => {
         console.log(data.data);
         this.loadingController.dismiss();
         this.usuarioL.Usuario = '';
         this.usuarioL.ClaveAcceso = '';
         this.gotoInicio();
      
       })
     .catch(err => {
       console.log(err);
       this.loadingController.dismiss();
       this.presentToast('Nombre de usuario o contraseña es incorrecto');
     });
     }
  

  gotoInicio(){
    this.router.navigate(['/inicio']);
   }

  }
