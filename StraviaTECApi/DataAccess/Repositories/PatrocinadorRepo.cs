using EFConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFConsole.DataAccess.Repositories
{
    class PatrocinadorRepo
    {
        private readonly StravaContext _context;

        // se inyecta el DB Context 
        public PatrocinadorRepo(StravaContext context)
        {
            _context = context;
        }

        public List<Patrocinador> obtenerTodos()
        {
            return _context.Patrocinador.ToList();
        }
    }
}
