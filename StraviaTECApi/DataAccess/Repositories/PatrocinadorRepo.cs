using StraviaTECApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public List<Patrocinador> obtenerTodos()
        {
            return _context.Patrocinador.ToList();
        }
    }
}
