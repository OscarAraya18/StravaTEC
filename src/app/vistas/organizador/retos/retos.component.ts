import { Component, OnInit } from '@angular/core';
import { Reto } from 'src/app/modelos/reto';
import { LogInService} from 'src/app/services/log-in.service';
import { RetoPatrocinador } from 'src/app/modelos/reto-patrocinador';
import { GrupoReto } from 'src/app/modelos/grupo-reto';
import { Patrocinador } from 'src/app/modelos/patrocinador';
import { CarreraService} from 'src/app/services/carrera.service';
import { Grupo } from 'src/app/modelos/grupo';
import { GrupoService} from 'src/app/services/grupo.service';
import { RetoService} from 'src/app/services/reto.service';

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
grupos: Grupo[];

patrocinadores: Patrocinador[];
grupo: GrupoReto;
patrocinador: RetoPatrocinador;
reto = new Reto();

checkedListpatr =[];
checkedListgroup =[];
gruposReto = [];
patrocinadoresReto = [];
visi = false;
visibilidad: string
alt: string;
constructor(private _logInService: LogInService,private _GroupService: GrupoService,
    private _CarreraService: CarreraService, private _RetoService: RetoService) { }

  ngOnInit(): void {
     this._GroupService.getGrupos().subscribe(data => this.grupos = data );
    this._CarreraService.getPatrocinadores().subscribe(data => this.patrocinadores = data );
    this._RetoService.getRetos().subscribe(data => { this.retos = data;
      for(let i = 0 ; i < this.retos.length; i++) {
        this._RetoService.getReto(this.retos[i]).subscribe(data => {this.retos[i] = data;
         console.log(data) }
       );

   }
   } );



    this.formVisibility = false;
  	
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

  this._RetoService.nuevoReto(this.reto).subscribe(data => {});
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
  this._RetoService.getReto(reto.nombre).subscribe(data => this.reto = data );
}

modifica(nombre,km,fecha2,alt,actividad,visibilidad, des){
console.log("Modifica");

this.reto.nombre = nombre;
this.reto.admindeportista = this._logInService.getUsuario();
this.reto.fondoaltitud = this.alt;
this.reto.periododisponibilidad = fecha2;
this.reto.privacidad = this.visi;
this.reto.kmtotales = km;
this.reto.tipoactividad = actividad;
this.reto.descripcion = des;


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


  this._RetoService.actualizaReto(this.reto).subscribe(data => {});

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


  this._RetoService.borraReto(this.reto).subscribe(data => {});

this.retos = this.retos.filter((i) => i !== id); // filtramos



}
}
}

