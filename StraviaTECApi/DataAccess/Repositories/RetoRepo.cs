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

        public List<Reto> verRetosAdministrados(string usuarioDeportista)
        {
            return _context.Reto.Where(x => x.Admindeportista == usuarioDeportista).ToList();
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
