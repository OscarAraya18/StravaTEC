using Microsoft.EntityFrameworkCore;
using StraviaTECApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFConsole.DataAccess.Repositories
{
    public class InscripcionRepo
    {

        private readonly StraviaContext _context;

        // se inyecta el DB Context 
        public InscripcionRepo(StraviaContext context)
        {
            _context = context;
        }

        /**
        * ------------------------------
        *         MÉTODOS CRUD
        * ------------------------------
        */

        public bool Create(Inscripcion inscripcion, string nombreCarrera, string adminCarrera)
        {
            if (inscripcion == null)
                throw new ArgumentNullException(nameof(inscripcion));

            _context.Inscripcion.Add(inscripcion);

            var deportista = _context.Deportista.Where(x => x.Usuario == inscripcion.Usuariodeportista).FirstOrDefault();
            var carrera = _context.Carrera.Where(x => x.Nombre == nombreCarrera && x.Admindeportista == adminCarrera).
                            Include(x => x.CarreraCategoria).FirstOrDefault();

            foreach (var categoria in carrera.CarreraCategoria)
            {
                if (categoria.Nombrecategoria == deportista.Nombrecategoria)
                {
                    var inscripcionCarrera = new InscripcionCarrera();
                    inscripcionCarrera.Deportistainscripcion = inscripcion.Usuariodeportista;
                    inscripcionCarrera.Estadoinscripcion = inscripcion.Estado;
                    inscripcionCarrera.Nombrecarrera = nombreCarrera;

                    _context.Add(inscripcionCarrera);
                    return true;
                }
            }

            return false;

        }

        public void Update(Inscripcion inscripcion)
        {
            if (inscripcion == null)
                throw new System.ArgumentNullException(nameof(inscripcion));

            _context.Inscripcion.Update(inscripcion);
            _context.Entry(inscripcion).State = EntityState.Modified;

        }

        public void aceptarInscripcion(Inscripcion inscripcion)
        {
            var inscripcionCarrera = _context.InscripcionCarrera.
                   Where(x => x.Estadoinscripcion == inscripcion.Estado
                   && x.Deportistainscripcion == inscripcion.Usuariodeportista).FirstOrDefault();

            var deportistaCarrera = new DeportistaCarrera();

            deportistaCarrera.Admindeportista = inscripcionCarrera.Admincarrera;
            deportistaCarrera.Nombrecarrera = inscripcionCarrera.Nombrecarrera;
            deportistaCarrera.Usuariodeportista = inscripcion.Usuariodeportista;
            deportistaCarrera.Completada = false;

            var inscripcionAceptada = new Inscripcion
            {
                Usuariodeportista = inscripcion.Usuariodeportista,
                Estado = "Aceptado",
                Recibopago = inscripcion.Recibopago
            };

            _context.Remove(inscripcionCarrera);
            _context.Remove(inscripcion);
            _context.Add(inscripcionAceptada);
            _context.Add(deportistaCarrera);

        }

        public List<Inscripcion> verInscripcionesEspera(string usuarioDeportista)
        {
            return _context.Inscripcion.Where(x => x.Usuariodeportista == usuarioDeportista
                                              && x.Estado.Equals("En espera")).ToList();
        }

        public List<Inscripcion> verInscripcionesEsperaCarrera(string nombreCarrera, string admin)
        {
            List<Inscripcion> inscripciones = new List<Inscripcion>();

            var inscripcionCarrera = _context.InscripcionCarrera.Where(x => x.Nombrecarrera == nombreCarrera
                                              && x.Inscripcion.Estado.Equals("En espera") && x.Admincarrera == admin).
                                                Include(x => x.Inscripcion).ToList();

            foreach (var inscripcion in inscripcionCarrera)
            {
                inscripciones.Add(inscripcion.Inscripcion);
            }

            return inscripciones;
        }

        /**         
         * Save the changes made to the database
         */
        public bool SaveChanges()
        {
            try
            {
                return (_context.SaveChanges() >= 0);
            }
            catch
            {
                return false;
            }
        }

    }
}
