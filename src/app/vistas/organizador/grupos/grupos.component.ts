import { Component, OnInit } from '@angular/core';
import { LogInService} from 'src/app/services/log-in.service';
import { Grupo } from 'src/app/modelos/grupo';

@Component({
  selector: 'app-grupos',
  templateUrl: './grupos.component.html',
  styleUrls: ['./grupos.component.css']
})
export class GruposComponent implements OnInit {
grupos: Grupo[];
formVisibility: boolean;
form2Visibility: boolean;
elimina: boolean;
grupo = new Grupo();

  constructor(private _logInService: LogInService) { }

  ngOnInit(): void {
    this.formVisibility = false;
  	this.grupos = [
 {
 "nombre": "Las Estrellas",
 "admindeportista": "sam.astua",
 },
 {
 "nombre": "Los Toros",
 "admindeportista": "kevintrox"
 },
 {
 "nombre": "Los Bichos",
 "admindeportista": "cr7"
 },
 {
 "nombre": "Los Físicos",
 "admindeportista": "crespo"
 }
]
;
  }


agregar(){
  this.formVisibility = true;
  this.grupo = new Grupo();

}

submit(nombre){
this.grupo = new Grupo();
this.grupo.nombre = nombre;
this.grupo.admindeportista = this._logInService.getUsuario();
console.log(this.grupo);
this.grupos.push(this.grupo);
 this.formVisibility = false;

}



 actualiza(grupo){
  this.grupo = grupo;
  this.form2Visibility = true;
}

modifica(nombre){
  console.log("Modifica");
this.grupo = new Grupo();
this.grupo.nombre = nombre;
this.grupo.admindeportista = this._logInService.getUsuario();
console.log(this.grupo);
for(let i = 0 ; i < this.grupos.length; i++) {
      if(this.grupos[i].nombre === nombre){
        this.grupos[i].nombre = this.grupo.nombre;
      }
 this.form2Visibility = false;

}
}


eliminar(id){
    const confirmed = window.confirm('¿Seguro que desea eliminar este grupo?');
if (confirmed) {
this.elimina = false;


this.grupos = this.grupos.filter((i) => i !== id); // filtramos



}
}
}

