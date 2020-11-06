import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-inscripciones',
  templateUrl: './inscripciones.component.html',
  styleUrls: ['./inscripciones.component.css']
})
export class InscripcionesComponent implements OnInit {

inscripciones: any[];

formVisibility: boolean;
form2Visibility: boolean;

  constructor() { }

  ngOnInit(): void {
    this.formVisibility = false;
  	this.inscripciones = [
    {
        "id": "147852369",
        "reciboPago": "0000000154",
        "estado": ""
    },
    {
        "id": "102364789",
        "reciboPago": "0000001541",
        "estado": ""
    },
    {
        "id": "01258963",
        "reciboPago": "000002154",
        "estado": ""
    },
    {
        "id": "856932147",
        "reciboPago": "0000020154",
        "estado": ""
    },
    {
        "id": "15896345",
        "reciboPago": "0000550154",
        "estado": ""
    }
   

];
  }




 actualiza(id,recibo){
  console.log(id);
  console.log(recibo);
  console.log("Aceptado");
  this.inscripciones = this.inscripciones.filter((i) => i.id !== id); // filtramos
}





eliminar(id,recibo){
    const confirmed = window.confirm('¿Seguro que desea denegar esta inscripción?');
if (confirmed) {
console.log(id);
  console.log(recibo);
  console.log("Rechazado");

this.inscripciones = this.inscripciones.filter((i) => i.id !== id); // filtramos



}
}
}

