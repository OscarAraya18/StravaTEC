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

export class InscripcionCarrera {
    constructor(
        public Usuariodeportista: string,
        public Estado: string,
        public Recibopago: string,
        public Nombrecarrera : string,
        public Admincarrera : string
        ){}
}

@Injectable({providedIn: 'root'})
export class BuscarCarrerasRetosService {
    constructor(private http: HttpClient) { }

    solicitarPosiblesRetos(nombreUsuario: string){
        return this.http.get<CarreraI[]>('https://localhost:44371/api/reto/user/retosDisponibles?',
                                        {
                                            params: {usuario : nombreUsuario}
                                        });
    } 
    solicitarPosiblesCarreras(nombreUsuario : string){
        return this.http.get<RetoI[]>('https://localhost:44371/api/carrera/user/carrerasDisponibles?',
                                        {
                                            params : {usuario : nombreUsuario}
                                        });
    }

    solicitarParticiparReto(nombreUsuario : string, nombreReto : string, administradorReto : string){
        return this.http.post<string>('https://localhost:44371/api/reto/user/inscribirse?', null,
                                    {
                                        params: { usuario : nombreUsuario, nombreReto : nombreReto, adminReto : administradorReto}
                                    });
    }

    solicitarInscribirCarrera(nombreUsuario : string, nombreCarrera : string, administradorCarrera : string, reciboPago : string){
        console.log(nombreCarrera);
        let inscripcion = new InscripcionCarrera(nombreUsuario,"En espera",reciboPago,nombreCarrera,administradorCarrera);
        return this.http.post<string>('https://localhost:44371/api/inscripcion/new?', inscripcion);
    }


}