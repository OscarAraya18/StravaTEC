import { Injectable } from '@angular/core';
import { Inscripcion } from 'src/app/modelos/inscripcion';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LogInService} from 'src/app/services/log-in.service';


@Injectable({
  providedIn: 'root'
})
export class InscrpcionService {

  constructor(private http: HttpClient, private _logInService: LogInService) { }

    getInscripciones(nombre): Observable<Inscripcion[]>{
    return this.http.get<Inscripcion[]>('https://localhost:44371/api/inscripcion/carrera/enespera', {
      params: {
        usuario: this._logInService.getUsuario(),
        nombreCarrera: nombre
      }});
}

aceptarInscripcion(inscripcion: Inscripcion){
  return this.http.post<string>('https://localhost:44371/api/inscripcion/accept', inscripcion);
}

   borraInscripcion(inscripcion: Inscripcion){
    return this.http.delete<string>('https://localhost:44371/api/inscripcion/delete',
  {
      params: {
        adminCarrera: this._logInService.getUsuario(),
        nombreCarrera: inscripcion.nombrecarrera,
        usuario: inscripcion.usuariodeportista

      }});
      console.log("Rechazado");

}



}
