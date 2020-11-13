import { Injectable } from '@angular/core';
import { LogIn } from 'src/app/modelos/log-in';


@Injectable({
  providedIn: 'root'
})
export class LogInService {
Usuario: string;

  constructor() { }

   login( LogIn){
    //return this.http.post<string>('/api/Productores/login', productor);
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

