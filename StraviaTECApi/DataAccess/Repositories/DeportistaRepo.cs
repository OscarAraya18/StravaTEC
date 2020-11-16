using Microsoft.EntityFrameworkCore;
using StraviaTECApi.Models;
using StraviaTECApi.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFConsole.DataAccess.Repositories
{
    public class DeportistaRepo
    {

        private readonly StraviaContext _context;

        // se inyecta el DB Context 
        public DeportistaRepo(StraviaContext context)
        {
            _context = context;
        }

        /**
         * ------------------------------
         *         MÉTODOS CRUD
         * ------------------------------
         */
            
        public void Create(DeportistaParser deportistaParser)
        {
            if (deportistaParser == null)
                throw new ArgumentNullException(nameof(deportistaParser));

            var deportista = new Deportista
            {
                Usuario = deportistaParser.Usuario,
                Claveacceso = deportistaParser.Claveacceso,
                Fechanacimiento = deportistaParser.Fechanacimiento,
                Nombre = deportistaParser.Nombre,
                Apellido1 = deportistaParser.Apellido1,
                Apellido2 = deportistaParser.Apellido2,
                Nombrecategoria = deportistaParser.Nombrecategoria,
                Nacionalidad = deportistaParser.Nacionalidad,
            };

            // si existe una foto en base64, hay que convertirla en un byte array
            if (deportistaParser.Foto != null)
                deportista.Foto = Convert.FromBase64String(deportistaParser.Foto);

            _context.Deportista.Add(deportista);
        }

        public void Update(DeportistaParser deportistaParser)
        {
            if (deportistaParser == null)
                throw new ArgumentNullException(nameof(deportistaParser));

            var deportista = new Deportista
            {
                Usuario = deportistaParser.Usuario,
                Claveacceso = deportistaParser.Claveacceso,
                Fechanacimiento = deportistaParser.Fechanacimiento,
                Nombre = deportistaParser.Nombre,
                Apellido1 = deportistaParser.Apellido1,
                Apellido2 = deportistaParser.Apellido2,
                Nombrecategoria = deportistaParser.Nombrecategoria,
                Nacionalidad = deportistaParser.Nacionalidad,
            };

            // si existe una foto en base64, hay que convertirla en un byte array
            if (deportistaParser.Foto != null)
                deportista.Foto = Convert.FromBase64String(deportistaParser.Foto);

            _context.Deportista.Update(deportista);
            _context.Entry(deportista).State = EntityState.Modified;

        }

        public void Delete(string usuario)
        {
            var deportista = _context.Deportista.FirstOrDefault(x => x.Usuario == usuario);

            if (deportista == null)
            {
                throw new ArgumentNullException(nameof(deportista));
            }

            _context.Deportista.Remove(deportista);

        }

        /// <summary>
        /// Método para acceder a un usuario en específico
        /// </summary>
        /// <param name="usuario">el usuario a buscar</param>
        /// <returns>El deportista con ese nombre de usuario</returns>
        public Deportista obtenerPorUsuario(string usuario)
        {
            return _context.Deportista.FirstOrDefault(x => x.Usuario == usuario);
        }

        /// <summary>
        /// Método para buscar un deportista por su nombre. Se valida que el usuario no sea amigo de
        /// el usuario que realiza la petición.
        /// </summary>
        /// <param name="nombre">Nombre a buscar (coincidencias del mismo) </param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public List<Deportista> obtenerPorNombre(string nombre, string usuario)
        {
            List<Deportista> deportistasDisponibles = new List<Deportista>();

            // se realiza la búsqueda en la base de datos
            var resultado = _context.Deportista.Where(x => x.Nombre.ToLower().Contains(nombre.ToLower())).ToList();

            // se acceden a los deportistas que son amigos del usuario
            var deportistasAmigos = verAmigosAsociados(usuario);

            foreach(var deportista in resultado)
            {
                if (!deportistasAmigos.Contains(deportista)) // se agrega si no es amigo
                    deportistasDisponibles.Add(deportista);
            }

            return deportistasDisponibles;
        }

        /// <summary>
        /// Método para verificar el login de un deportista-
        /// </summary>
        /// <param name="login">El obejto para validar nombre de usuario y clave de acceso</param>
        /// <returns>Un true si fue exitoso, false en caso contrario</returns>
        public bool verificarLogin(Login login)
        {
            var deportista = _context.Deportista.Where(x => x.Usuario == login.Usuario
                              && x.Claveacceso == login.ClaveAcceso).ToList();

            if (deportista.Count == 1)
                return true;

            return false;
        }

        /// <summary>
        /// Método para acceder a todas las actividades realizadas por los amigos de un deportista
        /// en concreto
        /// </summary>
        /// <param name="usuario">el usuario a validar</param>
        /// <returns></returns>
        public List<Actividad> verActividadesAmigos(string usuario)
        {
            List<Actividad> actividades = new List<Actividad>();
            
            // se accede a los amigos del deportista
            var amigoDeportista = _context.AmigoDeportista.
                    Where(x => x.Usuariodeportista == usuario).
                    Include(x => x.Amigo).
                    ThenInclude(x => x.Actividad).ToList();

            foreach (var amigo in amigoDeportista)
            {

                foreach(var actividad in amigo.Amigo.Actividad)
                {
                    actividades.Add(actividad);
                }
                actividades = actividades.OrderBy(x => x.Kilometraje).ToList(); // se ordenan por el kilometraje
            }

            return actividades;
        }

        /// <summary>
        /// Método para seguir a un deportista en concreto
        /// </summary>
        /// <param name="usuarioDeportista">el nombre de usuario del deportista que realiza la peticion</param>
        /// <param name="usuarioAmigo">el nombre de usuario del deportista a agreagar a amigos</param>
        public void seguirDeportista(string usuarioDeportista, string usuarioAmigo)
        {
            var amigoDeportista = new AmigoDeportista();
            amigoDeportista.Usuariodeportista = usuarioDeportista;
            amigoDeportista.Amigoid = usuarioAmigo;

            _context.Add(amigoDeportista);
        }

        /// <summary>
        /// Método para ver todas las carreras inscritas que tiene un deportista.
        /// Se valida que la carrera sea el día en que se hace la peticion
        /// </summary>
        /// <param name="usuarioDeportista">el usuario a verificar</param>
        /// <returns></returns>
        public List<Carrera> verCarrerasInscritas(string usuarioDeportista)
        {
            List<Carrera> carreras = new List<Carrera>();

            // se accede a las carrearas insscritas por el usuario
            var carrerasInscritas = _context.DeportistaCarrera.
                    Where(x => x.Usuariodeportista == usuarioDeportista && x.Completada == false).
                    Include(x => x.Carrera).ToList();

            foreach (var carrera in carrerasInscritas)
            {
                if ((int)(carrera.Carrera.Fecha - DateTime.Today).TotalDays == 0) // se valida si la carrera es el dia actual
                {
                    carreras.Add(carrera.Carrera);
                }          
            }
            return carreras;
        }

        /// <summary>
        /// Método para ver todos los retos que no han sido completados por el usuario
        /// </summary>
        /// <param name="usuarioDeportista">el deportista a verificar</param>
        /// <returns>La lista de retos que no han sido completados</returns>
        public List<Reto> verRetosIncompletos(string usuarioDeportista)
        {
            List<Reto> retos = new List<Reto>();

            var deportistaReto = _context.DeportistaReto.Where(x => x.Usuariodeportista == usuarioDeportista && x.Completado == false).
                Include(x => x.Reto).ToList();

            foreach (var reto in deportistaReto)
            {
                if ((reto.Reto.Periododisponibilidad - DateTime.Now).TotalDays >= 0)
                    retos.Add(reto.Reto);
            }

            return retos;
        }
        
        /// <summary>
        /// Método para ver los amigos que tiene asociados un deportista determinado
        /// </summary>
        /// <param name="usuarioDeportista">el deportista a validar</param>
        /// <returns>La lista con los deportistas asociados</returns>
        public List<Deportista> verAmigosAsociados(string usuarioDeportista)
        {
            List<Deportista> amigos = new List<Deportista>();

            // se accede a todos los amigos del deportista
            var amigoDeportista = _context.AmigoDeportista.
                   Where(x => x.Usuariodeportista == usuarioDeportista).
                   Include(x => x.Amigo).ToList();

            foreach(var amigo in amigoDeportista)
            {
                amigos.Add(amigo.Amigo);
            }

            return amigos;
        }

        /// <summary>
        /// Método para acceder a todos los deportistas NO amigos de un usuario específico
        /// </summary>
        /// <param name="usuarioDeportista">el deportista a validar</param>
        /// <returns>La lista de deportistas que NO son seguidos por el usuario</returns>
        public List<Deportista> mostrarTodosDeportistasNoAmigos(string usuarioDeportista)
        {
            // se accede a todos los deportistas amigos
            var amigos = verAmigosAsociados(usuarioDeportista);

            // se acceden a todos los deportistas de la base de datos
            var deportistasTotales = _context.Deportista.Where(x => x.Usuario != usuarioDeportista).ToList();

            List<Deportista> deportistasNoAmigos = new List<Deportista>();

            foreach (var deportista in deportistasTotales)
            {
                if (!amigos.Contains(deportista))
                    deportistasNoAmigos.Add(deportista);
            }
            return deportistasNoAmigos;
        }

        /// <summary>
        /// Método para registar una actividad asociada a un reto
        /// </summary>
        /// <param name="actividad">La actividad a registrar</param>
        /// <param name="usuarioDeportista">el deportista a validar</param>
        /// <returns>Un true si hay exito, false en caso contrario</returns>
        private bool registrarActividadReto(Actividad actividad, string usuarioDeportista)
        {
            // se accede al reto que el usuario quiere completar/avanzar
            var deportistaReto = _context.DeportistaReto.Where(x => x.Usuariodeportista == usuarioDeportista
            && x.Nombrereto == actividad.Nombreretocarrera && x.Admindeportista == actividad.Adminretocarrera).
            Include(x => x.Reto).FirstOrDefault();

            // se valida que el tipo de actividad del reto sea la misma que la actividad que hizo el deportista
            if (deportistaReto == null || deportistaReto.Reto.Tipoactividad != actividad.Tipoactividad)
                return false;

            _context.Add(actividad);

            if((actividad.Kilometraje + deportistaReto.Kmacumulados) >= deportistaReto.Reto.Kmtotales)
            {
                deportistaReto.Kmacumulados += actividad.Kilometraje; // se suma la cantidad de kilómetros de la actividad
                deportistaReto.Completado = true;
                _context.Update(deportistaReto);
                return true;
            }
            else
            {
                deportistaReto.Kmacumulados += actividad.Kilometraje; // se suma la cantidad de kilómetros de la actividad
                _context.Update(deportistaReto);
                return true;
            }

        }

        /// <summary>
        /// Método para registar una actividad asociada a una carrera
        /// </summary>
        /// <param name="actividad">la actividad a registar</param>
        /// <param name="usuarioDeportista">el deportista a validar</param>
        /// <returns>True si hubo exito, false en caso contrario</returns>
        private bool registrarActividadCarrera(Actividad actividad, string usuarioDeportista)
        {
            // se accede a la carrera la cual quiere completar el usuario
            var deportistaCarrera = _context.DeportistaCarrera.Where(x => x.Usuariodeportista == usuarioDeportista
            && x.Nombrecarrera == actividad.Nombreretocarrera && x.Admindeportista == actividad.Adminretocarrera).
            Include(x => x.Carrera).FirstOrDefault();

            // se valida que el tipo de actividad de la carrera sea la misma que la actividad que hizo el deportista
            if (deportistaCarrera == null || deportistaCarrera.Carrera.Tipoactividad != actividad.Tipoactividad)
                return false;

            if (deportistaCarrera.Carrera.Tipoactividad != actividad.Tipoactividad)
                return false;

            deportistaCarrera.Completada = true; // se marca como completada
            _context.Update(deportistaCarrera);
            _context.Add(actividad);
            return true;
        }

        /// <summary>
        /// Método para registarar una actividad. La clasifica de acuerdo a una banderilla
        /// en actividad reto, actividad carrera y actividad libre
        /// </summary>
        /// <param name="actividad">La actividad a registrar</param>
        /// <returns>True en caso de exito, false en caso contrario</returns>
        public bool registrarActividades(Actividad actividad)
        {
            if (actividad.Banderilla == 0)
            {
                return registrarActividadCarrera(actividad, actividad.Usuariodeportista);
            }
            else if (actividad.Banderilla == 1)
                return registrarActividadReto(actividad, actividad.Usuariodeportista);
            else
            {
                _context.Add(actividad);
                return true;
            }
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
