import { Injectable } from '@angular/core';
import { Reto } from 'src/app/modelos/reto';

@Injectable({
  providedIn: 'root'
})
export class RetoService {

  //Lista de retos de prueba
  listaRetos = [
    new Reto('Guana run', 'Mariela Ovando', 'Fondo', 'Correr', '20-12-2020', true, 12.7, 'Ruta para machos alfa, ¿podrás correr en el infiero costarricense?'),
    new Reto('Torero Cleteador', 'Kevin Acevedo', 'Fondo', 'Ciclismo', '31-12-2020', false, 57, '¿Alguna vez has cleteado cuando cuando te persiguen toros de monta?'),
    new Reto('Ruinas Melosas', 'Pipe Daza', 'Altitud', 'Senderismo', '31-12-2020', false, 57, 'Gente por todas partes y obstáculos te esperan en este reto'),
    new Reto('El más veloz', 'Chepe Fortuna', 'Fondo', 'Correr', '31-12-2020', false, 57, '¿Crees que puedes correr más rápido que nadie?')
  ]
  constructor() { }

  getListaRetos(){
    return this.listaRetos;
  }
}
