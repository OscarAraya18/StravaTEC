import { Component, OnInit } from '@angular/core';
import {Usuario } from '../modelos/usuario';
import { AlertController } from '@ionic/angular';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { UsuarioService } from 'src/app/servicios/usuario.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {
  constructor(private usuarioService: UsuarioService, public alertController: AlertController, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() { 
  }
  //modelo de usuario login
  public usuarioL = new Usuario('', '');


  /**
   * Método para mostrar una alerta en caso de que la contraseña sea incorrecta
   */
  async presentAlert() {
    const alert = await this.alertController.create({
      cssClass: 'my-custom-class',
      header: 'Alerta',
      subHeader: 'Inicio de sesión',
      message: 'El nombre de usuario o la contraeña son incorrectos',
      buttons: ['OK']
    });

    await alert.present();
  }

  /**
   * Este método recupera los datos de los campos de texto para validar el ingreso a
   * la página home. La validación se hace por medio de la api.
   */
  submit(){
    //this.presentAlert();

    //Se guarda el nombre usuario en la variable global del servicio
    this.usuarioService.setNombreUsuarioActual(this.usuarioL.nombreUsuario);
    this.gotoInicio();
  }
  gotoInicio(){
    this.router.navigate(['/inicio']);
   }


}
