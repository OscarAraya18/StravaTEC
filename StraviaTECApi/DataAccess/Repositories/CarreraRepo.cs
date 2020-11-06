using Microsoft.EntityFrameworkCore;
using StraviaTECApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFConsole.DataAccess.Repositories
{
    public class CarreraRepo
    {
        private readonly StraviaContext _context;

        // se inyecta el DB Context 
        public CarreraRepo(StraviaContext context)
        {
            _context = context;
        }

        /**
         * ------------------------------
         *         MÉTODOS CRUD
         * ------------------------------
         */

        public void Create(Carrera carrera)
        {
            if (carrera == null)
                throw new ArgumentNullException(nameof(carrera));

            _context.Carrera.Add(carrera);

        }

        public void Update(Carrera carrera)
        {
            if (carrera == null)
                throw new ArgumentNullException(nameof(carrera));

            _context.Carrera.Update(carrera);
            _context.Entry(carrera).State = EntityState.Modified;

        }

        public void Delete(string nombre)
        {
            var carrera = _context.Carrera.FirstOrDefault(x => x.Nombre == nombre);

            if (carrera == null)
            {
                throw new ArgumentNullException(nameof(carrera));
            }

            _context.Carrera.Remove(carrera);

        }

        public void verPosicionesCarrera(string nombre)
        {
            var carrera = _context.Carrera.FirstOrDefault(x => x.Nombre == nombre);
            // se tienen que buscar los participantes de la carrera
            // y luego ordenar las posiciones de acuerdo al tiempo
        }

        public void verParticipantesPorCarrera(string nombre)
        {
            var carrera = _context.Carrera.FirstOrDefault(x => x.Nombre == nombre);
            // se debe acceder a cada categoria que tiene esa carrera
            // por cada categoría se accede a los deportistas asociados
        }

        public void verPosicionesPorCarrera(string nombre)
        {
            var carrera = _context.Carrera.FirstOrDefault(x => x.Nombre == nombre);
            // se debe acceder a cada categoria que tiene esa carrera
            // por cada categoría se accede a los deportistas asociados
            // se debe ordenar de acuerdo al tiempo registrado (de menor a mayor)
        }

        public void verCategorias(string nombreCarrera)
        {
            List<Categoria> categorias = new List<Categoria>();

            var carreraCategorias = _context.CarreraCategoria.
                    Where(x => x.Nombrecarrera == nombreCarrera).
                    Include(x => x.NombrecategoriaNavigation).ToList();

            foreach (var c in carreraCategorias)
            {
                Console.WriteLine(c.NombrecategoriaNavigation.Descripcion);
                categorias.Add(c.NombrecategoriaNavigation);
            }
        }

        public List<Carrera> verMisCarreras(string usuarioDeportista)
        {
            return _context.Carrera.Where(x => x.Admindeportista == usuarioDeportista).ToList();
            // se retorna la lista
        }

        public List<Carrera> verTodas()
        {
            return _context.Carrera.ToList();
            // se retorna la lista
        }

        public void verPatrocinadores(string nombreCarrera)
        {
            List<Patrocinador> patrocinadores = new List<Patrocinador>();

            var carreraPatrocinadores = _context.CarreraPatrocinador.
                    Where(x => x.Nombrecarrera == nombreCarrera).
                    Include(x => x.NombrepatrocinadorNavigation).ToList();

            foreach (var p in carreraPatrocinadores)
            {
                Console.WriteLine(p.NombrepatrocinadorNavigation.Nombrecomercial);
                patrocinadores.Add(p.NombrepatrocinadorNavigation);
            }
            // se retorna la lista
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
