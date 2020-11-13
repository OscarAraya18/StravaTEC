import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  //Variable que almacena el nombre de usuario de los deportistas
  nombreUsuario: string = '';

  //Variable que almacena la dirección ip que se solicita en el login
  ip: string;

  //Variable que almacena el puerto que se solicita en el login
  puerto: string;

  constructor() { }
  
  setNombreUsuarioActual(nombreUsuario: string){
    this.nombreUsuario = nombreUsuario;
  }
  getNombreUsuarioActual(){
    return this.nombreUsuario;
  }

  setIp(ip: string){
    this.ip = ip;
  }

  getIp(){
    return this.ip;
  }

  setPuerto(puerto: string){
    this.puerto = puerto;
  }

  getPuerto(){
    return this.puerto;
  }
}
