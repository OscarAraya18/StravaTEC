using Microsoft.EntityFrameworkCore;
using StraviaTECApi.Models;
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
                throw new System.ArgumentNullException(nameof(reto));

            _context.Reto.Add(reto);

        }

        public void Update(Reto reto)
        {
            if (reto == null)
                throw new System.ArgumentNullException(nameof(reto));

            _context.Reto.Update(reto);
            _context.Entry(reto).State = EntityState.Modified;

        }

        public void Delete(string nombre)
        {
            var reto = _context.Reto.FirstOrDefault(x => x.Nombre == nombre);

            if (reto == null)
            {
                throw new System.ArgumentNullException(nameof(reto));
            }

            _context.Reto.Remove(reto);

        }

        public List<Reto> verEstadoRetos(string UsuarioDeportista)
        {
            List<Reto> retos = new List<Reto>();

            var deportistaRetos = _context.DeportistaReto.
                    Where(x => x.Usuariodeportista == UsuarioDeportista).
                    Include(x => x.Reto).ToList();
            // se debe retornar el avance que tiene el deportista
            // el objetivo del reto
            // también los días faltantes
            foreach (var reto in deportistaRetos)
            {
                retos.Add(reto.Reto);
            }
            return retos;
        }

        public List<Reto> verRetosIncompletos(string UsuarioDeportista)
        {
            List<Reto> retos = new List<Reto>();

            var deportistaRetos = _context.DeportistaReto.
                    Where(x => x.Usuariodeportista == UsuarioDeportista && x.Completado == false).
                    Include(x => x.Reto).ToList();
            // se debe retornar el avance que tiene el deportista
            // el objetivo del reto
            // también los días faltantes
            foreach (var reto in deportistaRetos)
            {
                retos.Add(reto.Reto);
            }
            return retos;
        }
        public List<Reto> verRetosAdministrados(string usuarioDeportista)
        {
            return _context.Reto.Where(x => x.Admindeportista == usuarioDeportista).ToList();
        }

        public void inscribirReto(string  adminReto, string nombreReto, string usuarioDeportista)
        {
            var deportistaReto = new DeportistaReto();

            deportistaReto.Admindeportista = adminReto;
            deportistaReto.Completado = false;
            deportistaReto.Kmacumulados = 0;
            deportistaReto.Usuariodeportista = usuarioDeportista;
            deportistaReto.Nombrereto = nombreReto;

            _context.Add(deportistaReto);
        }

        public List<Reto> verRetosDisponibles(string usuarioDeportista)
        {
            List<Reto> retosNoInscritos = new List<Reto>();

            var retosInscritos = verRetosInscritos(usuarioDeportista);

            var retosPublicos = _context.Reto.Where(x => x.Privacidad == false).ToList();

            var grupos = _context.GrupoDeportista.Where(x => x.Usuariodeportista == usuarioDeportista).Include(x => x.Grupo);

            var retosPrivados = _context.GrupoReto.
                    Include(x => x.Reto).
                    Where(x => x.Reto.Privacidad == true).ToList();

            foreach (var reto in retosPrivados)
            {
                foreach (var grupo in grupos)
                {
                    if (grupo.Nombregrupo.Equals(reto.Nombregrupo) && grupo.Admindeportista.Equals(reto.Admingrupo)
                        && !retosInscritos.Contains(reto.Reto))
                    {
                        retosNoInscritos.Add(reto.Reto);
                        break;
                    }
                }
            }

            foreach (var reto in retosPublicos)
            {
                if (!retosNoInscritos.Contains(reto))
                    retosNoInscritos.Add(reto);
            }

            return retosNoInscritos;
        }

        public List<Reto> verRetosInscritos(string usuarioDeportista)
        {
            List<Reto> retos = new List<Reto>();

            var deportistaReto = _context.DeportistaReto.Where(x => x.Usuariodeportista == usuarioDeportista).
                Include(x => x.Reto).ToList();

            foreach (var reto in deportistaReto)
            {
                
               retos.Add(reto.Reto);
            }

            return retos;
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
