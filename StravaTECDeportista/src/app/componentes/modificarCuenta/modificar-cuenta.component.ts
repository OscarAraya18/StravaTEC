import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import { from } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ModificarCuentaService } from 'src/app/servicios/modificarCuenta.service';


@Component({
  selector: 'app-modificar-cuenta',
  templateUrl: './modificar-cuenta.component.html',
  styleUrls: ['./modificar-cuenta.component.css']
})

export class ModificarCuentaComponent implements OnInit {
  metaDataFoto = environment.metadaDataFoto;
  nombreDelUsuario = ""; 
  nombreDeUsuario = "";
  fotoDePerfilUsuario = "";
  nuevaFotoPerfil: any;

  modificarPrimerNombre = true;
  modificarSegundoNombre = true;
  modificarPrimerApellido = true;
  modificarSegundoApellido = true;
  modificarFechaNacimiento = true;
  modificarNacionalidad = true;
  modificarClaveAcceso = true;

  datosUsuario = {
    primerNombre : "",
    segundoNombre : "",
    primerApellido : "",
    segundoApellido : "",
    fechaNacimiento : "",
    nacionalidad : "",
    claveAcceso : "",
    fotoPerfil : ""
  }

  constructor(private router: Router, private route: ActivatedRoute, private modificarCuentaService : ModificarCuentaService) { }

  ngOnInit(): void {
    this.nombreDelUsuario = environment.nombreDelUsuario;
    this.nombreDeUsuario = environment.nombreDeUsuario;
    this.fotoDePerfilUsuario = environment.fotoDePerfilUsuario;
    this.nuevaFotoPerfil = this.fotoDePerfilUsuario;
    this.modificarCuentaService.solicitarInformacionUsuario(this.nombreDeUsuario).subscribe(
      data => {
        this.datosUsuario.primerNombre = data["nombre"];
        this.datosUsuario.primerApellido = data["apellido1"];
        this.datosUsuario.segundoApellido = data["apellido2"];
        this.datosUsuario.nacionalidad = data["nacionalidad"];
        this.datosUsuario.claveAcceso = data["claveacceso"];
        this.datosUsuario.fotoPerfil = data["foto"];
        this.datosUsuario.fechaNacimiento = data["fechanacimiento"][0]+data["fechanacimiento"][1]+
                                            data["fechanacimiento"][2]+data["fechanacimiento"][3]+
                                            data["fechanacimiento"][4]+data["fechanacimiento"][5]+
                                            data["fechanacimiento"][6]+data["fechanacimiento"][7]+
                                            data["fechanacimiento"][8]+data["fechanacimiento"][9];
      });
  }


  subidaFotoPerfil(event){
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
        this.nuevaFotoPerfil=reader.result;
    }; 
  } 

  reestablecerFotoPerfil(){
    this.nuevaFotoPerfil = this.fotoDePerfilUsuario;
  }

  habilitarCambioPrimerNombre(){
    this.modificarPrimerNombre = !this.modificarPrimerNombre;
  }
  habilitarCambioSegundoNombre(){
    this.modificarSegundoNombre = !this.modificarSegundoNombre;
  }
  habilitarCambioPrimerApellido(){
    this.modificarPrimerApellido = !this.modificarPrimerApellido;
  }
  habilitarCambioSegundoApellido(){
    this.modificarSegundoApellido = !this.modificarSegundoApellido;
  }
  habilitarCambioFechaNacimiento(){
    this.modificarFechaNacimiento = !this.modificarFechaNacimiento;
  }
  habilitarCambioNacionalidad(){
    this.modificarNacionalidad = !this.modificarNacionalidad;
  }
  habilitarCambioClaveAcceso(){
    this.modificarClaveAcceso = !this.modificarClaveAcceso;
  }
   

  guardarCambio(){
    let fechaNacimiento = (document.getElementById("fechaNacimiento") as HTMLInputElement).value;
    let anioNacimientoTexto = fechaNacimiento.charAt(0) + fechaNacimiento.charAt(1) + fechaNacimiento.charAt(2) + fechaNacimiento.charAt(3);
    let anioNacimiento = +anioNacimientoTexto;
    let fechaActual = new Date().toISOString().slice(0, 10);
    let anioActualTexto = fechaActual.charAt(0) + fechaActual.charAt(1) + fechaActual.charAt(2) + fechaActual.charAt(3);
    let anioActual = +anioActualTexto;
    let edad = anioActual-anioNacimiento;
    var categoria : string;
    if(edad<15){
      categoria = "Junior";
    }
    else if(edad>15 && edad<23){
      categoria = "Sub-23";
    }
    else if(edad>23 && edad<30){
      categoria = "Open";
    }
    else if(edad>30 && edad<=40){
      categoria = "Master A";
    }
    else if(edad>=41 && edad<=50){
      categoria = "Master B";
    }
    else{
      categoria = "Master C";
    }

    let primerNombre = (document.getElementById("primerNombre") as HTMLInputElement).value;
    let primerApellido = (document.getElementById("primerApellido") as HTMLInputElement).value;
    let segundoApellido = (document.getElementById("segundoApellido") as HTMLInputElement).value;
    let nombreCategoria = categoria;
    let nacionalidad = (document.getElementById("nacionalidad") as HTMLInputElement).value;
    let nombreUsuario = this.nombreDeUsuario;
    let claveAcceso = (document.getElementById("claveAcceso") as HTMLInputElement).value;
    let fotoPerfil = this.nuevaFotoPerfil;

    let fotoPerfilEnviar = fotoPerfil.split(',')[1];


    //Modificar aqui que la foto de perfil que se manda pueda dejar de ser null
    this.modificarCuentaService.solicitarActualizarUsuario(primerNombre,primerApellido,segundoApellido,nombreUsuario,
                                                          claveAcceso,fechaNacimiento,nombreCategoria,nacionalidad,fotoPerfilEnviar)
    .subscribe(
      data => {},
      error => {
        if (error["status"]==200){
          if(confirm("Su cuenta ha sido actualizada.")){
            this.router.navigate(['']);
          }else{
            this.router.navigate(['']);
          }
        }
      }
    );
  }


  eliminarCuenta(){
    if(confirm("¡Atención, su cuenta será eliminada de forma definitiva! ¿Desea continuar?")==true){
      this.modificarCuentaService.solicitarEliminarUsuario(this.nombreDeUsuario).subscribe(
        data => {},
        error => {
          if(error["status"]==200){
            this.router.navigate(['']);
          }
        }
      );
    }
  }

  
  //Control de las rutas
  goToRegistroActividad(){
    this.router.navigate(['/registro-actividad']);
  }
  goToMiMuro(){
    this.router.navigate(['/mi-muro']);
  }
  goToBuscarCarrerasRetos(){
    this.router.navigate(['/buscar-carreras-retos']);
  }
  goToSeguirDeportistas(){
    this.router.navigate(['/seguir-deportistas']);
  }
  goToBuscarGrupos(){
    this.router.navigate(['/buscar-grupos']);
  }
  goToMisCarrerasRetos(){
    this.router.navigate(['/mis-carreras-retos'])
  }

}
