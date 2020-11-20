import { Component, OnInit } from '@angular/core';
import { Inscripcion } from 'src/app/modelos/inscripcion';
import { LogInService} from 'src/app/services/log-in.service';
import { CarreraService} from 'src/app/services/carrera.service';
import { Carrera } from 'src/app/modelos/carrera';
import { InscrpcionService} from 'src/app/services/inscrpcion.service';


@Component({
  selector: 'app-inscripciones',
  templateUrl: './inscripciones.component.html',
  styleUrls: ['./inscripciones.component.css']
})
export class InscripcionesComponent implements OnInit {

inscripciones: Inscripcion[];
inscripcion: Inscripcion;
carreras: Carrera[];
formVisibility: boolean;
form2Visibility: boolean;

  constructor(private _logInService: LogInService,
    private _CarreraService: CarreraService,  private _InscripcionService: InscrpcionService) { }

  ngOnInit(): void {
    this.formVisibility = false;
    this._CarreraService.getCarreras().subscribe(data => {this.carreras = data; });
  }

ver(nombre){
  this._InscripcionService.getInscripciones(nombre).subscribe(data => {this.inscripciones = data; });

    this.formVisibility = true;

}
nover(){

    this.formVisibility = false;

}


 actualiza(id){
  this.inscripcion = id;
  console.log("Aceptado");
  console.log(this.inscripcion);
  this._InscripcionService.aceptarInscripcion(id).subscribe(data => {});
  this.inscripciones = this.inscripciones.filter((i) => i !== id); // filtramos
}





eliminar(id){
  this.inscripcion = id;
    const confirmed = window.confirm('¿Seguro que desea denegar esta inscripción?');
if (confirmed) {

this._InscripcionService.borraInscripcion(id).subscribe(data => {});
this.inscripciones = this.inscripciones.filter((i) => i !== id); // filtramos
console.log(this.inscripcion);
  console.log("Rechazado");



}
}
}

