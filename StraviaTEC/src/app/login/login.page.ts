import { Component, OnInit } from '@angular/core';
import {Usuario } from '../modelos/usuario';
import { AlertController, ToastController } from '@ionic/angular';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { UsuarioService } from 'src/app/servicios/usuario.service';
import { HTTP } from '@ionic-native/http/ngx';
import { from } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {
  constructor(private http: HTTP, public toastController: ToastController, private usuarioService: UsuarioService, public alertController: AlertController, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() { 
  }
  //modelo de usuario login
  public usuarioL = new Usuario('', '');

  requestObject:any;


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


  /**
   * Este método recupera los datos de los campos de texto para validar el ingreso a
   * la página home. La validación se hace por medio de la api.
   */
    submit(){
      this.postData();
      /*
    this.http.setDataSerializer('json');
    let nativeCall = this.http.post('http://192.168.100.7:8085/api/user/login', this.usuarioL, 
    {'Content-Type': 'application/json'});

    from(nativeCall).pipe().subscribe( data => {
      console.log('native data: ', data);
      var parsed = JSON.parse(data.data);
    }, err => {
      console.log('JS Call error: ', err);
      this.presentToast('Error al iniciar sesión');
    });
  */
  }

  gotoInicio(){
    this.router.navigate(['/inicio']);
   }

   getData(){
    this.http.get('https://jsonplaceholder.typicode.com/comments?',
     {'postId': 1}, 
    {}).then(
      data => {
        console.log(data.data);
      })
    .catch(err => {
      console.log(err);
    });
   }

   postData(){
     let body = {
       'userId': 45,
       'id':101,
       'title':'Nogue api the monster server',
       'body':'Ehhtoh e sechh'
     };
    this.http.setDataSerializer('json');
    this.http.post('https://jsonplaceholder.typicode.com/posts',
    body, {'Content-Type': 'application/json'}).then(
     data => {
       console.log(data.data);
     })
   .catch(err => {
     console.log(err);
   });
   }


}
