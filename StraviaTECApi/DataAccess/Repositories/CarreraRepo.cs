using Microsoft.EntityFrameworkCore;
using StraviaTECApi.Models;
using StraviaTECApi.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFConsole.DataAccess.Repositories
{
    public class CarreraRepo
    {
        private readonly StraviaContext _context;

        // se inyecta el DB Context 
        public CarreraRepo(StraviaContext context)
        {
            _context = context;
        }

        /**
         * ------------------------------
         *         MÉTODOS CRUD
         * ------------------------------
         */

        public void Create(Carrera carrera)
        {
            if (carrera == null)
                throw new ArgumentNullException(nameof(carrera));

            _context.Carrera.Add(carrera);

        }

        public void Update(Carrera carrera)
        {
            if (carrera == null)
                throw new ArgumentNullException(nameof(carrera));

            _context.Carrera.Update(carrera);
            _context.Entry(carrera).State = EntityState.Modified;

        }

        public void Delete(string nombre, string admin)
        {
            var carrera = _context.Carrera.FirstOrDefault(x => x.Nombre == nombre && x.Admindeportista == admin);

            if (carrera == null)
            {
                throw new ArgumentNullException(nameof(carrera));
            }

            _context.Carrera.Remove(carrera);

        }

        /// <summary>
        /// Método para ver las carreras administradas por determinado deportista
        /// </summary>
        /// <param name="usuarioDeportista">el deportista a validar</param>
        /// <returns>La lista de carreras administradas</returns>
        public List<Carrera> verMisCarreras(string usuarioDeportista)
        {
            return _context.Carrera.Where(x => x.Admindeportista == usuarioDeportista).ToList();
            // se retorna la lista
        }

        /// <summary>
        /// Método para acceder a una carrera administrada mediante el nombre
        /// </summary>
        /// <param name="adminCarrera">el administrador de la carerra</param>
        /// <param name="nombreCarrera">el nombre de la carrera a buscar</param>
        /// <returns>La carrera encontrada</returns>
        public Carrera verCarreraAdministradaPorNombre(string adminCarrera, string nombreCarrera)
        {
            return _context.Carrera.Where(x => x.Admindeportista == adminCarrera && x.Nombre.Equals(nombreCarrera)).
                Include(x => x.CarreraCuentabancaria).
                Include(x => x.CarreraPatrocinador).
                Include(x => x.GrupoCarrera).
                Include(x => x.CarreraCategoria).FirstOrDefault();
        }

        /// <summary>
        /// Método para buscar una carrera por su nombre. Se valida que la carrera no haya sido
        /// inscrita por el deportista
        /// </summary>
        /// <param name="usuario">usuario a validar</param>
        /// <param name="nombreCarrera">el nombre de la carrera a buscar</param>
        /// <returns>La lista de carreras con el resultado obtenido</returns>
        public List<CarreraParser> buscarCarreraPorNombre(string usuario, string nombreCarrera)
        {
            List<Carrera> resultadoNoInscritas = new List<Carrera>();

            List<Carrera> resultadoParcial = new List<Carrera>();

            // se accede a los grupos del deportista
            var grupos = _context.GrupoDeportista.Where(x => x.Usuariodeportista == usuario).Include(x => x.Grupo).ToList();

            // se realiza la búsqueda de las carreras
            var resultadosBusqueda = _context.Carrera.Where(x => x.Nombre.Contains(nombreCarrera)).
                                    Include(x => x.CarreraCuentabancaria).
                                    Include(x => x.CarreraPatrocinador).
                                    Include(x => x.CarreraCategoria).ToList();
            
            // se accede a las carreras privadas y a los grupos en donde es posible verlas
            var carrerasPrivadas = _context.GrupoCarrera.
                    Include(x => x.Carrera).
                    ThenInclude(x => x.CarreraCuentabancaria).
                    Include(x => x.Carrera.CarreraCategoria).
                    Include(x => x.Carrera.CarreraPatrocinador).ToList();
            
            // se accede a las carreras inscritas por el usuario
            var carrerasInscritas = verCarrerasInscritas(usuario);

            foreach (var carrera in carrerasPrivadas)
            {
                foreach (var grupo in grupos)
                {
                    if (grupo.Idgrupo == carrera.Idgrupo && grupo.Admindeportista.Equals(carrera.Admingrupo)
                        && !carrerasInscritas.Contains(carrera.Carrera)
                        && (int)(carrera.Carrera.Fecha - DateTime.Today).TotalDays >= 0) // se valida si la carrera no está vencida
                    {
                        resultadoParcial.Add(carrera.Carrera);
                        break;
                    }
                }
            }

            foreach(var carrera in resultadosBusqueda)
            {
                if (resultadoParcial.Contains(carrera) && (int)(carrera.Fecha - DateTime.Today).TotalDays >= 0) // se valida que no esté vencida
                    resultadoNoInscritas.Add(carrera);
            }

            // se genera un JSON diferente para la parte del deportista
            return generarJSONCarrera(resultadoNoInscritas, null);
        }

        /// <summary>
        /// Método para acceder a las carerras inscritas por un deportista
        /// </summary>
        /// <param name="usuarioDeportista">el deportista a validar</param>
        /// <returns>La lista con las carerras inscritas</returns>
        public List<Carrera> verCarrerasInscritas(string usuarioDeportista)
        {
            List<Carrera> carreras = new List<Carrera>();

            // se accede a las carerras inscritas por el deportista
            var carrerasInscritas = _context.DeportistaCarrera.
                    Where(x => x.Usuariodeportista == usuarioDeportista).
                    Include(x => x.Carrera.CarreraCuentabancaria).
                    Include(x => x.Carrera.CarreraCategoria).
                    Include(x => x.Carrera.CarreraPatrocinador).ToList();

            foreach (var carrera in carrerasInscritas)
            {
                carreras.Add(carrera.Carrera);
               
            }

            return carreras;
        }

        /// <summary>
        /// Método para acceder a las carreras NO inscritas por un deportista
        /// </summary>
        /// <param name="usuarioDeportista">el deportista a validar</param>
        /// <returns>La lista de carreras NO inscritas por el deportista</returns>
        public List<Carrera> verCarrerasNoInscritas(string usuarioDeportista)
        {
            // se accede a las carreras inscritas
            var carrerasInscritas = verCarrerasInscritas(usuarioDeportista);

            // se accede a todas las carreras de la base de datos
            var carrerasTotales = _context.Carrera.ToList();

            List<Carrera> carrerasNoInscritas = new List<Carrera>();

            foreach (var carrera in carrerasTotales)
            {
                if (!carrerasInscritas.Contains(carrera))
                    carrerasNoInscritas.Add(carrera);
            }

            return carrerasNoInscritas;
        }

        /// <summary>
        /// Método para acceder a todas las carreras disponibles para un deportista. Se valida que las carreras
        /// no estén inscritas previamente, no estén vencidas y si es privada, que el deportista pertenezca a un grupo
        /// el cual tenga acceso a la carrea en cuestión.
        /// </summary>
        /// <param name="usuario">el deportista a validar</param>
        /// <returns>La lista de las carreras disponibles para ese deportista</returns>
        public List<CarreraParser> verCarrerasDisponibles(string usuario)
        {
            List<Carrera> carreras = new List<Carrera>();

            // se accede a los grupos que tiene asociado el usuario
            var grupos = _context.GrupoDeportista.Where(x => x.Usuariodeportista == usuario).Include(x => x.Grupo).ToList();

            // se acceden a todas las carreas que sean públicas
            var carrerasPublicas = _context.Carrera.Where(x => x.Privacidad == false).Include(x => x.CarreraCuentabancaria).
                                    Include(x => x.CarreraCategoria).
                                    Include(x => x.CarreraPatrocinador).ToList();

            // Se acceden a las carreras inscritas por el usuario
            var carrerasInscritas = verCarrerasInscritas(usuario);

            // se accede a las carreras privadas y los grupos en los cuales se puede visualizar
            var carrerasPrivadas = _context.GrupoCarrera.
                    Include(x => x.Carrera).
                    ThenInclude(x => x.CarreraCuentabancaria).
                    Include(x => x.Carrera.CarreraCategoria).
                    Include(x => x.Carrera.CarreraPatrocinador).ToList();

            foreach(var carrera in carrerasPrivadas)
            {
                foreach(var grupo in grupos)
                {
                    if(grupo.Idgrupo == carrera.Idgrupo && grupo.Admindeportista.Equals(carrera.Admingrupo)
                        && !carrerasInscritas.Contains(carrera.Carrera) 
                        && (int)(carrera.Carrera.Fecha - DateTime.Today).TotalDays >= 0) // se valida que no esté vencida
                    {
                       carreras.Add(carrera.Carrera);
                        break;
                    }
                }
            }

            foreach(var carrera in carrerasPublicas)
            {
                if (!carrerasInscritas.Contains(carrera) && (int)(carrera.Fecha - DateTime.Today).TotalDays >= 0) // se valida que no esté vencida
                    carreras.Add(carrera);
            }  
            // se genera un JSON específico para la parte de deportista
            return generarJSONCarrera(carreras, null);
        }
        
        /// <summary>
        /// Método para ver las carreras inscritas por un usuariuo, con sus respectivas posiciones en caso de que esté
        /// terminada
        /// </summary>
        /// <param name="usuario">deportista a validar</param>
        /// <returns>La lista de carreras con las posiciones en caso de que esté finalizada</returns>
        public List<CarreraParser> carrerasConPosiciones(string usuario)
        {
            // se accede a las carreras inscritas por el usuario
            var carrerasInscritas = verCarrerasInscritas(usuario);

            // se acceden todas las actividades que hay en la base de datos
            var actividades = _context.Actividad.ToList();

            // se genera un JSOn específico para la parte de deportista
            return generarJSONCarrera(carrerasInscritas, actividades);
        }

        /// <summary>
        /// Método para preparar un JSON específico para el FrontEnd y se pueda
        /// acceder a todos los requerimientos de mejor manera
        /// </summary>
        /// <param name="listaCarreras">La lista de carreras a realizarle el nuevo JSON</param>
        /// <param name="actividades">Las actividades existentes para incluirlas en el JSON</param>
        /// <returns>El nuevo JSON (lista de carreras con su respectivas posiciones)</returns>
        public List<CarreraParser> generarJSONCarrera(List<Carrera> listaCarreras, List<Actividad> actividades)
        {
            // se acceden todos los patrocinadores existentes en la base de datos
            var patrocinadores = _context.Patrocinador.ToList();

            List<CarreraParser> carrerasConPosiciones = new List<CarreraParser>();

            foreach (var carrera in listaCarreras)
            {
                // se crea un parser para la carrera
                var carreraParser = new CarreraParser
                {
                    nombreCarrera = carrera.Nombre,
                    adminDeportista = carrera.Admindeportista,
                    recorridoGPX = carrera.Recorrido,
                    fecha = carrera.Fecha,
                    tipoActividad = carrera.Tipoactividad,
                    costo = carrera.Costo,
                    privacidad = carrera.Privacidad,
                    diasFaltantes = (int)(carrera.Fecha - DateTime.Today).TotalDays
                };

                // se agregan las cuentas bancarias
                foreach(var cuenta in carrera.CarreraCuentabancaria)
                {
                    carreraParser.carreraCuentabancaria.Add(cuenta.Cuentabancaria);
                }

                // se agregan los patrocinadores
                foreach (var patrocinador in carrera.CarreraPatrocinador)
                {
                    // se accede a un patrocinador específico de una carrera
                    var carreraPatrocinador = patrocinadores.
                        Where(x => x.Nombrecomercial == patrocinador.Nombrepatrocinador).
                        FirstOrDefault();

                    // se crea un parser para los patrocinadores
                    carreraParser.carreraPatrocinador.Add(new PatrocinadorParser
                    {
                        Nombrecomercial = carreraPatrocinador.Nombrecomercial,
                        Nombrerepresentante = carreraPatrocinador.Nombrerepresentante,
                        Logo = carreraPatrocinador.Logo,
                        Numerotelrepresentante = carreraPatrocinador.Numerotelrepresentante
                    });
                }

                // se agregan las categorías
                foreach (var categoria in carrera.CarreraCategoria)
                {
                    carreraParser.carreraCategorias.Add(categoria.Nombrecategoria);
                }

                if (actividades != null)
                {
                    // se agregan las actividades
                    foreach (var actividad in actividades)
                    {
                        if (actividad.Nombreretocarrera == carrera.Nombre && actividad.Adminretocarrera == carrera.Admindeportista
                            && (int)(DateTime.Today - carrera.Fecha).TotalDays > 0)
                        {
                            var posActividad = new PosActividad()
                            {
                                usuarioDeportista = actividad.Usuariodeportista,
                                duracion = actividad.Duracion,
                                tipoActividad = actividad.Tipoactividad
                            };
                            carreraParser.actividades.Add(posActividad);
                            carreraParser.finalizada = true;
                        }
                    }
                    carreraParser.actividades = carreraParser.actividades.OrderBy(x => x.duracion).ToList();
                }
                // se agrega a la lista el parser de carrera
                carrerasConPosiciones.Add(carreraParser);
            }

            return carrerasConPosiciones;
        }

        public string verParticipantesPorCarrera(string nombreCarrera, string adminCarrera)
        {

            //var reporte = new ReportController();

            //return reporte.Reporte_participantes(nombreCarrera, adminCarrera);
            return "Reporte participantes";
        }

        public string verPosicionesPorCarrera(string nombreCarrera, string adminCarrera)
        {

            //var reporte = new ReportController();

            //return reporte.Reporte_posiciones(nombreCarrera, adminCarrera);
            return "Reporte posiciones";
        }


        /**         
         * Save the changes made to the database
         */
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
