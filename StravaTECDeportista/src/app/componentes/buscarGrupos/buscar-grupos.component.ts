import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import { environment } from 'src/environments/environment';
import { BuscarGruposService, GrupoI } from 'src/app/servicios/buscarGrupos.service';


@Component({
  selector: 'app-buscar-grupos',
  templateUrl: './buscar-grupos.component.html',
  styleUrls: ['./buscar-grupos.component.css']
})
export class BuscarGruposComponent implements OnInit {

  //DATOS DE PRUEBA
  nombreDelUsuario = "";
  nombreDeUsuario = "";
  fotoDePerfilUsuario = "";

  gruposEncontrados = []
  cantidadGruposEncontrados = []

  constructor(private router: Router, private route: ActivatedRoute, private buscarGruposService : BuscarGruposService) { }

  ngOnInit(): void {
    this.nombreDelUsuario= environment.nombreDelUsuario;
    this.nombreDeUsuario= environment.nombreDeUsuario;
    this.fotoDePerfilUsuario= environment.fotoDePerfilUsuario;

    this.buscarGruposService.solicitarGruposNoInscrito(environment.nombreDeUsuario).subscribe(
      data =>{
        for(var grupo of data){
          let grupoEncontrado = {
            nombre : grupo["nombre"],
            administrador : grupo["admindeportista"],
            id : grupo["id"]
          };
          this.gruposEncontrados.push(grupoEncontrado);
        }
        for(var contador of this.gruposEncontrados){
          this.cantidadGruposEncontrados.push(this.cantidadGruposEncontrados.length);
        }
      }
    );
  }

  buscarGrupos(){
    let nombreGrupo = (document.getElementById("grupo") as HTMLInputElement).value; 
    this.buscarGruposService.solicitarGruposPorNombre(nombreGrupo).subscribe(
      data => {
        this.gruposEncontrados=[];
        this.cantidadGruposEncontrados=[];
        for(var grupo of data){
          let grupoEncontrado = {
            nombre : grupo["nombre"],
            administrador : grupo["admindeportista"],
            id : grupo["id"]
          };
          this.gruposEncontrados.push(grupoEncontrado);
        }
        for(var contador of this.gruposEncontrados){
          this.cantidadGruposEncontrados.push(this.cantidadGruposEncontrados.length);
        }
      }
    );
  }


  unirGrupo(index : number){
    let nombreGrupo = this.gruposEncontrados[index].nombre;
    let administradorGrupo = this.gruposEncontrados[index].administrador;
    let idGrupo = this.gruposEncontrados[index].id;

    this.buscarGruposService.solicitarUnirGrupo(idGrupo,administradorGrupo,this.nombreDeUsuario).subscribe(
      data => {},
      error => {
        if (error["status"]==200){
          if(confirm("¡@"+administradorGrupo + " le da la bienvenida a " + nombreGrupo + "!")){
            this.router.navigate(['/mi-muro']);
          }else{
            this.router.navigate(['/mi-muro']);
          }
        }
        console.log(error);
      }
    );
    
  }


  //CONTROL DE LAS RUTAS
  goToRegistrarActividad(){
    this.router.navigate(['/registro-actividad']);
  }

  goToSeguirDeportistas(){
    this.router.navigate(['/seguir-deportistas']);
  }

  goToMiMuro(){
    this.router.navigate(['/mi-muro']);
  }

  goToBuscarCarrerasRetos(){
    this.router.navigate(['/buscar-carreras-retos']);
  }
  
  goToMisCarrerasRetos(){
    this.router.navigate(['/mis-carreras-retos']);
  }

  goToModificarCuenta(){
    this.router.navigate(['/modificar-cuenta']);
  }


}
