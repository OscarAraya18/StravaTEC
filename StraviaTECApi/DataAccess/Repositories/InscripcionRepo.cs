﻿using Microsoft.EntityFrameworkCore;
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


            // se crea un ainscripción para guardar en la base de datos
            var inscripcion = new Inscripcion
            {
                Usuariodeportista = inscripcionParser.Usuariodeportista,
                Estado = inscripcionParser.Estado,
                Nombrecarrera = inscripcionParser.Nombrecarrera,
                Admincarrera = inscripcionParser.Admincarrera
            };

            // si viene un recibo en base64 hay que parsearlo a byte array
            if (inscripcionParser.Recibopago != null)
                inscripcion.Recibopago = Convert.FromBase64String(inscripcionParser.Recibopago);

            _context.Inscripcion.Add(inscripcion);

            return true;
        }

        public void Update(InscripcionParser inscripcionParser)
        {
            if (inscripcionParser == null)
                throw new ArgumentNullException(nameof(inscripcionParser));

            // se crea una inscripción a guardar en la base de datos
            var inscripcion = new Inscripcion
            {
                Usuariodeportista = inscripcionParser.Usuariodeportista,
                Estado = inscripcionParser.Estado,
                Nombrecarrera = inscripcionParser.Nombrecarrera,
                Admincarrera = inscripcionParser.Admincarrera
            };

            // si viene un recibo en base64 hay que parsearlo a byte array
            if (inscripcionParser.Recibopago != null)
                inscripcion.Recibopago = Convert.FromBase64String(inscripcionParser.Recibopago);

            _context.Inscripcion.Update(inscripcion);
            _context.Entry(inscripcion).State = EntityState.Modified;

        }

        public void Delete(string nombreCarerra, string adminDeportista, string usuario)
        {
            var inscripcion = _context.Inscripcion.Where(x => x.Admincarrera == adminDeportista &&
            x.Nombrecarrera == nombreCarerra && x.Usuariodeportista == usuario).FirstOrDefault();
            
            if (inscripcion == null)
                throw new ArgumentNullException(nameof(inscripcion));

            _context.Inscripcion.Remove(inscripcion);

        }

        /// <summary>
        /// Método para aceptar una inscripción 
        /// </summary>
        /// <param name="inscripcion">la inscripción a aceptar</param>
        public void aceptarInscripcion(Inscripcion inscripcion)
        {
            // se crea una relacion entre deportista y carrera
            var deportistaCarrera = new DeportistaCarrera();

            deportistaCarrera.Admindeportista = inscripcion.Admincarrera;
            deportistaCarrera.Nombrecarrera = inscripcion.Nombrecarrera;
            deportistaCarrera.Usuariodeportista = inscripcion.Usuariodeportista;
            deportistaCarrera.Completada = false;

            inscripcion.Estado = "Aceptado"; // se marca como aceptada la inscripción

            _context.Update(inscripcion);
            _context.Add(deportistaCarrera);

        }

        /// <summary>
        /// Método para ver las inscripciones en espera para un determinado deportista
        /// </summary>
        /// <param name="usuarioDeportista">el deportista a validar</param>
        /// <returns>La lista de inscripciones en espera</returns>
        public List<Inscripcion> verInscripcionesEspera(string usuarioDeportista)
        {
            return _context.Inscripcion.Where(x => x.Usuariodeportista == usuarioDeportista
                                              && x.Estado.Equals("En espera")).ToList();
        }

        /// <summary>
        /// Método para ver las inscripciones en espera que tiene una carrera en específico
        /// </summary>
        /// <param name="nombreCarrera">el nombre de la carrera</param>
        /// <param name="admin">el administrador de la carrera</param>
        /// <returns>La lista de inscripciones en espera para esa carrera</returns>
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
