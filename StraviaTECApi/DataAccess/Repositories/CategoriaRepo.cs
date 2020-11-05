using StraviaTECApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFConsole.DataAccess.Repositories
{
    public class CategoriaRepo
    {
        private readonly StraviaContext _context;

        // se inyecta el DB Context 
        public CategoriaRepo(StraviaContext context)
        {
            _context = context;
        }

        public List<Categoria> obtenerTodas()
        {
            return _context.Categoria.ToList();
        }

    }
}
