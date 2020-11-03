using EFConsole.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFConsole.DataAccess.Repositories
{
    class InscripcionRepo
    {

        private readonly StravaContext _context;

        // se inyecta el DB Context 
        public InscripcionRepo(StravaContext context)
        {
            _context = context;
        }

        /**
        * ------------------------------
        *         MÉTODOS CRUD
        * ------------------------------
        */

        public void Create(Inscripcion inscripcion, string nombreCarrera)
        {
            if (inscripcion == null)
                throw new System.ArgumentNullException(nameof(inscripcion));

            _context.Inscripcion.Add(inscripcion);

            var inscripcionCarrera = new InscripcionCarrera();
            inscripcionCarrera.Deportistainscripcion = inscripcion.Usuariodeportista;
            inscripcionCarrera.Idinscripcion = inscripcion.Id;
            inscripcionCarrera.Nombrecarrera = nombreCarrera;

            Console.WriteLine("Numero: " + inscripcion.Id);
            _context.Add(inscripcionCarrera);

        }

        public void Update(Inscripcion inscripcion)
        {
            if (inscripcion == null)
                throw new System.ArgumentNullException(nameof(inscripcion));

            _context.Inscripcion.Update(inscripcion);
            _context.Entry(inscripcion).State = EntityState.Modified;

        }

        public void aceptarInscripcion(int id)
        {
            var inscripcionCarrera = _context.InscripcionCarrera.
                   Where(x => x.Idinscripcion == id).
                   Include(x => x.NombrecarreraNavigation).
                   Include(x => x.Inscripcion).
                   ThenInclude(x => x.UsuariodeportistaNavigation).ToList();

            var deportistaCarrera = new DeportistaCarrera();
            var inscripcion = inscripcionCarrera[0].Inscripcion;
            var carrera = inscripcionCarrera[0].NombrecarreraNavigation;

            deportistaCarrera.Admindeportista = carrera.Admindeportista;
            deportistaCarrera.Nombrecarrera = carrera.Nombre;
            deportistaCarrera.Usuariodeportista = inscripcion.UsuariodeportistaNavigation.Usuario;
            deportistaCarrera.Completada = false;

            inscripcion.Estado = "Aceptado";
            _context.Entry(inscripcion).State = EntityState.Modified;
            _context.Add(deportistaCarrera);
        }

    }
}
