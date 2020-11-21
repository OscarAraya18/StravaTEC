import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { __param } from 'tslib';
import { stringify } from 'querystring';



export class UsuarioLogin {
  constructor(
      public Usuario: string,
      public ClaveAcceso: string
  ){}
}

export interface UsuarioI {
  usuario : string,
  claveacceso : string,
  fechanacimiento : string,
  nombre : string,
  apellido1 : string,
  apellido2 : string,
  nombrecategoria : string,
  nacionalidad: string,
  foto : string
}

    


@Injectable({providedIn: 'root'})
  export class InicioSesionService {
  
    constructor(private http: HttpClient) { }

    iniciarSesion(nombreUsuario: string, claveAcceso: string){
      let usuario = new UsuarioLogin(nombreUsuario,claveAcceso);
      return this.http.post<string>('https://localhost:44371/api/user/login', usuario);
    } 

    obtenerInformacion(nombreUsuario: string){
      return this.http.get<UsuarioI[]>('https://localhost:44371/api/user/getDeportista?', 
                                      {
                                        params: { usuario : nombreUsuario}
                                      });
    }

    solicitarRecuperarClaveAcceso(nombreUsuario : string, claveAcceso : string, correoElectronico : string){
      return this.http.post<string>('https://localhost:44371/api/deportista/recuperarClaveAcceso',null,
                                    {
                                      params: {nombreUsuario : nombreUsuario, claveAcceso: claveAcceso, correoElectronico: correoElectronico}
                                    });
    }

  }
  