import { Injectable } from '@angular/core';
import { Reto } from 'src/app/modelos/reto';
import { HttpClient } from '@angular/common/http';
import { LogInService} from 'src/app/services/log-in.service';

@Injectable({
  providedIn: 'root'
})
export class RetoService {

  constructor(private http: HttpClient, private _logInService: LogInService) { }

nuevoReto(reto: Reto){
    return this.http.post<string>('https://localhost:44371/api/reto/new', reto);
}

getRetos(){
 return this.http.get<Reto[]>('https://localhost:44371/api/reto/admin/misretos', {
	params: {
		usuario: this._logInService.getUsuario()
	}
 });
}

getReto(reto: Reto){
 return this.http.get<Reto>('https://localhost:44371/api/reto/admin/verReto', {
	params: {
		usuario: this._logInService.getUsuario(),
		nombreReto: reto.nombre
	}
 });
}

actualizaReto(reto: Reto){
 return this.http.put<string>('https://localhost:44371/api/reto/edit', reto , {
	params: {
		usuarioAdmin: this._logInService.getUsuario()
	}
 });
}

 borraReto(reto: Reto){
    return this.http.delete<string>('https://localhost:44371/api/reto/delete',
  {
      params: {
        nombreReto: reto.nombre,
        usuario: this._logInService.getUsuario()

      }});

}

}
