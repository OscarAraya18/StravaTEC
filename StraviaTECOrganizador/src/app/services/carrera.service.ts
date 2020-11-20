import { Injectable } from '@angular/core';
import { Carrera } from 'src/app/modelos/carrera';
import { Patrocinador } from 'src/app/modelos/patrocinador';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LogInService} from 'src/app/services/log-in.service';


@Injectable({
  providedIn: 'root'
})
export class CarreraService {

  constructor(private http: HttpClient, private _logInService: LogInService) { }

   getPatrocinadores(): Observable<Patrocinador[]>{
    return this.http.get<Patrocinador[]>('https://localhost:44371/api/patrocinadores');
}

 getCategorias(): Observable<string[]>{
    return this.http.get<string[]>('https://localhost:44371/api/categoria');
}
 getCarreras(): Observable<Carrera[]>{
    return this.http.get<Carrera[]>('https://localhost:44371/api/carrera/admin/miscarreras', {
      params: {
        usuario: this._logInService.getUsuario()
      }});
}


nuevaCarrera(carrera: Carrera){
    return this.http.post<string>('https://localhost:44371/api/carrera/admin/new', carrera);
}

  getCarrera(nombre): Observable<Carrera>{
    return this.http.get<Carrera>('https://localhost:44371/api/carrera/admin/verCarrera', {
      params: {
        usuario: this._logInService.getUsuario(),
        nombreCarrera: nombre
      }});
}
actualizaCarrera(Carrera: Carrera){
    return this.http.put<string>('https://localhost:44371/api/carrera/admin/edit',  Carrera, {
      params: {
        usuarioAdmin: this._logInService.getUsuario(),
      }});
  }

   borraCarrera(nombre){
    return this.http.delete<string>('https://localhost:44371/api/carrera/admin/delete', {
      params: {
        usuario: this._logInService.getUsuario(),
        nombreCarrera: nombre
      }});
}
}
