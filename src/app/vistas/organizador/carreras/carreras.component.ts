import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-carreras',
  templateUrl: './carreras.component.html',
  styleUrls: ['./carreras.component.css']
})
export class CarrerasComponent implements OnInit {
carreras: any[];
formVisibility: boolean;
form2Visibility: boolean;
elimina: boolean;
actividades = ["Nadar","Correr","Ciclismo","Senderismo","Kayak","Caminata"];
categorias = ["Junior","Sub-23","Elite","Master-A","Master-B","Master-C"];
patrocinadores = ["Coca cola", "Freedom", "Puma","Adidas"];
grupos = ["Equipo de ciclismo TEC", "Grupo2","Grupo3",'Grupo 4']
checkedListcat =[];
checkedListpatr =[];
checkedListgroup =[];
base64img;
visi = false;

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
 console.log(nombre);
 console.log(fecha);
 console.log(costo);
 console.log(cuentas);
 console.log(actividad);
 console.log(visibilidad);
 console.log(this.checkedListcat);
 console.log(this.checkedListpatr);
 // console.log(this.base64img);
 console.log(this.checkedListgroup);

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
onFileChanged(event): void {
  this.readThis(event.target);
  }
  readThis(inputValue: any): void {
  let file : File = inputValue.files[0];
  let  myReader: FileReader = new FileReader();
myReader.readAsDataURL(file);
  myReader.onloadend = (e) => {
    this.base64img = myReader.result;
  }
}

 actualiza(){
  this.form2Visibility = true;
}

modifica(nombre,fecha,costo,cuentas,actividad,visibilidad){
console.log("modifica")
 console.log(nombre);
 console.log(fecha);
 console.log(costo);
 console.log(cuentas);
 console.log(actividad);
 console.log(visibilidad);
 console.log(this.checkedListcat);
 console.log(this.checkedListpatr);
 console.log(this.base64img);

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


this.carreras = this.carreras.filter((i) => i.nombre !== id); // filtramos



}
}
}

