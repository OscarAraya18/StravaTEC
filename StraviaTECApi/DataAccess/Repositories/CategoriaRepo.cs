using StraviaTECApi.Models;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Método para acceder a todas las categorías existentes en la base de datos
        /// </summary>
        /// <returns>La lista con todas las categorias existentes</returns>
        public List<Categoria> obtenerTodas()
        {
            return _context.Categoria.ToList();
        }

    }
}
