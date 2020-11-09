import { Injectable } from '@angular/core';
import { Carrera } from 'src/app/modelos/carrera';

@Injectable({
  providedIn: 'root'
})
export class CarreraService {

  //Lista de carreras de prueba
    //GPX PARSER
    xhttp = new XMLHttpRequest();
    parser = new DOMParser();
  listaCarreras = [
    new Carrera('La hermandad', 'Juan Pablo', '20-12-2020', 'recorrido', 25000, 'Correr', false),
    new Carrera('Las brisas', 'Jose Alfredo', '10-12-2020', 'recorrido', 30000, 'Cliclismo', true),
    new Carrera('Pandemia', 'Brandon Gutierrez', '3-10-2020', 'recorrido', 30000, 'Nadar', true),
    new Carrera('Valerosa', 'Pedro Gallardo', '5-8-2020', 'recorrido', 30000, 'Kayak', true),
    new Carrera('Elite 5', 'Manolo Solis', '1-12-2020', 'recorrido', 30000, 'Caminata', true),
    new Carrera('Family tour', 'Kendall Zuñiga', '15-3-2020', 'recorrido', 30000, 'Senderismo', true),
  ]

  constructor() { }

  getListaCarreras(){
    return this.listaCarreras;
  }
  
  loadXmlFile(path: string){
    this.xhttp.onreadystatechange = () => {
    if (this.xhttp.readyState == 4 && this.xhttp.status == 200) {
       // Typical action to be performed when the document is ready:
      return this.xhttp.responseText;
    }
    };
    this.xhttp.open("GET", path, true);
    this.xhttp.send();
  }
}
