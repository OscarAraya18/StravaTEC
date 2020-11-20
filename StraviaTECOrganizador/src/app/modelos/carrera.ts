import { CarreraCategoria } from 'src/app/modelos/carrera-categoria';
import { CarreraCuentaBancaria } from 'src/app/modelos/carrera-cuenta-bancaria';
import { CarreraPatrocinador } from 'src/app/modelos/carrera-patrocinador';
import { GrupoCarrera } from 'src/app/modelos/grupo-carrera';

export class Carrera {
nombre: string;
admindeportista: string;
fecha: string;
recorrido: string;
costo: number;
tipoactividad: string;
privacidad: boolean;
carreraCategoria: CarreraCategoria[];
carreraCuentabancaria: CarreraCuentaBancaria[];
carreraPatrocinador: CarreraPatrocinador[];
grupoCarrera: GrupoCarrera[];
}
