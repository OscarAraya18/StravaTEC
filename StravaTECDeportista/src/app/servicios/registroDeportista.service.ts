import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { __param } from 'tslib';



export class UsuarioRegistro {
    constructor(
        public usuario: string,
        public claveacceso: string,
        public fechanacimiento: string,
        public nombre: string,
        public apellido1: string,
        public apellido2: string,
        public nombrecategoria: string,
        public nacionalidad: string,
        public foto: string
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
  export class RegistroDeportistaService {
  
    constructor(private http: HttpClient) { }

    verificarNombreUsuario(nombreUsuario: string){
        return this.http.get<UsuarioI[]>('https://localhost:44371/api/user/getDeportista?', 
                                        {
                                          params: { usuario : nombreUsuario}
                                        });
    }

    crearDeportista(primerNombre: string, primerApellido: string, segundoApellido: string,
                    nombreUsuario: string, claveAcceso: string, fechaNacimiento: string,
                    nombreCategoria: string, nacionalidad: string, fotoPerfil: string){
        let usuario = new UsuarioRegistro(nombreUsuario,claveAcceso,fechaNacimiento,primerNombre,
                                            primerApellido,segundoApellido,nombreCategoria,
                                            nacionalidad,fotoPerfil);
        return this.http.post<string>('https://localhost:44371/api/user/new?', usuario);
    } 
}
  