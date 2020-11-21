import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { __param } from 'tslib';


export interface DeportistaI {
    nombre : string,
    apellido1 : string,
    apellido2 : string,
    nombrecategoria : string,
    nacionalidad : string,
    foto : string,
    usuario : string,
    claveacceso : string,
    fechanacimiento : string
}

@Injectable({providedIn: 'root'})
export class SeguirDeportistasService {

  constructor(private http: HttpClient) { }

  solicitarDeportistasNoSeguidos(nombreUsuario: string){
      return this.http.get<DeportistaI[]>('https://localhost:44371/api/user/noAmigos?', 
                                      {
                                        params: { usuario : nombreUsuario }
                                      });
  }

  solicitarDeportistasPorNombre(nombreDeportista : string, nombreUsuario : string){
    return this.http.get<DeportistaI[]>('https://localhost:44371/api/user/buscarPorNombre?', 
                                    {
                                      params: { nombre : nombreDeportista, usuario : nombreUsuario }
                                    });
    }

    solicitarSeguirDeportista(nombreUsuarioDeportista : string, nombreUsuario : string){
        return this.http.post<string>('https://localhost:44371/api/user/amigo/new?',null,
                                    {
                                        params: { amigo : nombreUsuarioDeportista, usuario : nombreUsuario}
                                    });
    }

}


