import { analyzeAndValidateNgModules } from '@angular/compiler';
import { BindingFlags } from '@angular/compiler/src/core';
import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import { environment } from 'src/environments/environment';
import { RegistrarActividadService } from 'src/app/servicios/registrarActividad.service';

@Component({
  selector: 'app-registro-actividad',
  templateUrl: './registro-actividad.component.html',
  styleUrls: ['./registro-actividad.component.css']
})

export class RegistroActividadComponent implements OnInit {
  constructor(private router: Router, private route: ActivatedRoute, private registrarActividadService : RegistrarActividadService){}

  ruta : any = null;
  nombreRuta = "Seleccione una ruta";
  noEsActividadLibre = true;
  noEsCarrera = true;
  noEsReto = true;
  nombreDelUsuario= "";
  nombreDeUsuario= "";
  fotoDePerfilUsuario="";

  posiblesCarreras = [];
  posiblesRetos = [];

  ngOnInit(): void {
    this.nombreDelUsuario= environment.nombreDelUsuario;
    this.nombreDeUsuario= environment.nombreDeUsuario;
    this.fotoDePerfilUsuario= environment.fotoDePerfilUsuario;

    this.registrarActividadService.solicitarPosiblesRetos(this.nombreDeUsuario).subscribe(
      data => {
        for(var reto of data){
          let retoPosible = {
            nombre : reto["nombreReto"],
            administrador: reto["adminReto"],
            tipoActividad: reto["tipoActividad"]
          };
          this.posiblesRetos.push(retoPosible);
        }

        this.registrarActividadService.solicitarPosiblesCarreras(this.nombreDeUsuario).subscribe(
          data => {
            for(var carrera of data){
              let carreraPosible = {
                nombre : carrera["nombreCarrera"],
                administrador : carrera["adminDeportista"],
                tipoActividad: carrera["tipoActividad"]
              };
              this.posiblesCarreras.push(carreraPosible);
            }
          }
        );
      }
    );
  } 

  habilitarEsCarrera(){
    this.noEsCarrera = !this.noEsCarrera;
    this.noEsReto = true;
    this.noEsActividadLibre = true;
  }
  habilitarEsReto(){
    this.noEsReto = !this.noEsReto;
    this.noEsCarrera = true;
    this.noEsActividadLibre = true;
  }
  habilitarEsActividadLibre(){
    this.noEsActividadLibre = !this.noEsActividadLibre;
    this.noEsCarrera = true;
    this.noEsReto = true;
  }
  subidaRuta(event){
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.readAsText(file);
    reader.onload = () => {
        this.ruta=reader.result;
        this.nombreRuta=file.name;
    };
  }


  registrarActividad(){
    let carreraRetoRealizado = ""
    let administrador = "";
    let tipoActividad = "";
    let actividadRealizada = "Carrera o reto";
    let flag = 0;
    if(!this.noEsCarrera){
      flag = 0;
      carreraRetoRealizado = (document.getElementById("carreraRealizada") as HTMLInputElement).value;
      if (carreraRetoRealizado!="Seleccione una carrera"){
        for(var carrera of this.posiblesCarreras){
          if(carrera.nombre==carreraRetoRealizado){
            administrador = carrera.administrador;
            tipoActividad = carrera.tipoActividad;
            carreraRetoRealizado = carrera.nombre;
          }
        }
      }
    }else if(!this.noEsReto){
      flag = 1;
      carreraRetoRealizado = (document.getElementById("retoRealizado") as HTMLInputElement).value;
      if (carreraRetoRealizado!="Seleccione un reto"){
        for(var reto of this.posiblesRetos){
          if(reto.nombre==carreraRetoRealizado){
            administrador = reto.administrador;
            tipoActividad = reto.tipoActividad;
            carreraRetoRealizado = reto.nombre;
          }
        }
      }
    }else{
      flag = 2;
      carreraRetoRealizado = "Actividad Libre";
      actividadRealizada = (document.getElementById("actividadRealizada") as HTMLInputElement).value;
      tipoActividad = "Actividad Libre";
      carreraRetoRealizado = "Actividad Libre";
    }

    let fecha = (document.getElementById("fecha") as HTMLInputElement).value;
    let horaInicio = (document.getElementById("horaInicio") as HTMLInputElement).value;
    let distanciaRecorrida = +(document.getElementById("distanciaRecorrida") as HTMLInputElement).value;
    let duracion = (document.getElementById("duracion") as HTMLInputElement).value;
    
    this.registrarActividadService.solicitarRegistrarActividad(this.nombreDeUsuario, actividadRealizada,fecha,
                                                                horaInicio,duracion,distanciaRecorrida,tipoActividad,
                                                                this.ruta, carreraRetoRealizado, administrador, flag)
    .subscribe(
      data => {},
      error => {
        if(error["status"]==200){
          if(confirm("Su actividad ha sido registrada")){
            this.goToMiMuro();
          }else{
            this.goToMiMuro();
          }
        }
      }
    )
  }



  goToMiMuro(){
    this.router.navigate(['/mi-muro']);
  }
  goToSeguirDeportistas(){
    this.router.navigate(['/seguir-deportistas'])
  }
  goToBuscarCarrerasRetos(){
    this.router.navigate(['/buscar-carreras-retos']);
  }
  goToBuscarGrupos(){
    this.router.navigate(['/buscar-grupos']);
  }
  goToMisCarrerasRetos(){
    this.router.navigate(['/mis-carreras-retos']);
  }
  goToModificarCuenta(){
    this.router.navigate(['/modificar-cuenta']);
  }


  


}
