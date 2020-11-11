export class Actividad {
    constructor(
        public UsuarioDeportista: string,
        public Nombre: string,
        public FechaHora: string,
        public Duracion: string,
        public Kilometraje: number,
        public TipoActividad: string,
        public RecorridoGPX: string,
        public NombreRetoCarrera: string,
        public AdminRetoCarrera: string,
        public Banderilla: number
    ){}
}
