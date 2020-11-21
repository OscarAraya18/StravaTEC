import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import { browser, error } from 'protractor';
import { RegistroDeportistaService } from 'src/app/servicios/registroDeportista.service';

@Component({
  selector: 'app-registro-deportista',
  templateUrl: './registro-deportista.component.html',
  styleUrls: ['./registro-deportista.component.css'
  ]
})

export class RegistroDeportistaComponent implements OnInit {
  fotoPerfil : any = null;

  constructor(private router: Router, private route: ActivatedRoute, private registroDeportistaService : RegistroDeportistaService) {}
  ngOnInit(): void {}

  subidaFoto(event) {
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
        this.fotoPerfil=reader.result;
    };
  } 

  registrarDeportista(){
    let faltaDato = false;

    let primerNombre = (document.getElementById("primerNombre") as HTMLInputElement).value;
    let segundoNombre = (document.getElementById("segundoNombre") as HTMLInputElement).value;
    let primerApellido = (document.getElementById("primerApellido") as HTMLInputElement).value;
    let segundoApellido = (document.getElementById("segundoApellido") as HTMLInputElement).value;
    let fechaDeNacimiento = (document.getElementById("fechaDeNacimiento") as HTMLInputElement).value;
    let nacionalidad = (document.getElementById("nacionalidad") as HTMLInputElement).value;
    let nombreDeUsuario = (document.getElementById("nombreDeUsuario") as HTMLInputElement).value;
    let claveDeAcceso = (document.getElementById("claveDeAcceso") as HTMLInputElement).value;

    
    if(primerNombre==""){
      alert("Debe colocar su primer nombre. Intente nuevamente.");
      faltaDato = true;
    }else if(segundoNombre==""){
      alert("Debe colocar su segundo nombre. Intente nuevamente.");
      faltaDato = true;
    }else if(primerApellido==""){
      alert("Debe colocar su primer apellido. Intente nuevamente.");
      faltaDato = true;
    }else if(segundoApellido==""){
      alert("Debe colocar su segundo apellido. Intente nuevamente.");
      faltaDato = true;
    }else if(fechaDeNacimiento==""){
      alert("Debe colocar su fecha de nacimiento. Intente nuevamente.");
      faltaDato = true;
    }else if(nacionalidad=="Seleccione una nacionalidad"){
      alert("Debe colocar su nacionalidad. Intente nuevamente.");
      faltaDato = true;
    }else if(nombreDeUsuario==""){
      alert("Debe colocar su nombre de usuario. Intente nuevamente.");  
      faltaDato = true;  
    }else if(claveDeAcceso==""){
      alert("Debe colocar su nombre de usuario. Intente nuevamente.");  
      faltaDato = true; 
    }

    if(!faltaDato){
      let anioNacimientoTexto = fechaDeNacimiento.charAt(0) + fechaDeNacimiento.charAt(1) + fechaDeNacimiento.charAt(2) + fechaDeNacimiento.charAt(3);
      let anioNacimiento = +anioNacimientoTexto;
      let fechaActual = new Date().toISOString().slice(0, 10)
      let anioActualTexto = fechaActual.charAt(0) + fechaActual.charAt(1) + fechaActual.charAt(2) + fechaActual.charAt(3);
      let anioActual = +anioActualTexto
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
        categoria = "Master C"
      }

      let fotoPerfilEnviar = this.fotoPerfil.split(',')[1];
      
      this.registroDeportistaService.verificarNombreUsuario(nombreDeUsuario).subscribe(
        data => {
          alert("El nombre de usuario no está disponible. Intente nuevamente.");},
        error => {

          this.registroDeportistaService.crearDeportista(primerNombre,primerApellido,segundoApellido,
                                                          nombreDeUsuario,claveDeAcceso,fechaDeNacimiento,
                                                          categoria,nacionalidad,fotoPerfilEnviar)
          .subscribe(
            data =>{},
            error => {
              if (error["status"]==200){
                if(confirm("Su cuenta ha sido creada, ¡Bienvenido a Strava!")){
                  this.router.navigate(['']);
                }else{
                  this.router.navigate(['']);
                }
              }
            }
          );
        }
      );
    }
  }
}
