using Microsoft.EntityFrameworkCore;
using StraviaTECApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                throw new System.ArgumentNullException(nameof(grupo));

            _context.Grupo.Add(grupo);

        }

        public void Update(Grupo grupo)
        {
            if (grupo == null)
                throw new System.ArgumentNullException(nameof(grupo));

            _context.Grupo.Update(grupo);
            _context.Entry(grupo).State = EntityState.Modified;

        }

        public void Delete(string nombre)
        {
            var grupo = _context.Grupo.FirstOrDefault(x => x.Nombre == nombre);

            if (grupo == null)
            {
                throw new System.ArgumentNullException(nameof(grupo));
            }

            _context.Grupo.Remove(grupo);

        }

        public void agregarAgrupo(string usuarioDeportista, Grupo grupo)
        {
            var deportista = _context.Deportista.FirstOrDefault(x => x.Usuario == usuarioDeportista);
            // también se podria recibir el grupo directamente desde los argumentos

            var grupoAgregado = new GrupoDeportista();
            grupoAgregado.Admindeportista = grupo.Admindeportista;
            grupoAgregado.Nombregrupo = grupo.Nombre;
            grupoAgregado.Usuariodeportista = deportista.Usuario;
            _context.Add(grupoAgregado);
        }

        public List<Carrera> accederCarreras(string nombreGrupo)
        {
            List<Carrera> carreras = new List<Carrera>();

            var verCarreras = _context.GrupoCarrera.
                    Where(x => x.Nombregrupo == nombreGrupo).
                    Include(x => x.Carrera).ToList();


            foreach (var carrera in verCarreras)
            {
                carreras.Add(carrera.Carrera);
                Console.WriteLine(carrera.Carrera.Nombre);
            }
            return carreras;
            // se debe retornar el resultado
        }

        public List<Grupo> verMisGruposAdministrados(string usuarioDeportista)
        {
            return _context.Grupo.Where(x => x.Admindeportista == usuarioDeportista).ToList();
        }

        public List<Grupo> verTodos()
        {
            return _context.Grupo.ToList();
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
