import { Component, Input, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import { environment } from 'src/environments/environment';
import { InicioSesionService } from 'src/app/servicios/inicioSesion.service';
import { error } from 'protractor';



@Component({
  selector: 'app-inicio-sesion',
  templateUrl: './inicio-sesion.component.html',
  styleUrls: ['./inicio-sesion.component.css']
})


export class InicioSesionComponent implements OnInit {
  textoBoton = "Iniciar Sesión";
  textoInput = "Clave de Acceso";
  tipoInput = "password";
  recuperandoCuenta = false;

  constructor(private router: Router, 
              private route: ActivatedRoute, 
              private inicioSesionService : InicioSesionService) { 
  }

  ngOnInit(): void {
    environment.nombreDeUsuario = "";
    environment.claveDeAcceso = "";
  }


  iniciarSesion(){
    let nombreDeUsuario = (document.getElementById("nombreDeUsuario") as HTMLInputElement).value;
    let claveDeAcceso = (document.getElementById("claveDeAcceso") as HTMLInputElement).value;

    if(this.recuperandoCuenta){
      this.inicioSesionService.solicitarRecuperarClaveAcceso(nombreDeUsuario,"prueba",claveDeAcceso).subscribe(
        data => {
          alert("Se ha enviado un correo electrónico a la dirección " + claveDeAcceso + " para proceder con la recuperación de su cuenta.");
        }, error => {
          alert("Se ha enviado un correo electrónico a la dirección " + claveDeAcceso + " para proceder con la recuperación de su cuenta.");
          this.textoBoton = "Iniciar Sesión";
          this.textoInput = "Clave de Acceso";
          this.tipoInput = "password";
          this.recuperandoCuenta = false;
        }
      );
    }else{
      let faltaDato = false;
      if(nombreDeUsuario==""){
        alert("Debe colocar su nombre de usuario. Intente nuevamente.");
        faltaDato = true;
      }else if(claveDeAcceso==""){
        alert("Debe colocar su clave de acceso. Intente nuevamente.");
        faltaDato = true;
      }
      if(!faltaDato){
        
        this.inicioSesionService.iniciarSesion(nombreDeUsuario,claveDeAcceso)
          .subscribe(
            data => {
            },
            error => {
              if (error["status"]==200){
                console.log("El usuario ingresado es valido");

                this.inicioSesionService.obtenerInformacion(nombreDeUsuario).subscribe(
                  data => {
                    environment.nombreDeUsuario = data["usuario"];
                    environment.nombreDelUsuario = data["nombre"] + " " + data["apellido1"];
                    environment.claveDeAcceso = data["claveacceso"];

                    if(data["foto"]==null){
                      environment.fotoDePerfilUsuario = environment.placeHolderFotoPerfil;
                    }else{
                      environment.fotoDePerfilUsuario = "data:image/png;base64,"+data["foto"];
                    }
                    this.router.navigate(['/mi-muro']);
                  }
                );

              }else{
                if(confirm("Al parecer has olvidado tu clave de acceso. ¿Deseas realizar el proceso de recuperación por correo electrónico?")){
                  this.recuperarCuenta(nombreDeUsuario);
                }
              }
            }
          );


      }
    }
  }

  recuperarCuenta(nombreUsuario: string){
    this.textoBoton = "Recuperar Cuenta";
    this.textoInput = "Correo electrónico";
    this.recuperandoCuenta = true;
    this.tipoInput = "text";
  }


}



