import { RetoPatrocinador } from 'src/app/modelos/reto-patrocinador';
import { GrupoReto } from 'src/app/modelos/grupo-reto';
export class Reto {
nombre: string;
admindeportista: string;
fondoaltitud: string;
tipoactividad: string;
periododisponibilidad: string;
privacidad: boolean;
kmtotales: number;
descripcion: string;
grupoReto: GrupoReto[];
retoPatrocinador: RetoPatrocinador[];
}
