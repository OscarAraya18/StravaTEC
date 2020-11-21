import { Injectable } from '@angular/core';
import { LogInService} from 'src/app/services/log-in.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ReporteService {

  constructor(private http: HttpClient, private _logInService: LogInService) { }


    getParticipantes(nombre){
    return this.http.get<string>('https://localhost:44307/api/admin/verParticipantes', {
      params: {
        admin: this._logInService.getUsuario(),
        nombreCarrera: nombre
      }});
}

   getPosiciones(nombre){
    return this.http.get<string>('https://localhost:44307/api/admin/verPosiciones', {
      params: {
        admin: this._logInService.getUsuario(),
        nombreCarrera: nombre
      }});
}
}
