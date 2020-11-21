import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { __param } from 'tslib';

export interface RetoI {
    nombre : string,
}

export interface CarreraI {
    nombre : string
}

@Injectable({providedIn: 'root'})
export class MisCarrerasRetosService {
    constructor(private http: HttpClient) { }

    solicitarMisRetos(nombreUsuario: string){
        return this.http.get<CarreraI[]>('https://localhost:44371/api/user/reto/estado?',
                                        {
                                            params: {usuario : nombreUsuario}
                                        });
    } 
    solicitarMisCarreras(nombreUsuario : string){
        return this.http.get<RetoI[]>('https://localhost:44371/api/carrera/user/carrerasConPosiciones?',
                                        {
                                            params : {usuario : nombreUsuario}
                                        });
    }
}