import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  //Variable que almacena el nombre de usuario de los deportistas
  nombreUsuario: string = '';
  constructor() { }
  
  setNombreUsuarioActual(nombreUsuario: string){
    this.nombreUsuario = nombreUsuario;
  }
  getNombreUsuarioActual(){
    return this.nombreUsuario;
  }
}
