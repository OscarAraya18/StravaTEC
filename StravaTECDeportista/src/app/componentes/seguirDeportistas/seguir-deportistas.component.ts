import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import { environment } from 'src/environments/environment';
import { SeguirDeportistasService } from 'src/app/servicios/seguirDeportistas.service';


@Component({
  selector: 'app-seguir-deportistas',
  templateUrl: './seguir-deportistas.component.html',
  styleUrls: ['./seguir-deportistas.component.css']
})


export class SeguirDeportistasComponent implements OnInit {
  placeHolderFotoPerfil = environment.placeHolderFotoPerfil;
  metaDataFoto = environment.metadaDataFoto;
  tieneFotoPerfil=[]
  nombreDelUsuario="";
  nombreDeUsuario="";
  fotoDePerfilUsuario="";
  deportistasEncontrados = [];
  fotosCorregidas = [];
  cantidadDeportistasEncontrados = [];

  constructor(private router: Router, private route: ActivatedRoute, private seguirDeportistasService : SeguirDeportistasService) {}

  ngOnInit(): void {
    this.nombreDelUsuario = environment.nombreDelUsuario;
    this.nombreDeUsuario = environment.nombreDeUsuario;
    this.fotoDePerfilUsuario = environment.fotoDePerfilUsuario;

    this.seguirDeportistasService.solicitarDeportistasNoSeguidos(this.nombreDeUsuario).subscribe(
      data => {
        for(var deportista of data){
          let deportistaEncontrado = {
            nombre : deportista["nombre"] + " " +  deportista["apellido1"],
            nombreUsuario : deportista["usuario"],
            fotoPerfil : deportista["foto"]
          };
          this.deportistasEncontrados.push(deportistaEncontrado);
        }
        for(var contador of this.deportistasEncontrados){
          this.cantidadDeportistasEncontrados.push(this.cantidadDeportistasEncontrados.length);
          if(contador.fotoPerfil!=null){
            this.tieneFotoPerfil.push(true);
            this.fotosCorregidas.push(this.metaDataFoto+contador.fotoPerfil);
          }else{
            this.tieneFotoPerfil.push(false);
            this.fotosCorregidas.push("");
          }
        }
      }
    );
  }


  buscarDeportista(){
    let nombreDeportista = (document.getElementById("deportista") as HTMLInputElement).value;
    this.seguirDeportistasService.solicitarDeportistasPorNombre(nombreDeportista,this.nombreDeUsuario).subscribe(
      data => {
        this.deportistasEncontrados=[];
        this.cantidadDeportistasEncontrados=[];
        this.fotosCorregidas=[];
        this.tieneFotoPerfil=[];
        for(var deportista of data){
          let deportistaEncontrado = {
            nombre : deportista["nombre"] + " " +  deportista["apellido1"],
            nombreUsuario : deportista["usuario"],
            fotoPerfil : deportista["foto"]
          };
          this.deportistasEncontrados.push(deportistaEncontrado);
        }
        for(var contador of this.deportistasEncontrados){
          this.cantidadDeportistasEncontrados.push(this.cantidadDeportistasEncontrados.length);
          if(contador.fotoPerfil!=null){
            this.tieneFotoPerfil.push(true);
            this.fotosCorregidas.push(this.metaDataFoto+contador.fotoPerfil);
          }else{
            this.tieneFotoPerfil.push(false);
            this.fotosCorregidas.push("");
          }
        }
      }
    )
  }

  seguirDeportista(nombreUsuarioSeguir:string){
    this.seguirDeportistasService.solicitarSeguirDeportista(nombreUsuarioSeguir,this.nombreDeUsuario).subscribe(
      data => {},
      error => {
        if(error["status"]=="200"){
          if(confirm("¡@"+nombreUsuarioSeguir + " y tu ahora son amigos!")){
            this.router.navigate(['/mi-muro']);
          }else{
            this.router.navigate(['/mi-muro']);
          }
        }
      }
    )

  }


  //Control de las rutas
  goToRegistrarActividad(){
    this.router.navigate(['/registro-actividad']);
  }
  goToMiMuro(){
    this.router.navigate(['/mi-muro']);
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
