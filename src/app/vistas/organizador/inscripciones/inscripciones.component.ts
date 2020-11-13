import { Component, OnInit } from '@angular/core';
import { Inscripcion } from 'src/app/modelos/inscripcion';
import { LogInService} from 'src/app/services/log-in.service';

@Component({
  selector: 'app-inscripciones',
  templateUrl: './inscripciones.component.html',
  styleUrls: ['./inscripciones.component.css']
})
export class InscripcionesComponent implements OnInit {

inscripciones: Inscripcion[];
inscripcion: Inscripcion;

formVisibility: boolean;
form2Visibility: boolean;

  constructor() { }

  ngOnInit(): void {
    this.formVisibility = false;
  	this.inscripciones = [
 {
 "usuariodeportista": "cj",
 "estado": "En espera",
 "recibopago": null,
 "nombrecarrera": "The Best",
 "admincarrera": "cr7"
 },
 {
 "usuariodeportista": "cj2",
 "estado": "En espera",
 "recibopago": null,
 "nombrecarrera": "The Best",
 "admincarrera": "cr7"
 },
 {
 "usuariodeportista": "cj3",
 "estado": "En espera",
 "recibopago": null,
 "nombrecarrera": "The Best",
 "admincarrera": "cr7"
 }
]
;
  }




 actualiza(id){
  this.inscripcion = id;
  console.log(this.inscripcion);
  console.log("Aceptado");
  this.inscripciones = this.inscripciones.filter((i) => i !== id); // filtramos
}





eliminar(id){
  this.inscripcion = id;
    const confirmed = window.confirm('¿Seguro que desea denegar esta inscripción?');
if (confirmed) {
console.log(this.inscripcion);
  console.log("Rechazado");

this.inscripciones = this.inscripciones.filter((i) => i !== id); // filtramos



}
}
}

