using Microsoft.EntityFrameworkCore;
using StraviaTECApi.Models;
using StraviaTECApi.Parsers;
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

        public bool Create(InscripcionParser inscripcionParser)
        {
            if (inscripcionParser == null)
                throw new ArgumentNullException(nameof(inscripcionParser));

            var inscripcion = new Inscripcion
            {
                Usuariodeportista = inscripcionParser.Usuariodeportista,
                Estado = inscripcionParser.Estado,
                Nombrecarrera = inscripcionParser.Nombrecarrera,
                Admincarrera = inscripcionParser.Admincarrera
            };

            if (inscripcionParser.Recibopago != null)
                inscripcion.Recibopago = Convert.FromBase64String(inscripcionParser.Recibopago);

            _context.Inscripcion.Add(inscripcion);

            var deportista = _context.Deportista.Where(x => x.Usuario == inscripcionParser.Usuariodeportista).FirstOrDefault();
            
            var carrera = _context.Carrera.Where(x => x.Nombre == inscripcion.Nombrecarrera 
                            && x.Admindeportista == inscripcion.Admincarrera).
                            Include(x => x.CarreraCategoria).FirstOrDefault();

            foreach (var categoria in carrera.CarreraCategoria)
            {
                if (categoria.Nombrecategoria == deportista.Nombrecategoria)
                {
                    var inscripcionCarrera = new InscripcionCarrera();
                    inscripcionCarrera.Deportistainscripcion = inscripcion.Usuariodeportista;
                    inscripcionCarrera.Estadoinscripcion = inscripcion.Estado;
                    inscripcionCarrera.Nombrecarrera = inscripcion.Nombrecarrera;
                    inscripcionCarrera.Admincarrera = inscripcion.Admincarrera;

                    _context.Add(inscripcionCarrera);
                    return true;
                }
            }

            return false;

        }

        public void Update(InscripcionParser inscripcionParser)
        {
            if (inscripcionParser == null)
                throw new ArgumentNullException(nameof(inscripcionParser));

            var inscripcion = new Inscripcion
            {
                Usuariodeportista = inscripcionParser.Usuariodeportista,
                Estado = inscripcionParser.Estado,
                Nombrecarrera = inscripcionParser.Nombrecarrera,
                Admincarrera = inscripcionParser.Admincarrera
            };

            if (inscripcionParser.Recibopago != null)
                inscripcion.Recibopago = Convert.FromBase64String(inscripcionParser.Recibopago);

            _context.Inscripcion.Update(inscripcion);
            _context.Entry(inscripcion).State = EntityState.Modified;

        }

        public void Delete(Inscripcion inscripcion)
        {
            if (inscripcion == null)
                throw new System.ArgumentNullException(nameof(inscripcion));

            _context.Inscripcion.Remove(inscripcion);

        }

        public void aceptarInscripcion(Inscripcion inscripcion)
        {
            
            var deportistaCarrera = new DeportistaCarrera();

            deportistaCarrera.Admindeportista = inscripcion.Admincarrera;
            deportistaCarrera.Nombrecarrera = inscripcion.Nombrecarrera;
            deportistaCarrera.Usuariodeportista = inscripcion.Usuariodeportista;
            deportistaCarrera.Completada = false;

            inscripcion.Estado = "Aceptado";

            _context.Update(inscripcion);
            _context.Add(deportistaCarrera);

        }

        public List<Inscripcion> verInscripcionesEspera(string usuarioDeportista)
        {
            return _context.Inscripcion.Where(x => x.Usuariodeportista == usuarioDeportista
                                              && x.Estado.Equals("En espera")).ToList();
        }

        public List<Inscripcion> verInscripcionesEsperaCarrera(string nombreCarrera, string admin)
        {
            return _context.Inscripcion.Where(x => x.Nombrecarrera == nombreCarrera
                                              && x.Estado.Equals("En espera") && x.Admincarrera == admin).ToList();
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
