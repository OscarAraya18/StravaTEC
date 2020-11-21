import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import { environment } from 'src/environments/environment';
import { parseString } from 'xml2js';
import { MisCarrerasRetosService } from 'src/app/servicios/misCarrerasRetos.service'



export interface posicion {
  latitud: number;
  longitud: number;
}


@Component({
  selector: 'app-mis-carreras-retos',
  templateUrl: './mis-carreras-retos.component.html',
  styleUrls: ['./mis-carreras-retos.component.css']
})
export class MisCarrerasRetosComponent implements OnInit {
  constructor(private router: Router, private route: ActivatedRoute, private misCarrerasRetosService : MisCarrerasRetosService) { }


  icon = {
    url : 'https://res.cloudinary.com/dfionqbqe/image/upload/v1604793824/Icono.png',
    scaledSize:{
      width:20,
      height:20
    }
  }

  carrerasEncontradas = [];
  cantidadCarrerasEncontradas = [];
  rutas = [];
  tieneRuta = [];

  retosEncontrados = [];
  cantidadRetosEncontrados = [];

  cantidad = [];
  posiciones = [];

  fotoDePerfilUsuario="";
  nombreDelUsuario="";
  nombreDeUsuario="";
  


  ngOnInit(): void {
    this.nombreDelUsuario = environment.nombreDelUsuario;
    this.nombreDeUsuario = environment.nombreDeUsuario;
    this.fotoDePerfilUsuario = environment.fotoDePerfilUsuario;

    this.misCarrerasRetosService.solicitarMisCarreras(this.nombreDeUsuario).subscribe(
      data => {
        for(var carrera of data){
          console.log(carrera);
          let posibleCarrera = {
            nombre : carrera["nombreCarrera"],
            fecha : carrera["fecha"][8]+carrera["fecha"][9]+"-"+carrera["fecha"][5]+carrera["fecha"][6]+"-"+
                    carrera["fecha"][0]+carrera["fecha"][1]+carrera["fecha"][2]+carrera["fecha"][3],
            actividad : carrera["tipoActividad"],
            costo : carrera["costo"],
            cuentasBancarias : carrera["carreraCuentabancaria"],
            finalizada : carrera["finalizada"],
            tablaDePosiciones : carrera["actividades"],
            ruta : carrera["recorridoGPX"],
            patrocinadores : carrera["carreraPatrocinador"]
          };
          this.carrerasEncontradas.push(posibleCarrera);
        }
        this.mostrarCarrerasEncontradas();

        this.misCarrerasRetosService.solicitarMisRetos(this.nombreDeUsuario).subscribe(
          data => {
            for(var reto of data){
              let posibleReto = {
                nombre : reto["nombreReto"],
                diasRestantes : reto["diasFaltantes"],
                fechaDeFinalizacion : reto["fechaLimite"][8]+reto["fechaLimite"][9]+"-"+reto["fechaLimite"][5]+reto["fechaLimite"][6]+"-"+
                                      reto["fechaLimite"][0]+reto["fechaLimite"][1]+reto["fechaLimite"][2]+reto["fechaLimite"][3],
                objetivo : reto["kmTotales"] + " km",
                progresoActual : reto["kmAcumulados"] + " km",
                actividad : reto["tipoActividad"],
                fondoAltitud : reto["fondoAltitud"][0].toUpperCase()+reto["fondoAltitud"].slice(1,reto["fondoAltitud"].length),
                patrocinadores : reto["retoPatrocinador"]
              };
              if(reto["kmAcumulados"]>=reto["kmTotales"]){
                posibleReto.progresoActual = "Finalizado";
              }
              this.retosEncontrados.push(posibleReto);
            }
            this.mostrarRetosEncontrados();
          }
        );
      }
    );
  }

  //Funcionalidades
  mostrarCarrerasEncontradas(){
    for(var carreraEncontrada of this.carrerasEncontradas){
      this.cantidadCarrerasEncontradas.push(this.cantidadCarrerasEncontradas.length);
      if(carreraEncontrada.ruta==null){
        this.tieneRuta.push(false);
        this.rutas.push([]);
      }else{
        this.tieneRuta.push(true);
        this.mostrarMapasCarreras(carreraEncontrada.ruta);
      }
    }
  }

  mostrarRetosEncontrados(){
    for(var retoEncontrado of this.retosEncontrados){
      this.cantidadRetosEncontrados.push(this.cantidadRetosEncontrados.length);
    }
  }

  mostrarMapasCarreras(ruta : any){
    this.posiciones = [];
    let t = "";
    parseString(ruta, function (err,result) {
      t = result;
    });
    
    let i = 0;
    for(var contador of t["gpx"]["trk"]["0"]["trkseg"]["0"]["trkpt"]){
      i=i+1;
    }

    let innerVariable = 0;
    for(var j=0;j<i;j++){
      if(innerVariable==4 || innerVariable==0){
        console.log(t["gpx"]["trk"]["0"]["trkseg"]["0"]["trkpt"][j]["$"]);
        let posicion : posicion;
        posicion = {latitud: +t["gpx"]["trk"]["0"]["trkseg"]["0"]["trkpt"][j]["$"]["lat"],
                    longitud: +t["gpx"]["trk"]["0"]["trkseg"]["0"]["trkpt"][j]["$"]["lon"]};
        this.posiciones.push(posicion);
        innerVariable=0;
      }
    }
    this.rutas.push(this.posiciones);
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
  
  goToBuscarGrupos(){
    this.router.navigate(['/buscar-grupos']);
  }

  goToModificarCuenta(){
    this.router.navigate(['/modificar-cuenta']);
  }
}
