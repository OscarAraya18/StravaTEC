import { Component, OnInit} from '@angular/core';
import { parseString } from 'xml2js';
import {ActivatedRoute, Router} from "@angular/router";
import { environment } from 'src/environments/environment';
import { MiMuroService } from 'src/app/servicios/miMuro.service';


export interface posicion {
  latitud: number;
  longitud: number;
}

@Component({
  selector: 'app-mi-muro',
  templateUrl: './mi-muro.component.html',
  styleUrls: ['./mi-muro.component.css'
  ]
})

export class MiMuroComponent implements OnInit {
  constructor(private router: Router, private route: ActivatedRoute, private miMuroService : MiMuroService) {}

  metadaDataFoto = environment.metadaDataFoto;
  placeHolderFotoPerfil = environment.placeHolderFotoPerfil;
  nombreDelUsuario="";
  nombreDeUsuario="";
  fotoDePerfilUsuario="";

  tieneRuta = [];
  tieneFotoPerfil = [];
  rutas = [];
  
  informacionFichasActividad=[];
  cantidadFichasActividad= [];

  cantidad = [];
  posiciones = [];


  icon = {
    url : 'https://res.cloudinary.com/dfionqbqe/image/upload/v1604793824/Icono.png',
    scaledSize:{
      width:20,
      height:20
    }
  }


  ngOnInit(){
    this.nombreDelUsuario = environment.nombreDelUsuario;
    this.nombreDeUsuario = environment.nombreDeUsuario;
    this.fotoDePerfilUsuario = environment.fotoDePerfilUsuario;


    this.miMuroService.solicitarActividades(this.nombreDeUsuario).subscribe(
      data => {
        for(var actividad of data){
          console.log(actividad);
          let actividadRegistrar = {
            nombre : actividad["usuariodeportistaNavigation"]["nombre"] + " " + actividad["usuariodeportistaNavigation"]["apellido1"],
            fotoPerfil : actividad["usuariodeportistaNavigation"]["foto"],
            nombreDeUsuario : actividad["usuariodeportista"],
            fecha : actividad["fechahora"][8]+actividad["fechahora"][9]+"-"+actividad["fechahora"][5]+actividad["fechahora"][6]+"-"+ 
                    actividad["fechahora"][0]+actividad["fechahora"][1]+actividad["fechahora"][2]+actividad["fechahora"][3],
            hora : actividad["fechahora"][11]+actividad["fechahora"][12]+":"+actividad["fechahora"][14]+actividad["fechahora"][15]+
                  ":"+actividad["fechahora"][17]+actividad["fechahora"][18],
            nombreCompeticionReto : actividad["nombreretocarrera"],
            actividadRealizada : actividad["tipoactividad"],
            distanciaRecorrida : actividad["kilometraje"] + " km",
            duracion : actividad["duracion"],
            ruta : actividad["recorridogpx"]
          }
          this.informacionFichasActividad.push(actividadRegistrar);
        }

        for(var deportista of this.informacionFichasActividad){
          if (deportista.fotoPerfil!=null){
            this.tieneFotoPerfil.push(true);
          }else{
            this.tieneFotoPerfil.push(false);
          }
          if (deportista.ruta!=null){
            this.tieneRuta.push(true);
            this.dibujarMapas(deportista.ruta);
          }else{
            this.tieneRuta.push(false);
            this.rutas.push([]);
          } 
          this.cantidadFichasActividad.push(this.cantidadFichasActividad.length);
        }
      }
    );
  }


  dibujarMapas(rutaTexto : any){
    this.posiciones = [];
    let t = "";
    parseString(rutaTexto, function (err,result) {
      t = result;
    });
    
    let i = 0;
    for(var contador of t["gpx"]["trk"]["0"]["trkseg"]["0"]["trkpt"]){
      i=i+1;
    }

    let innerVariable = 0;
    for(var j=0;j<i;j++){
      if(innerVariable==4 || innerVariable==0){
        let posicion : posicion;
        posicion = {latitud: +t["gpx"]["trk"]["0"]["trkseg"]["0"]["trkpt"][j]["$"]["lat"],
                    longitud: +t["gpx"]["trk"]["0"]["trkseg"]["0"]["trkpt"][j]["$"]["lon"]};
        this.posiciones.push(posicion);
        innerVariable=0;
      }
    }
    this.rutas.push(this.posiciones);
  }


  //Manejo de rutas
  goToRegistroActividad(){
    this.router.navigate(['/registro-actividad']);
  }

  goToSeguirDeportistas(){
    this.router.navigate(['/seguir-deportistas']);
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

