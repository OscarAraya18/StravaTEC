import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-reportes',
  templateUrl: './reportes.component.html',
  styleUrls: ['./reportes.component.css']
})
export class ReportesComponent implements OnInit {

 carreras: any[];

formVisibility: boolean;
form2Visibility: boolean;
elimina: boolean;

  constructor() { }

  ngOnInit(): void {
    this.formVisibility = false;
  	this.carreras = [
    {
        "nombre": "Carrera 1",
        "fecha": "20/12/2020",
        "tipoActividad": "Correr",
        "visibilidad": "Privada",
        "costo": "20000",
        "cuentasBancarias": "CR23015108410026012345 wesrdctfyguh",
        "categorias": "Junior",
        "patrocinadores": "Coca Cola"
    },
    {
        "nombre": "Carrera 2",
        "fecha": "20/12/2020",
        "tipoActividad": "Nadar",
        "visibilidad": "Privada",
        "costo": "20000",
        "cuentasBancarias": "CR23015108410026012345",
        "categorias": "Junior",
        "patrocinadores": "Coca Cola"
    },
    {
        "nombre": "Carrera 3",
        "fecha": "20/12/2020",
        "tipoActividad": "Ciclismo",
        "visibilidad": "Privada",
        "costo": "20000",
        "cuentasBancarias": "CR23015108410026012345",
        "categorias": "Junior",
        "patrocinadores": "Coca Cola"
    },
    {
        "nombre": "Carrera 4",
        "fecha": "20/12/2020",
        "tipoActividad": "Senderismo",
        "visibilidad": "Privada",
        "costo": "20000",
        "cuentasBancarias": "CR23015108410026012345",
        "categorias": "Junior",
        "patrocinadores": "Coca Cola"
    }

];
  }


agregar(){
  this.formVisibility = true;

}

submit(nombre,fecha,costo,cuentas,actividad,visibilidad){


}



 actualiza(){
  this.form2Visibility = true;
}

modifica(nombre,fecha,costo,cuentas,actividad,visibilidad){



}



eliminar(id){
    const confirmed = window.confirm('¿Seguro que desea eliminar esta carrera?');
if (confirmed) {
this.elimina = false;


this.carreras = this.carreras.filter((i) => i.nombre !== id); // filtramos



}
}
}

