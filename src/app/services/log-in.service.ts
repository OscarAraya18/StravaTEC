import { Injectable } from '@angular/core';
import { LogIn } from 'src/app/modelos/log-in';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class LogInService {
Usuario: string;

  constructor(private http: HttpClient) { }

   login( user: LogIn){
    return this.http.post<string>('https://localhost:44371/api/user/login', user);
  }
  setUsuario(Usuario: string){
    this.Usuario = Usuario;
    
  }

  getUsuario(): string{
    return this.Usuario;
  }
  logOut(){
    this.Usuario = null;
   
  }
}

