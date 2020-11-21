import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { __param } from 'tslib';

export interface GrupoI {
    nombre : string,
    admindeportista : string
}


export class GrupoUnion {
    constructor(
        public usuarioDeportista: string,
        public idGrupo: number,
        public adminDeportista: string
        ){}
}

  @Injectable({providedIn: 'root'})
  export class BuscarGruposService {
  
    constructor(private http: HttpClient) { }

    solicitarGruposNoInscrito(nombreUsuario: string){
        return this.http.get<GrupoI[]>('https://localhost:44371/api/user/grupos/noInscritos?', 
                                        {
                                          params: { usuario : nombreUsuario }
                                        });
    }

    solicitarGruposPorNombre(nombreGrupo : string){
        return this.http.get<GrupoI[]>('https://localhost:44371/api/user/grupo?', 
                                        {
                                          params: { nombreGrupo : nombreGrupo }
                                        });
    }

    
    solicitarUnirGrupo(idGrupo : number , administradorGrupo : string, nombreUsuario : string){
        console.log(idGrupo);
        console.log(administradorGrupo);
        
        let grupo = new GrupoUnion(nombreUsuario,idGrupo,administradorGrupo);
        console.log(grupo);
        return this.http.post<string>('https://localhost:44371/api/grupo/new/deportista', grupo);
    }

  }
