using EFConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFConsole.DataAccess.Repositories
{
    class CategoriaRepo
    {
        private readonly StravaContext _context;

        // se inyecta el DB Context 
        public CategoriaRepo(StravaContext context)
        {
            _context = context;
        }

        public List<Categoria> obtenerTodas()
        {
            return _context.Categoria.ToList();
        }
    }
}
