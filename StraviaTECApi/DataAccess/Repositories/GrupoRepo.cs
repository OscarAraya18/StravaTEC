using Microsoft.EntityFrameworkCore;
using StraviaTECApi.Models;
using StraviaTECApi.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFConsole.DataAccess.Repositories
{
    public class GrupoRepo
    {
        private readonly StraviaContext _context;

        // se inyecta el DB Context 
        public GrupoRepo(StraviaContext context)
        {
            _context = context;
        }

        /**
        * ------------------------------
        *         MÉTODOS CRUD
        * ------------------------------
        */

        public void Create(Grupo grupo)
        {
            if (grupo == null)
                throw new ArgumentNullException(nameof(grupo));

            _context.Grupo.Add(grupo);

        }

        public void Update(Grupo grupo)
        {
            if (grupo == null)
                throw new ArgumentNullException(nameof(grupo));

            _context.Grupo.Update(grupo);
            _context.Entry(grupo).State = EntityState.Modified;
        }

        public void Delete(int idGrupo, string admin)
        {
            var grupo = _context.Grupo.FirstOrDefault(x => x.Id == idGrupo && x.Admindeportista == admin);

            if (grupo == null)
            {
                throw new System.ArgumentNullException(nameof(grupo));
            }

            _context.Grupo.Remove(grupo);

        }

        /// <summary>
        /// método para agregar un deportista a un determinado grupo
        /// </summary>
        /// <param name="usuarioDeportista"> el usuario que se quiere agregar</param>
        /// <param name="grupo"> el grupo al cual se agrega el usuario</param>
        public void agregarAgrupo(string usuarioDeportista, Grupo grupo)
        {
           // se crea una relacion para grupo y deportista
            var grupoAgregado = new GrupoDeportista();
            grupoAgregado.Admindeportista = grupo.Admindeportista;
            grupoAgregado.Idgrupo = grupo.Id;
            grupoAgregado.Usuariodeportista = usuarioDeportista;
            _context.Add(grupoAgregado);
        }

        /// <summary>
        /// Método para acceder a todos los grupos que existen en la base de datos
        /// </summary>
        /// <returns>Una lista con todos los grupos que hay</returns>
        public List<Grupo> verTodosLosGrupos()
        {
            return _context.Grupo.ToList();
        }
        /// <summary>
        /// Método para acceder a todos los grupos en donde
        /// un deportista aún no esté asociado
        /// </summary>
        /// <param name="usuarioDeportista"> el deportista a verificar</param>
        /// <returns>Una lista con los grupos donde el deportista no está inscrito</returns>
        public List<Grupo> verTodosLosGruposNoInscritos(string usuarioDeportista)
        {
            List<Grupo> gruposNoInscritos = new List<Grupo>();
            
            List<Grupo> gruposInscritos = new List<Grupo>();

            //Se accede a los grupos inscritos por el deportista
            var grupoDeportista = _context.GrupoDeportista.
                Where(x => x.Usuariodeportista == usuarioDeportista).
                Include(x => x.Grupo).ToList();

            // se acceden a todos los grupos de la base de datos
            var gruposTotales = _context.Grupo.ToList();

            foreach(var grupo in grupoDeportista)
            {
                gruposInscritos.Add(grupo.Grupo);
            }

            foreach(var grupo in gruposTotales)
            {
                if (!gruposInscritos.Contains(grupo)) //si no está en los grupos inscritos se agrega
                    gruposNoInscritos.Add(grupo);
            }

            return gruposNoInscritos;
        }

        /// <summary>
        /// Método para aaceder a los retos de un determinado gupo
        /// </summary>
        /// <param name="idGrupo">id del grupo al cual se quiere acceder</param>
        /// <param name="usuario">administrador del grupo al cual se quiere acceder</param>
        /// <returns>La lista con todos los retos que son visibles para ese grupo</returns>
        public List<Reto> accederRetos(int idGrupo, string usuario)
        {
            List<Reto> retos = new List<Reto>();

            // revisa los grupos a los cuales el reto es visible
            var verRetos = _context.GrupoReto.
                    Where(x => x.Idgrupo == idGrupo).
                    Include(x => x.Reto).
                    Where(x => x.Reto.Privacidad == true).ToList();

            // se accede a los retos inscritos por el usuario
            var retosInscritos = verRetosInscritos(usuario);

            //se accede a todos los retos públicos
            var retosPublicos = _context.Reto.Where(x => x.Privacidad == false).ToList();

            foreach (var reto in verRetos)
            {
                if (!retosInscritos.Contains(reto.Reto))
                    retos.Add(reto.Reto);
            }

            foreach (var reto in retosPublicos)
            {
                if (!retosInscritos.Contains(reto))
                    retos.Add(reto);
            }

            return retos;
            // se debe retornar el resultado
        }

        /// <summary>
        /// Método para acceder a todos los grupos disponibles para un grupo concreto
        /// </summary>
        /// <param name="idGrupo">Id del grupo al cual se quiere acceder</param>
        /// <param name="usuario"> usuario a validar carreras inscritas</param>
        /// <returns></returns>
        public List<Carrera> accederCarreras(int idGrupo, string usuario)
        {
            List<Carrera> carreras = new List<Carrera>();

            // se acceden a los grupos visibles para una determinada carrera
            var verCarreras = _context.GrupoCarrera.
                    Where(x => x.Idgrupo == idGrupo).
                    Include(x => x.Carrera).
                    ThenInclude(x => x.CarreraCuentabancaria).
                    Where(x => x.Carrera.Privacidad == true).ToList();

            // se acceden a las carreras inscritas por el usuario
            var carrerasInscritas = verCarrerasInscritas(usuario);

            // se acceden a las carreras públicas
            var carrerasPublicas = _context.Carrera.Where(x => x.Privacidad == false).
                                    Include(x => x.CarreraCuentabancaria).ToList();

            foreach (var carrera in verCarreras)
            {
                if(!carrerasInscritas.Contains(carrera.Carrera))
                    carreras.Add(carrera.Carrera);
            }

            foreach(var carrera in carrerasPublicas)
            {
                if (!carrerasInscritas.Contains(carrera))
                    carreras.Add(carrera);
            }

            return carreras;
            // se debe retornar el resultado
        }

        /// <summary>
        /// Método para ver las carreras inscritas por determinado usuario
        /// </summary>
        /// <param name="usuarioDeportista">usuario a validar</param>
        /// <returns>Unba lista con las carreras inscritas por el deportista</returns>
        public List<Carrera> verCarrerasInscritas(string usuarioDeportista)
        {
            List<Carrera> carreras = new List<Carrera>();

            // se accede a las carreras inscritas por el usuario
            var carrerasInscritas = _context.DeportistaCarrera.
                    Where(x => x.Usuariodeportista == usuarioDeportista).
                    Include(x => x.Carrera).ToList();

            foreach (var carrera in carrerasInscritas)
            {

                carreras.Add(carrera.Carrera);


            }
            // se debe retornar el resultado
            return carreras;
        }

        /// <summary>
        /// Método para acceder a los retos inscritos por determinado usuario
        /// </summary>
        /// <param name="usuarioDeportista">usuario a validar</param>
        /// <returns>la lista de retos inscritos</returns>
        public List<Reto> verRetosInscritos(string usuarioDeportista)
        {
            List<Reto> retos = new List<Reto>();

            // se acceden a los retos inscritos por el deportista
            var retosInscritos = _context.DeportistaReto.
                    Where(x => x.Usuariodeportista == usuarioDeportista).
                    Include(x => x.Reto).ToList();

            foreach (var reto in retosInscritos)
            {
                retos.Add(reto.Reto);
            }
            // se debe retornar el resultado
            return retos;
        }

        /// <summary>
        /// Método para acceder a los grupos administrados por determinado deportista
        /// </summary>
        /// <param name="usuarioDeportista">el deportista a validar</param>
        /// <returns>La lista con los grupos administrados</returns>
        public List<Grupo> verMisGruposAdministrados(string usuarioDeportista)
        {
            return _context.Grupo.Where(x => x.Admindeportista == usuarioDeportista).ToList();
        }

        /// <summary>
        /// Método para ver todos los grupos a los cuales está asociado determinado deportista
        /// </summary>
        /// <param name="usuarioDeportista">usuario a validar</param>
        /// <returns>La lista de los grupos asociados</returns>
        public List<Grupo> verMisGruposAsociados(string usuarioDeportista)
        {
            List<Grupo> grupos = new List<Grupo>();

            // se accede a todos los grupos asociados el deportista
            var gruposDeportista = _context.GrupoDeportista.
                Where(x => x.Usuariodeportista == usuarioDeportista).
                Include(x => x.Grupo).ToList();

            foreach(var grupo in gruposDeportista)
            {
                grupos.Add(grupo.Grupo);
            }

            return grupos;
        }

        /// <summary>
        /// Método para ver los grupos alos cuales un deportista NO está asociado
        /// </summary>
        /// <param name="nombreUsuario">usuario a validar</param>
        /// <returns>La lista de grupos NO asociados</returns>
        public List<Grupo> verTodosNoAsociados(string nombreUsuario)
        {
            // se accede a los grupos asociados
            var gruposAsociados = verMisGruposAsociados(nombreUsuario);

            // se accede a todos los grupos de la base de datos
            var gruposTotales = _context.Grupo.ToList();

            List<Grupo> gruposNoAsociados = new List<Grupo>();

            foreach(var grupo in gruposTotales)
            {
                if (!gruposAsociados.Contains(grupo))
                    gruposNoAsociados.Add(grupo);
            }

            return gruposNoAsociados;
        }

        /// <summary>
        /// Método para buscar un grupo por nombre. Valida que el usuario no esté inscrito
        /// en los grupos que pueda retornar
        /// </summary>
        /// <param name="nombreGrupo">El nombre del grupo a buscar</param>
        /// <param name="usuario">usuario para validar</param>
        /// <returns>Lista de grupos con el resultado de la búsqueda</returns>
        public List<Grupo> buscarPorNombre(string nombreGrupo, string usuario)
        {
            List<Grupo> gruposEncontradosNoAsociados = new List<Grupo>();

            // se accede a los grupos asociados
            var gruposAsociados = verMisGruposAsociados(usuario);

            // se accede a los grupos asociados al criterio de búsqueda
            var gruposEncontrados = _context.Grupo.Where(x => x.Nombre.Contains(nombreGrupo)).ToList();

            foreach (var grupo in gruposEncontrados)
            {
                if (!gruposAsociados.Contains(grupo))
                    gruposEncontradosNoAsociados.Add(grupo);
            }

            return gruposEncontradosNoAsociados;
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
