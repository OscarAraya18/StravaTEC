import { Injectable } from '@angular/core';
import { Carrera } from 'src/app/modelos/carrera';

@Injectable({
  providedIn: 'root'
})
export class CarreraService {

  listaCarreras = [];

  constructor() { }

  getListaCarreras(){
    return this.listaCarreras;
  }
  
  setListaCarreras(listaCarreras){
    this.listaCarreras = listaCarreras;
  }

  getCarreraGPX(nombreCarrera: string){
    for(let i = 0; i < this.listaCarreras.length; i++){
      if(nombreCarrera === this.listaCarreras[i].nombre){
        return this.listaCarreras[i].recorrido;
      }
    }
  }
}
