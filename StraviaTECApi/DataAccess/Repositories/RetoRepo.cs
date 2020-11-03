using EFConsole.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFConsole.DataAccess.Repositories
{
    class RetoRepo
    {
        private readonly StravaContext _context;

        // se inyecta el DB Context 
        public RetoRepo(StravaContext context)
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

        public void verEstadoReto(string nombreReto, string UsuarioDeportista)
        {
            var reto = _context.Reto.FirstOrDefault(x => x.Nombre == nombreReto);
            // se debe retornar el avance que tiene el deportista
            // el objetivo del reto
            // también los días faltantes
        }

        public List<Reto> verRetosAdministrados(string usuarioDeportista)
        {
            return _context.Reto.Where(x => x.Admindeportista == usuarioDeportista).ToList();
        }
    }
}
