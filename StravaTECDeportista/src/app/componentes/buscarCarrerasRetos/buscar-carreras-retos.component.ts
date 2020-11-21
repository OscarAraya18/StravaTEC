import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { environment } from 'src/environments/environment';
import { parseString } from 'xml2js';
import { BuscarCarrerasRetosService } from 'src/app/servicios/buscarCarrerasRetos.service';

export interface posicion {
  latitud: number;
  longitud: number;
}

@Component({
  selector: 'app-buscar-carreras-retos',
  templateUrl: './buscar-carreras-retos.component.html',
  styleUrls: ['./buscar-carreras-retos.component.css']
})



export class BuscarCarrerasRetosComponent implements OnInit {
  constructor(private router: Router, private route: ActivatedRoute, private buscarCarrerasRetosService: BuscarCarrerasRetosService) { }


  nombreDelUsuario = "";
  nombreDeUsuario = "";
  fotoDePerfilUsuario = "";
  metaDataFoto = environment.metadaDataFoto;


  rutasCarreras = [];
  tieneRuta = [];
  posiciones = [];

  icon = {
    url : 'https://res.cloudinary.com/dfionqbqe/image/upload/v1604793824/Icono.png',
    scaledSize:{
      width:20,
      height:20
    }

  };

  carrerasEncontradas = [];
  cantidadCarrerasEncontradas = [];

  listaRecibos = [];
  recibosSubidos = [];

  retosEncontrados = [];
  cantidadRetosEncontrados = [];


  ngOnInit(): void {
    this.nombreDelUsuario = environment.nombreDelUsuario;
    this.nombreDeUsuario = environment.nombreDeUsuario;
    this.fotoDePerfilUsuario = environment.fotoDePerfilUsuario;

    this.buscarCarrerasRetosService.solicitarPosiblesCarreras(this.nombreDeUsuario).subscribe(
      data => {
        for(var carrera of data){
          let carreraPosible = {
            nombre : carrera["nombreCarrera"],
            fecha : carrera["fecha"][8]+carrera["fecha"][9]+"-"+carrera["fecha"][5]+carrera["fecha"][6]+"-"+
                    carrera["fecha"][0]+carrera["fecha"][1]+carrera["fecha"][2]+carrera["fecha"][3],
            actividad : carrera["tipoActividad"],
            costo : carrera["costo"],
            cuentasBancarias : carrera["carreraCuentabancaria"],
            administrador : carrera["adminDeportista"],
            patrocinadores :  carrera["carreraPatrocinador"],
            finalizada : carrera["finalizada"],
            tablaDePosiciones : carrera["tablaPosiciones"],
            ruta : carrera["recorridoGPX"] 
          };
          this.carrerasEncontradas.push(carreraPosible);
        }

        this.mostrarCarrerasEncontradas();

      }
    );
    this.buscarCarrerasRetosService.solicitarPosiblesRetos(this.nombreDeUsuario).subscribe(
      data => {
        for(var reto of data){
          let retoPosible = {
            nombre : reto["nombreReto"],
            diasRestantes : reto["diasFaltantes"],
            fechaDeFinalizacion : reto["fechaLimite"][8]+reto["fechaLimite"][9]+"-"+reto["fechaLimite"][5]+
                                  reto["fechaLimite"][6]+"-"+reto["fechaLimite"][0]+reto["fechaLimite"][1]+
                                  reto["fechaLimite"][2]+reto["fechaLimite"][3],
            objetivo: reto["kmTotales"] + " km",
            progresoActual : reto["kmAcumulados"] + " km",
            actividad : reto["descripcion"],
            fondoAltitud : reto["fondoAltitud"][0].toUpperCase()+reto["fondoAltitud"].slice(1,reto["fondoAltitud"].length),
            administrador : reto["adminReto"],
            patrocinadores : reto["retoPatrocinador"]
          };
          this.retosEncontrados.push(retoPosible);
        }
        this.mostrarRetosEncontrados();
      }
    );
  }
    
  
  mostrarCarrerasEncontradas(){
    for(var carreraEncontrada of this.carrerasEncontradas){
      this.cantidadCarrerasEncontradas.push(this.cantidadCarrerasEncontradas.length);
      this.listaRecibos.push("Subir Factura");
      this.recibosSubidos.push("");
      if(carreraEncontrada.ruta == null){
        this.tieneRuta.push(false);
        this.rutasCarreras.push([]);
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
        let posicion : posicion;
        posicion = {latitud: +t["gpx"]["trk"]["0"]["trkseg"]["0"]["trkpt"][j]["$"]["lat"],
                    longitud: +t["gpx"]["trk"]["0"]["trkseg"]["0"]["trkpt"][j]["$"]["lon"]};
        this.posiciones.push(posicion);
        innerVariable=0;
      }
    }
    this.rutasCarreras.push(this.posiciones);
  }

  subidaFactura(event, index : number){
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
        this.recibosSubidos[index] = reader.result;
        this.listaRecibos[index]=file.name;
    };
  }

  inscribirCarrera(index : number){
    let nombreCarrera = this.carrerasEncontradas[index].nombre;
    let administradorCarrera = this.carrerasEncontradas[index].administrador;
    let reciboPago = this.recibosSubidos[index];

    let reciboEnviar = reciboPago.split(',')[1];

    this.buscarCarrerasRetosService.solicitarInscribirCarrera(this.nombreDeUsuario, nombreCarrera, administradorCarrera, reciboEnviar)
    .subscribe(
      data => {},
      error => {
        if(error["status"]==200){
          if(confirm("Se ha enviado su solicitud de inscripción para la carrera " + nombreCarrera + ". El equipo de administración de StravaTEC le contestará pronto.")){
            this.goToMiMuro();
          }else{
            this.goToMiMuro();
          }
        }else{
          alert("Su solicitud para esta carrera se encuentra siendo procesada. Por favor espere a la respuesta de la administración de StravaTEC.");
        }
      }
    );
  }

  inscribirReto(index : number){
    let nombreReto = this.retosEncontrados[index].nombre;
    let administradorReto = this.retosEncontrados[index].administrador;
  
    this.buscarCarrerasRetosService.solicitarParticiparReto(this.nombreDeUsuario, nombreReto, administradorReto).subscribe(
      data => {},
      error => {
        console.log(error);
        if(error["status"]==200){
          if(confirm("@" + administradorReto + " le da la bienvenida al reto " + nombreReto + ".")){
            this.goToMiMuro();
          }else{
            this.goToMiMuro();
          }
        }
      }
    );
  }




  //Manejo de rutas
  goToRegistrarActividad(){
    this.router.navigate(['/registro-actividad']);
  }
  goToSeguirDeportistas(){
    this.router.navigate(['/seguir-deportistas']);
  }
  goToMiMuro(){
    this.router.navigate(['/mi-muro']);
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
