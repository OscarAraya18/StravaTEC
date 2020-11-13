using Microsoft.AspNetCore.Mvc;
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

        public void Delete(string nombre, string admin)
        {
            var carrera = _context.Carrera.FirstOrDefault(x => x.Nombre == nombre && x.Admindeportista == admin);

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

        public List<Carrera> verCarreraPorNombre(string adminCarrera, string nombreCarrera)
        {
            return _context.Carrera.Where(x => x.Admindeportista == adminCarrera && x.Nombre.Contains(nombreCarrera)).
                Include(x => x.CarreraCuentabancaria).
                Include(x => x.CarreraPatrocinador).
                Include(x => x.GrupoCarrera).
                Include(x => x.CarreraCategoria).ToList();
        }
        public List<Carrera> verCarrerasInscritas(string usuarioDeportista)
        {
            List<Carrera> carreras = new List<Carrera>();

            var carrerasInscritas = _context.DeportistaCarrera.
                    Where(x => x.Usuariodeportista == usuarioDeportista).
                    Include(x => x.Carrera).
                    ThenInclude(x => x.CarreraCuentabancaria).
                    Include(x => x.Carrera).
                    ThenInclude(x => x.CarreraPatrocinador).ToList();

            foreach (var carrera in carrerasInscritas)
            {
                carreras.Add(carrera.Carrera);
               
            }

            return carreras;
        }

        public List<Carrera> verCarrerasNoInscritas(string usuarioDeportista)
        {
            var carrerasInscritas = verCarrerasInscritas(usuarioDeportista);

            var carrerasTotales = _context.Carrera.ToList();

            List<Carrera> carrerasNoInscritas = new List<Carrera>();

            foreach (var carrera in carrerasTotales)
            {
                if (!carrerasInscritas.Contains(carrera))
                    carrerasNoInscritas.Add(carrera);
            }

            return carrerasNoInscritas;
        }

        public List<Carrera> verCarrerasDisponibles(string usuario)
        {
            List<Carrera> carreras = new List<Carrera>();

            var grupos = _context.GrupoDeportista.Where(x => x.Usuariodeportista == usuario).Include(x => x.Grupo).ToList();

            var carrerasPublicas = _context.Carrera.Where(x => x.Privacidad == false).Include(x => x.CarreraCuentabancaria).
                                    Include(x => x.CarreraCategoria).
                                    Include(x => x.CarreraPatrocinador).ToList();

            var carrerasInscritas = verCarrerasInscritas(usuario);

            var carrerasPrivadas = _context.GrupoCarrera.
                    Include(x => x.Carrera).
                    ThenInclude(x => x.CarreraCuentabancaria).
                    Include(x => x.Carrera).
                    ThenInclude(x => x.CarreraCategoria).
                    Include(x => x.Carrera).
                    ThenInclude(x => x.CarreraPatrocinador).ToList();

            foreach(var carrera in carrerasPrivadas)
            {
                foreach(var grupo in grupos)
                {
                    if(grupo.Nombregrupo.Equals(carrera.Nombregrupo) && grupo.Admindeportista.Equals(carrera.Admingrupo)
                        && !carrerasInscritas.Contains(carrera.Carrera))
                    {
                       carreras.Add(carrera.Carrera);
                        break;
                    }
                }
            }

            foreach(var carrera in carrerasPublicas)
            {
                if (!carrerasInscritas.Contains(carrera))
                    carreras.Add(carrera);
            }  

            return carreras;
        }

        public List<Carrera> accederCarreras(string nombreGrupo, string adminGrupo, string usuario)
        {
            List<Carrera> carreras = new List<Carrera>();

            var verCarreras = _context.GrupoCarrera.
                    Where(x => x.Nombregrupo == nombreGrupo && x.Admingrupo == adminGrupo).
                    Include(x => x.Carrera).
                    ThenInclude(x => x.CarreraCuentabancaria).
                    Where(x => x.Carrera.Privacidad == true).ToList();

            var carrerasInscritas = verCarrerasInscritas(usuario);

            foreach (var carrera in verCarreras)
            {
                if (!carrerasInscritas.Contains(carrera.Carrera))
                    carreras.Add(carrera.Carrera);
            }

            return carreras;
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

        public List<PosicionesCarrera> carrerasConPosiciones(string usuario)
        {
            List<PosicionesCarrera> carrerasConPosiciones = new List<PosicionesCarrera>();

            var carrerasInscritas = verCarrerasInscritas(usuario);

            var actividades = _context.Actividad.ToList();

            foreach(var carrera in carrerasInscritas)
            {
                var posicionCarrera = new PosicionesCarrera();

                posicionCarrera.nombreCarrera = carrera.Nombre;
                posicionCarrera.adminCarrera = carrera.Admindeportista;
                posicionCarrera.recorridoGPX = carrera.Recorrido;
                posicionCarrera.fecha = carrera.Fecha;
                posicionCarrera.CarreraCuentabancaria = carrera.CarreraCuentabancaria.ToList();
                posicionCarrera.CarreraPatrocinador = carrera.CarreraPatrocinador.ToList();

                foreach (var actividad in actividades)
                {
                    if(actividad.Nombreretocarrera == carrera.Nombre && actividad.Adminretocarrera == carrera.Admindeportista
                        && (int)(DateTime.Today - carrera.Fecha ).TotalDays > 0)
                    {
                        var posActividad = new PosActividad()
                        {
                            usuarioDeportista = actividad.Usuariodeportista,
                            duracion = actividad.Duracion,
                            tipoActividad = actividad.Tipoactividad
                        };
                        posicionCarrera.actividades.Add(posActividad);
                        posicionCarrera.completada = true;
                    }
                }
                posicionCarrera.actividades = posicionCarrera.actividades.OrderBy(x => x.duracion).ToList();
                carrerasConPosiciones.Add(posicionCarrera);
            }

            return carrerasConPosiciones;
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
