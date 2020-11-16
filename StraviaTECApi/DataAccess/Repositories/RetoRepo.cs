using Microsoft.EntityFrameworkCore;
using StraviaTECApi.Models;
using StraviaTECApi.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFConsole.DataAccess.Repositories
{
    public class RetoRepo
    {
        private readonly StraviaContext _context;

        // se inyecta el DB Context 
        public RetoRepo(StraviaContext context)
        {
            _context = context;
        }

        /**
        * ------------------------------
        *         MÉTODOS CRUD
        * ------------------------------
        */

        public void Create(Reto reto)
        {
            if (reto == null)
                throw new ArgumentNullException(nameof(reto));

            _context.Reto.Add(reto);

        }

        public void Update(Reto reto)
        {
            if (reto == null)
                throw new ArgumentNullException(nameof(reto));

            _context.Reto.Update(reto);
            _context.Entry(reto).State = EntityState.Modified;

        }

        public void Delete(string nombre, string admin)
        {
            var reto = _context.Reto.FirstOrDefault(x => x.Nombre == nombre && x.Admindeportista == admin);

            if (reto == null)
            {
                throw new ArgumentNullException(nameof(reto));
            }
            _context.Reto.Remove(reto);
        }

        /// <summary>
        /// Método para ver el estado de los retos de un deportista en específico
        /// </summary>
        /// <param name="UsuarioDeportista">el deportista a verificar</param>
        /// <returns>la lista de retos con su respectivo estado</returns>
        public List<RetoParser> verEstadoRetos(string UsuarioDeportista)
        {
            List<Reto> retos = new List<Reto>();

            // se accede a los retos inscritos por el deportista
            var deportistaRetos = _context.DeportistaReto.
                    Where(x => x.Usuariodeportista == UsuarioDeportista).
                    Include(x => x.Reto).
                    ThenInclude(x => x.RetoPatrocinador).ToList();

            foreach (var reto in deportistaRetos)
            {
                retos.Add(reto.Reto);
            }
            return generarJSONRetos(retos);
        }

        /// <summary>
        /// Método para ver los retos que están inclompletos para un deportista en específico
        /// </summary>
        /// <param name="UsuarioDeportista">el deportista a validar</param>
        /// <returns>Lal ista de retos que aún no han sido completados</returns>
        public List<Reto> verRetosIncompletos(string UsuarioDeportista)
        {
            List<Reto> retos = new List<Reto>();

            var deportistaRetos = _context.DeportistaReto.
                    Where(x => x.Usuariodeportista == UsuarioDeportista && x.Completado == false).
                    Include(x => x.Reto).ToList();

            foreach (var reto in deportistaRetos)
            {
                retos.Add(reto.Reto);
            }
            return retos;
        }

        /// <summary>
        /// Método para ver todos los retos administrados por un deportista en específico
        /// </summary>
        /// <param name="usuarioDeportista">el deportista a validar</param>
        /// <returns>La lista de retos administrados</returns>
        public List<Reto> verRetosAdministrados(string usuarioDeportista)
        {
            return _context.Reto.Where(x => x.Admindeportista == usuarioDeportista).ToList();
        }

        /// <summary>
        /// Método para acceder a un reto mediante su nombre
        /// </summary>
        /// <param name="admin">el administrador del reto</param>
        /// <param name="nombreReto">el nombre del reto</param>
        /// <returns>El reto encontrado</returns>
        public Reto verRetoPorNombre(string admin, string nombreReto)
        {
            return _context.Reto.Where(x => x.Admindeportista == admin && x.Nombre == nombreReto).
                Include(x => x.GrupoReto).
                Include(x => x.RetoPatrocinador).FirstOrDefault();
        }

        /// <summary>
        /// Método para inscribir un deportista a un determinado reto
        /// </summary>
        /// <param name="adminReto">el administrador del reto</param>
        /// <param name="nombreReto">el nombre del reto</param>
        /// <param name="usuarioDeportista">el usuario a inscribir</param>
        public void inscribirReto(string  adminReto, string nombreReto, string usuarioDeportista)
        {
            // se crea una relación entre el deportista y el reto
            var deportistaReto = new DeportistaReto();

            deportistaReto.Admindeportista = adminReto;
            deportistaReto.Completado = false;
            deportistaReto.Kmacumulados = 0;
            deportistaReto.Usuariodeportista = usuarioDeportista;
            deportistaReto.Nombrereto = nombreReto;

            _context.Add(deportistaReto);
        }

        /// <summary>
        /// Método para ver los retos disponibles para un determinado usuario. Valida que el usuario NO esté inscrito en
        /// esos retos y que si el reto es privado, el deportista pertenezca a un grupo en donde el reto sea visible
        /// </summary>
        /// <param name="usuarioDeportista">el deportista a validar</param>
        /// <returns></returns>
        public List<RetoParser> verRetosDisponibles(string usuarioDeportista)
        {
            List<Reto> retosNoInscritos = new List<Reto>();

            // se acceden los retos inscritos
            var retosInscritos = verRetosInscritos(usuarioDeportista);

            // se acceden los retos públicos
            var retosPublicos = _context.Reto.Where(x => x.Privacidad == false).
                                Include(x => x.RetoPatrocinador).ToList();

            // se acceden los grupos asociados que tiene el deportista
            var grupos = _context.GrupoDeportista.Where(x => x.Usuariodeportista == usuarioDeportista).
                         Include(x => x.Grupo);

            // se accede a los retos privados, incluyendo los grupos a los cuales son visibles
            var retosPrivados = _context.GrupoReto.
                                Include(x => x.Reto).
                                ThenInclude(x => x.RetoPatrocinador).
                                Where(x => x.Reto.Privacidad == true).ToList();

            foreach (var reto in retosPrivados)
            {
                foreach (var grupo in grupos)
                {
                    if (grupo.Idgrupo == reto.Idgrupo && grupo.Admindeportista.Equals(reto.Admingrupo)
                        && !retosInscritos.Contains(reto.Reto))
                    {
                        retosNoInscritos.Add(reto.Reto);
                        break;
                    }
                }
            }

            foreach (var reto in retosPublicos)
            {
                if (!retosNoInscritos.Contains(reto) && !retosInscritos.Contains(reto))
                    retosNoInscritos.Add(reto);
            }
            // se genera un JSON específico para la parte del deportista
            return generarJSONRetos(retosNoInscritos);
        }

        /// <summary>
        /// Método para ver los retos en los cuales el deportista está inscrito
        /// </summary>
        /// <param name="usuarioDeportista">el deportista avalidar</param>
        /// <returns>La lista de retos inscritos por el usuario</returns>
        public List<Reto> verRetosInscritos(string usuarioDeportista)
        {
            List<Reto> retos = new List<Reto>();

            // se accede a los retos inscritos por el usuario
            var deportistaReto = _context.DeportistaReto.Where(x => x.Usuariodeportista == usuarioDeportista).
                Include(x => x.Reto).ThenInclude(x => x.RetoPatrocinador).ToList();

            foreach (var reto in deportistaReto)
            {
                
               retos.Add(reto.Reto);
            }

            return retos;
        }

        /// <summary>
        /// Método para generar los JSON adaptados a las necesidades del frontEnd y que se pueda accesar la
        /// información de mejor manera
        /// </summary>
        /// <param name="listaRetos">La lista de retos a parsear</param>
        /// <returns>El nuevo JSON adaptado</returns>
        public List<RetoParser> generarJSONRetos(List<Reto> listaRetos)
        {
            List<RetoParser> retosParser = new List<RetoParser>();

            // se accede a todos los patrocinadores de la base de datos
            var patrocinadores = _context.Patrocinador.ToList();

            foreach(var reto in listaRetos)
            {
                // se crea un parser para el reto
                var retoParser = new RetoParser
                {
                    nombreReto = reto.Nombre,
                    adminReto = reto.Admindeportista,
                    fondoAltitud = reto.Fondoaltitud,
                    tipoActividad = reto.Tipoactividad,
                    privacidad = reto.Privacidad,
                    kmTotales = reto.Kmtotales,
                    descripcion = reto.Descripcion,
                    fechaLimite = reto.Periododisponibilidad,
                    diasFaltantes = (int)(reto.Periododisponibilidad - DateTime.Today).TotalDays
                };
                // se agrega el estado actual del reto
                if(reto.DeportistaReto != null)
                {
                    retoParser.kmAcumulados = reto.DeportistaReto.First().Kmacumulados;
                    retoParser.completado = reto.DeportistaReto.First().Completado;
                }

                foreach(var patrocinador in reto.RetoPatrocinador)
                {
                    // se accede a los patrocinadores de cada reto
                    var retoPatrocinador = patrocinadores.
                        Where(x => x.Nombrecomercial == patrocinador.Nombrepatrocinador).
                        FirstOrDefault();

                    // se crea un parser para el reto
                    retoParser.RetoPatrocinador.Add(new PatrocinadorParser
                    {
                        Nombrecomercial = retoPatrocinador.Nombrecomercial,
                        Nombrerepresentante = retoPatrocinador.Nombrerepresentante,
                        Logo = retoPatrocinador.Logo,
                        Numerotelrepresentante = retoPatrocinador.Numerotelrepresentante
                    });
                }

                // se agrega el parser del reto a la lista
                retosParser.Add(retoParser);
            }
            return retosParser;
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
