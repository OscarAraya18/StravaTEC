import { Injectable } from '@angular/core';
import { Grupo } from 'src/app/modelos/grupo';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LogInService} from 'src/app/services/log-in.service';


@Injectable({
  providedIn: 'root'
})
export class GrupoService {

  constructor(private http: HttpClient, private _logInService: LogInService) { }

  getGrupos(): Observable<Grupo[]>{
    return this.http.get<Grupo[]>('https://localhost:44371/api/grupo/todos');
}

nuevoGrupo(grupo: Grupo){
    return this.http.post<string>('https://localhost:44371/api/grupo/new', grupo);
}

getMisGrupos(){
 return this.http.get<Grupo[]>('https://localhost:44371/api/grupo/admin/misgrupos', {
	params: {
		usuario: this._logInService.getUsuario()
	}
 });
}



actualizaGrupo(grupo: Grupo){
 return this.http.put<string>('https://localhost:44371/api/grupo/edit', grupo , {
	params: {
		usuarioAdmin: this._logInService.getUsuario()
	}
 });
}

 borraGrupo(grupo: Grupo){
    return this.http.delete<string>('https://localhost:44371/api/grupo/delete',
  {
      params: {

        usuario: this._logInService.getUsuario(),
        idGrupo: grupo.id.toString()

      }});

}
}
