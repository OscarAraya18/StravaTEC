using StraviaTECApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace EFConsole.DataAccess.Repositories
{
    public class PatrocinadorRepo
    {
        private readonly StraviaContext _context;

        // se inyecta el DB Context 
        public PatrocinadorRepo(StraviaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método para obtener todos los patrocinadores de la base de datos
        /// </summary>
        /// <returns>la lista con todos los patrocinadores</returns>
        public List<Patrocinador> obtenerTodos()
        {
            return _context.Patrocinador.ToList();
        }
    }
}
