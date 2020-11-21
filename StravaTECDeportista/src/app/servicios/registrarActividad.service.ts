import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { __param } from 'tslib';


export class ActividadRegistro {
    constructor(
        public usuariodeportista: string,
        public nombre: string,
        public fechahora: string,
        public duracion: string,
        public kilometraje: number,
        public tipoactividad: string,
        public recorridogpx: string,
        public NombreRetoCarrera: string,
        public AdminRetoCarrera: string,
        public banderilla: number
        ){}
}

export interface CarreraI {
    nombre : string
}

export interface RetoI {
    nombre: string
}

@Injectable({providedIn: 'root'})
  export class RegistrarActividadService {
  
    constructor(private http: HttpClient) { }

    solicitarRegistrarActividad(nombreUsuario: string, nombre: string, fecha: string,
                                hora: string, duracion: string, distanciaRecorrida: number,
                                tipoactividad: string, ruta: string, nombreRetoCarrera: string,
                                administradorRetoCarrera: string, flag : number){
        let fechaHora = fecha[8]+fecha[9]+"/"+fecha[5]+fecha[6]+"/"+fecha[0]+fecha[1]+fecha[2]+fecha[3] + " " + hora;
        let actividad = new ActividadRegistro(nombreUsuario,nombre,fechaHora,duracion,
                                            distanciaRecorrida,tipoactividad,ruta,
                                            nombreRetoCarrera,administradorRetoCarrera,flag);
        console.log(actividad);
        return this.http.post<string>('https://localhost:44371/api/user/registrar/actividad', actividad);
    } 

    solicitarPosiblesRetos(nombreUsuario: string){
        return this.http.get<CarreraI[]>('https://localhost:44371/api/user/reto/estado?',
                                        {
                                            params: {usuario : nombreUsuario}
                                        });
    }
    
    solicitarPosiblesCarreras(nombreUsuario : string){
        return this.http.get<RetoI[]>('https://localhost:44371/api/carrera/user/carrerasConPosiciones?',
                                        {
                                            params : {usuario : nombreUsuario}
                                        });
    }

}
  