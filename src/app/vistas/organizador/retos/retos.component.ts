import { Component, OnInit } from '@angular/core';
import { Reto } from 'src/app/modelos/reto';
import { LogInService} from 'src/app/services/log-in.service';
import { RetoPatrocinador } from 'src/app/modelos/reto-patrocinador';
import { GrupoReto } from 'src/app/modelos/grupo-reto';


@Component({
  selector: 'app-retos',
  templateUrl: './retos.component.html',
  styleUrls: ['./retos.component.css']
})
export class RetosComponent implements OnInit {
retos: Reto[];
formVisibility: boolean;
form2Visibility: boolean;
elimina: boolean;
actividades = ["Nadar","Correr","Ciclismo","Senderismo","Kayak","Caminata"];
gruposCarrera = [];
patrocinadoresCarrera = [];
grupos = [
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
];

patrocinadores = [
 {
 "nombrecomercial": "Grupo INS",
 "logo": "https://acortar.link/gGMhs",
 "nombrerepresentante": "Róger Guillermo Arias Agüero",
 "numerotelrepresentante": "(+506)2287-6000"
 },
 {
 "nombrecomercial": "CoopeTarrazú",
 "logo": "https://acortar.link/jtm5s",
 "nombrerepresentante": "Yendry Leiva",
 "numerotelrepresentante": "(+506)2546-8615"
 },
 {
 "nombrecomercial": "KOLBI",
 "logo": "https://acortar.link/rI9vm",
 "nombrerepresentante": "Marjorie González Cascante",
 "numerotelrepresentante": "(+506)2255-1155"
 },
 {
 "nombrecomercial": "TDMAS",
 "logo": "https://acortar.link/L2LyQ",
 "nombrerepresentante": "Andres Nicolas",
 "numerotelrepresentante": "(+506)2232-2222"
 }
]
;
grupo: GrupoReto;
patrocinador: RetoPatrocinador;
reto = new Reto();

checkedListpatr =[];
checkedListgroup =[];
gruposReto = [];
patrocinadoresReto = [];
visi = false;
visibilidad: string
alt:string;
  constructor(private _logInService: LogInService) { }

  ngOnInit(): void {
    this.formVisibility = false;
  	this.retos = [
 {
 "nombre": "Reto 2",
 "admindeportista": "sam.astua",
 "fondoaltitud": "altitud",
 "tipoactividad": "Ciclismo",
 "periododisponibilidad": "2020-11-29",
 "privacidad": true,
 "kmtotales": 2.0,
 "descripcion": "Deberá completar un total de 2km ascendidos en bicicleta",
 "grupoReto": [
 {
 "nombrereto": "Reto 2",
 "adminreto": "sam.astua",
 "admingrupo": "cr7",
 "nombregrupo": "Los Bichos"
 },
 {
 "nombrereto": "Reto 2",
 "adminreto": "sam.astua",
 "admingrupo": "kevintrox",
 "nombregrupo": "Los Toros"
 }
 ],
 "retoPatrocinador": [
 {
 "nombrepatrocinador": "CoopeTarrazú",
 "nombrereto": "Reto 2",
 "admindeportista": "sam.astua"
 },
 {
 "nombrepatrocinador": "Grupo INS",
 "nombrereto": "Reto 2",
 "admindeportista": "sam.astua"
 }
 ]
 },
 {
 "nombre": "Reto 3",
 "admindeportista": "sam.astua",
 "fondoaltitud": "altitud",
 "tipoactividad": "Ciclismo",
 "periododisponibilidad": "2020-11-29",
 "privacidad": true,
 "kmtotales": 2.0,
 "descripcion": "Deberá completar un total de 2km ascendidos en bicicleta",
 "grupoReto": [
 {
 "nombrereto": "Reto 2",
 "adminreto": "sam.astua",
 "admingrupo": "cr7",
 "nombregrupo": "Los Bichos"
 },
 {
 "nombrereto": "Reto 2",
 "adminreto": "sam.astua",
 "admingrupo": "kevintrox",
 "nombregrupo": "Los Toros"
 }
 ],
 "retoPatrocinador": [
 {
 "nombrepatrocinador": "CoopeTarrazú",
 "nombrereto": "Reto 2",
 "admindeportista": "sam.astua"
 },
 {
 "nombrepatrocinador": "Grupo INS",
 "nombrereto": "Reto 2",
 "admindeportista": "sam.astua"
 }
 ]
 }
]
;
  }


agregar(){
  this.formVisibility = true;
  this.reto = new Reto();

}

submit(nombre, km,fecha2,alt,actividad,visibilidad,des){

this.reto = new Reto();
this.reto.nombre = nombre;
this.reto.admindeportista = this._logInService.getUsuario();
this.reto.fondoaltitud = this.alt;
this.reto.periododisponibilidad = fecha2;
this.reto.privacidad = this.visi;
this.reto.kmtotales = km;
this.reto.tipoactividad = actividad;
this.reto.descripcion = des;

 for(let i = 0 ; i < this.checkedListpatr.length; i++) {
      this.patrocinador = new RetoPatrocinador();
      this.patrocinador.nombrepatrocinador = this.checkedListpatr[i];
      this.patrocinador.nombrereto = nombre;
      this.patrocinador.admindeportista = this._logInService.getUsuario();
      this.patrocinadoresReto.push(this.patrocinador);
  }
  this.reto.retoPatrocinador = this.patrocinadoresReto;

  for(let i = 0 ; i < this.checkedListgroup.length; i++) {
      this.grupo = new GrupoReto();
      this.grupo.nombregrupo = this.checkedListgroup[i].nombre;
      this.grupo.admingrupo = this.checkedListgroup[i].admindeportista;
      this.grupo.nombrereto = nombre;
      this.grupo.adminreto = this._logInService.getUsuario();
      this.gruposReto.push(this.grupo);
  }
  this.reto.grupoReto = this.gruposReto;


 this.checkedListgroup = [];
 this.checkedListpatr = [];
 this.gruposReto = [];
 this.patrocinadoresReto = [];
 console.log(this.reto);
 this.retos.push(this.reto);
 this.formVisibility = false;

}

onCheckboxChangegroup(option, event) {
     if(event.target.checked) {
       this.checkedListgroup.push(option);
     } else {
     for(let i = 0 ; i < this.checkedListgroup.length; i++) {
       if(this.checkedListgroup[i] === option) {
         this.checkedListgroup.splice(i,1);
      }
    }
  }
 
}


onCheckboxChange2(option, event) {
     if(event.target.checked) {
       this.checkedListpatr.push(option);
     } else {
     for(let i = 0 ; i < this.checkedListpatr.length; i++) {
       if(this.checkedListpatr[i] === option) {
         this.checkedListpatr.splice(i,1);
      }
    }
  }
 
}


 actualiza(reto){
  this.form2Visibility = true;
  this.reto = reto;
}

modifica(nombre,km,fecha2,alt,actividad,visibilidad, des){
console.log("Modifica");

this.reto = new Reto();
this.reto.nombre = nombre;
this.reto.admindeportista = this._logInService.getUsuario();
this.reto.fondoaltitud = this.alt;
this.reto.periododisponibilidad = fecha2;
this.reto.privacidad = this.visi;
this.reto.kmtotales = km;
this.reto.tipoactividad = actividad;
this.reto.descripcion = des;

 for(let i = 0 ; i < this.checkedListpatr.length; i++) {
      this.patrocinador = new RetoPatrocinador();
      this.patrocinador.nombrepatrocinador = this.checkedListpatr[i];
      this.patrocinador.nombrereto = nombre;
      this.patrocinador.admindeportista = this._logInService.getUsuario();
      this.patrocinadoresReto.push(this.patrocinador);
  }
  this.reto.retoPatrocinador = this.patrocinadoresReto;

  for(let i = 0 ; i < this.checkedListgroup.length; i++) {
      this.grupo = new GrupoReto();
      this.grupo.nombregrupo = this.checkedListgroup[i].nombre;
      this.grupo.admingrupo = this.checkedListgroup[i].admindeportista;
      this.grupo.nombrereto = nombre;
      this.grupo.adminreto = this._logInService.getUsuario();
      this.gruposReto.push(this.grupo);
  }
  this.reto.grupoReto = this.gruposReto;


 this.checkedListgroup = [];
 this.checkedListpatr = [];
 this.gruposReto = [];
 this.patrocinadoresReto = [];
 console.log(this.reto);
 
  for(let i = 0 ; i < this.retos.length; i++) {
      if(this.retos[i].nombre === nombre){
        this.retos[i] = this.reto;
      }
  }

 this.form2Visibility = false;

}

radio(e){
     if(e.target.checked) {
        this.visi = false;
        this.visibilidad = "Publica";  
     } else {
          this.visi = true;  

}
}
radio2(e){
     if(e.target.checked) {
        this.visi = true;
        this.visibilidad = "Privada";    
     } else {
          this.visi = false;  

}
}

radio3(e){
     if(e.target.checked) {
      
        this.alt = "Altitud";  
     } 
}
radio4(e){
     if(e.target.checked) {
        
        this.alt = "Fondo";    
     } 
}

eliminar(id){
    const confirmed = window.confirm('¿Seguro que desea eliminar este reto?');
if (confirmed) {
this.elimina = false;


this.retos = this.retos.filter((i) => i !== id); // filtramos



}
}
}

