import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { __param } from 'tslib';

export interface ActividadI {
    usuariodeportista : string
}

@Injectable({providedIn: 'root'})
export class MiMuroService {
  
    constructor(private http: HttpClient) { }

    solicitarActividades(nombreUsuario: string){
        return this.http.get<ActividadI[]>('https://localhost:44371/api/user/amigos/actividades?', 
                                        {
                                          params: { usuario : nombreUsuario}
                                        });
    }
}
