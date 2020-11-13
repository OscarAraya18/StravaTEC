import { Component, OnInit } from '@angular/core';
import { Carrera } from 'src/app/modelos/carrera';
import { CarreraCategoria } from 'src/app/modelos/carrera-categoria';
import { CarreraCuentaBancaria } from 'src/app/modelos/carrera-cuenta-bancaria';
import { CarreraPatrocinador } from 'src/app/modelos/carrera-patrocinador';
import { GrupoCarrera } from 'src/app/modelos/grupo-carrera';
import { LogInService} from 'src/app/services/log-in.service';

@Component({
  selector: 'app-carreras',
  templateUrl: './carreras.component.html',
  styleUrls: ['./carreras.component.css']
})
export class CarrerasComponent implements OnInit {
carreras: Carrera[];
carrera = new Carrera();
categoria: CarreraCategoria;
grupo: GrupoCarrera;
patrocinador: CarreraPatrocinador;
cuenta: CarreraCuentaBancaria;


formVisibility: boolean;
form2Visibility: boolean;
elimina: boolean;
visibilidad: string;
actividades = ["Nadar","Correr","Ciclismo","Senderismo","Kayak","Caminata"];
categorias = ["Junior","Sub-23","Elite","Master-A","Master-B","Master-C"];
patrocinadores =[
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

checkedListcat = [];
checkedListpatr = [];
checkedListgroup = [];
categoriasCarrera = [];
gruposCarrera = [];
patrocinadoresCarrera = [];
cuentasCarrera = [];

cuentas = [];
base64img;
visi = false;

  constructor(private _logInService: LogInService) { }

  ngOnInit(): void {
    this.formVisibility = false;
  	this.carreras = [
 {
 "nombre": "Endurance 2020",
 "admindeportista": "sam.astua",
 "fecha": "2020-12-10",
 "recorrido": null,
 "costo": 15000,
 "tipoactividad": "Ciclismo",
 "privacidad": true,
"carreraCategoria": [
 {
 "nombrecategoria": "Master B",
 "nombrecarrera": "Carrera 2020",
 "admindeportista": "sam.astua"
 },
 {
 "nombrecategoria": "Master C",
 "nombrecarrera": "Carrera 2020",
 "admindeportista": "sam.astua"
 },
 {
 "nombrecategoria": "Sub-23",
 "nombrecarrera": "Carrera 2020",
 "admindeportista": "sam.astua"
 }
 ],
 "carreraCuentabancaria": [
 {
 "nombrecarrera": "Carrera 2020",
 "admindeportista": "sam.astua",
 "cuentabancaria": "CR05010044498887679823"
 },
 {
 "nombrecarrera": "Carrera 2020",
 "admindeportista": "sam.astua",
 "cuentabancaria": "CR02410056898889747823"
 },

 ],
 "carreraPatrocinador": [
 {
 "nombrepatrocinador": "CoopeTarrazú",
 "nombrecarrera": "Carrera 2020",
 "admindeportista": "sam.astua"
 },
 {
 "nombrepatrocinador": "TDMAS",
 "nombrecarrera": "Carrera 2020",
 "admindeportista": "sam.astua"
 }
 ],
"grupoCarrera": [
{
 "nombrecarrera": "Carrera 2020",
 "admincarrera": "sam.astua",
 "admingrupo": "kevintrox",
 "nombregrupo": "Los Toros"
},
{
 "nombrecarrera": "Carrera 2020",
 "admincarrera": "sam.astua",
 "admingrupo": "sam.astua",
 "nombregrupo": "Las Estrellas"
 }
]
 },
 {
 "nombre": "The Best",
 "admindeportista": "cr7",
 "fecha": "2020-12-20",
 "recorrido": null,
 "costo": 40000,
 "tipoactividad": "Correr",
 "privacidad": false,
"carreraCategoria": [
 {
 "nombrecategoria": "Master B",
 "nombrecarrera": "Carrera 2020",
 "admindeportista": "sam.astua"
 },
 {
 "nombrecategoria": "Master C",
 "nombrecarrera": "Carrera 2020",
 "admindeportista": "sam.astua"
 },
 {
 "nombrecategoria": "Sub-23",
 "nombrecarrera": "Carrera 2020",
 "admindeportista": "sam.astua"
 }
 ],
 "carreraCuentabancaria": [
 {
 "nombrecarrera": "Carrera 2020",
 "admindeportista": "sam.astua",
 "cuentabancaria": "CR05010044498887679823"
 },
 {
 "nombrecarrera": "Carrera 2020",
 "admindeportista": "sam.astua",
 "cuentabancaria": "CR02410056898889747823"
 }
 ],
 "carreraPatrocinador": [
 {
 "nombrepatrocinador": "CoopeTarrazú",
 "nombrecarrera": "Carrera 2020",
 "admindeportista": "sam.astua"
 },
 {
 "nombrepatrocinador": "TDMAS",
 "nombrecarrera": "Carrera 2020",
 "admindeportista": "sam.astua"
 }
 ],
"grupoCarrera": [
]
 }
]
;
  }


agregar(){
  this.formVisibility = true;
   this.carrera = new Carrera();
  

}

submit(nombre,fecha,costo,cuentas,actividad,visibilidad){
  this.carrera = new Carrera();
  this.carrera.nombre = nombre;
  this.carrera.fecha = fecha;
  this.carrera.costo = costo;
  this.carrera.admindeportista = this._logInService.getUsuario();
  this.carrera.tipoactividad = actividad;
  this.carrera.privacidad = this.visi;
  this.carrera.recorrido = this.base64img;
  for(let i = 0 ; i < this.checkedListcat.length; i++) {
      this.categoria = new CarreraCategoria();
      this.categoria.nombrecategoria = this.checkedListcat[i];
      this.categoria.nombrecarrera = nombre;
      this.categoria.admindeportista = this._logInService.getUsuario();
      this.categoriasCarrera.push(this.categoria);
      }
  this.carrera.carreraCategoria = this.categoriasCarrera;

 this.cuentas = cuentas.split(",");

  for(let i = 0 ; i < this.cuentas.length; i++) {
      this.cuenta = new CarreraCuentaBancaria();
      this.cuenta.cuentabancaria = this.cuentas[i];
      this.cuenta.nombrecarrera = nombre;
      this.cuenta.admindeportista = this._logInService.getUsuario();
      this.cuentasCarrera.push(this.cuenta);
  }
  this.carrera.carreraCuentabancaria = this.cuentasCarrera;

  for(let i = 0 ; i < this.checkedListpatr.length; i++) {
      this.patrocinador = new CarreraPatrocinador();
      this.patrocinador.nombrepatrocinador = this.checkedListpatr[i];
      this.patrocinador.nombrecarrera = nombre;
      this.patrocinador.admindeportista = this._logInService.getUsuario();
      this.patrocinadoresCarrera.push(this.patrocinador);
  }
  this.carrera.carreraPatrocinador= this.patrocinadoresCarrera;

  for(let i = 0 ; i < this.checkedListgroup.length; i++) {
      this.grupo = new GrupoCarrera();
      this.grupo.nombregrupo= this.checkedListgroup[i].nombre;
      this.grupo.admingrupo= this.checkedListgroup[i].admindeportista;
      this.grupo.nombrecarrera = nombre;
      this.grupo.admincarrera = this._logInService.getUsuario();
      this.gruposCarrera.push(this.grupo);
  }
  this.carrera.grupoCarrera = this.gruposCarrera;


 console.log(this.checkedListpatr);
 console.log(this.checkedListgroup);
 console.log(this.carrera);
 this.checkedListcat = [];
 this.checkedListpatr = [];
 this.checkedListgroup = [];
 this.categoriasCarrera = [];
 this.gruposCarrera = [];
 this.cuentasCarrera = [];
 this.patrocinadoresCarrera = [];
 this.carreras.push(this.carrera);
this.formVisibility = false;


}
onCheckboxChange(option, event) {
     if(event.target.checked) {
       this.checkedListcat.push(option);
     } else {
     for(let i = 0 ; i < this.checkedListcat.length; i++) {
       if(this.checkedListcat[i] === option) {
         this.checkedListcat.splice(i,1);
      }
    }
  }
  
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
onFileChanged(e): void {
  const file = e.target.files[0];
        if (!file) {
            return;
        }
        const reader = new FileReader();
        reader.onload = (evt) => {
            this.base64img =  (evt as any).target.result;
        };
        reader.readAsText(file);
  }

  readThis(inputValue: any): void {
  let file : File = inputValue.files[0];
  let  myReader: FileReader = new FileReader();
myReader.readAsDataURL(file);
  myReader.onloadend = (e) => {
    this.base64img = myReader.result;
  }
}

 actualiza(carrera){
  this.form2Visibility = true;
  this.carrera = carrera;
  console.log(carrera);

}

modifica(nombre,fecha,costo,cuentas,actividad,visibilidad){
console.log("modifica")
 this.carrera = new Carrera();
  this.carrera.nombre = nombre;
  this.carrera.fecha = fecha;
    this.carrera.admindeportista = this._logInService.getUsuario();
  this.carrera.costo = costo;
  this.carrera.tipoactividad = actividad;
  this.carrera.privacidad = this.visi;
  this.carrera.recorrido = this.base64img;
  for(let i = 0 ; i < this.checkedListcat.length; i++) {
      this.categoria = new CarreraCategoria();
      this.categoria.nombrecategoria = this.checkedListcat[i];
      this.categoria.nombrecarrera = nombre;
      this.categoria.admindeportista = this._logInService.getUsuario();
      this.categoriasCarrera.push(this.categoria);
      }
  this.carrera.carreraCategoria = this.categoriasCarrera;

 this.cuentas = cuentas.split(",");

  for(let i = 0 ; i < this.cuentas.length; i++) {
      this.cuenta = new CarreraCuentaBancaria();
      this.cuenta.cuentabancaria = this.cuentas[i];
      this.cuenta.nombrecarrera = nombre;
      this.cuenta.admindeportista = this._logInService.getUsuario();
      this.cuentasCarrera.push(this.cuenta);
  }
  this.carrera.carreraCuentabancaria = this.cuentasCarrera;

  for(let i = 0 ; i < this.checkedListpatr.length; i++) {
      this.patrocinador = new CarreraPatrocinador();
      this.patrocinador.nombrepatrocinador = this.checkedListpatr[i];
      this.patrocinador.nombrecarrera = nombre;
      this.patrocinador.admindeportista = this._logInService.getUsuario();
      this.patrocinadoresCarrera.push(this.patrocinador);
  }
  this.carrera.carreraPatrocinador= this.patrocinadoresCarrera;

  for(let i = 0 ; i < this.checkedListgroup.length; i++) {
      this.grupo = new GrupoCarrera();
      this.grupo.nombregrupo= this.checkedListgroup[i].nombre;
      this.grupo.admingrupo= this.checkedListgroup[i].admindeportista;
      this.grupo.nombrecarrera = nombre;
      this.grupo.admincarrera = this._logInService.getUsuario();
      this.gruposCarrera.push(this.grupo);
  }
  this.carrera.grupoCarrera = this.gruposCarrera;


 console.log(this.checkedListpatr);
 console.log(this.checkedListgroup);
 console.log(this.carrera);
 this.checkedListcat = [];
 this.checkedListpatr = [];
 this.checkedListgroup = [];
 this.categoriasCarrera = [];
 this.gruposCarrera = [];
 this.cuentasCarrera = [];
 this.patrocinadoresCarrera = [];
 this.form2Visibility = false;


  for(let i = 0 ; i < this.carreras.length; i++) {
      if(this.carreras[i].nombre === nombre){
        this.carreras[i] = this.carrera;
      }
  }


}

radio(e){
     if(e.target.checked) {
        this.visi = false;
     
     } else {
          this.visi = true;  

}
}
radio2(e){
     if(e.target.checked) {
        this.visi = true;
        
     } else {
          this.visi = false;  

}
}

eliminar(id){
    const confirmed = window.confirm('¿Seguro que desea eliminar esta carrera?');
if (confirmed) {
this.elimina = false;


this.carreras = this.carreras.filter((i) => i !== id); // filtramos



}
}
}

