using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StraviaTECApi.Models;
using StraviaTECApi.Parsers;
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

        public List<Carrera> verCarreraAdministradaPorNombre(string adminCarrera, string nombreCarrera)
        {
            return _context.Carrera.Where(x => x.Admindeportista == adminCarrera && x.Nombre.Equals(nombreCarrera)).
                Include(x => x.CarreraCuentabancaria).
                Include(x => x.CarreraPatrocinador).
                Include(x => x.GrupoCarrera).
                Include(x => x.CarreraCategoria).ToList();
        }

        public List<CarreraParser> buscarCarreraPorNombre(string usuario, string nombreCarrera)
        {
            List<Carrera> resultadoNoInscritas = new List<Carrera>();

            List<Carrera> resultadoParcial = new List<Carrera>();

            var grupos = _context.GrupoDeportista.Where(x => x.Usuariodeportista == usuario).Include(x => x.Grupo).ToList();

            var resultadosBusqueda = _context.Carrera.Where(x => x.Nombre.Contains(nombreCarrera)).
                                    Include(x => x.CarreraCuentabancaria).
                                    Include(x => x.CarreraPatrocinador).
                                    Include(x => x.CarreraCategoria).ToList();

            var carrerasPrivadas = _context.GrupoCarrera.
                    Include(x => x.Carrera).
                    ThenInclude(x => x.CarreraCuentabancaria).
                    Include(x => x.Carrera.CarreraCategoria).
                    Include(x => x.Carrera.CarreraPatrocinador).ToList();

            var carrerasInscritas = verCarrerasInscritas(usuario);


            foreach (var carrera in carrerasPrivadas)
            {
                foreach (var grupo in grupos)
                {
                    if (grupo.Idgrupo == carrera.Idgrupo && grupo.Admindeportista.Equals(carrera.Admingrupo)
                        && !carrerasInscritas.Contains(carrera.Carrera)
                        && (int)(carrera.Carrera.Fecha - DateTime.Today).TotalDays >= 0)
                    {
                        resultadoParcial.Add(carrera.Carrera);
                        break;
                    }
                }
            }

            foreach(var carrera in resultadosBusqueda)
            {
                if (resultadoParcial.Contains(carrera) && (int)(carrera.Fecha - DateTime.Today).TotalDays >= 0)
                    resultadoNoInscritas.Add(carrera);
            }

            return generarJSONCarrera(resultadoNoInscritas, null);
        }

        public List<Carrera> verCarrerasInscritas(string usuarioDeportista)
        {
            List<Carrera> carreras = new List<Carrera>();

            var carrerasInscritas = _context.DeportistaCarrera.
                    Where(x => x.Usuariodeportista == usuarioDeportista).
                    Include(x => x.Carrera.CarreraCuentabancaria).
                    Include(x => x.Carrera.CarreraCategoria).
                    Include(x => x.Carrera.CarreraPatrocinador).ToList();

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

        public List<CarreraParser> verCarrerasDisponibles(string usuario)
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
                    Include(x => x.Carrera.CarreraCategoria).
                    Include(x => x.Carrera.CarreraPatrocinador).ToList();

            foreach(var carrera in carrerasPrivadas)
            {
                foreach(var grupo in grupos)
                {
                    if(grupo.Idgrupo == carrera.Idgrupo && grupo.Admindeportista.Equals(carrera.Admingrupo)
                        && !carrerasInscritas.Contains(carrera.Carrera) 
                        && (int)(carrera.Carrera.Fecha - DateTime.Today).TotalDays >= 0)
                    {
                       carreras.Add(carrera.Carrera);
                        break;
                    }
                }
            }

            foreach(var carrera in carrerasPublicas)
            {
                if (!carrerasInscritas.Contains(carrera) && (int)(carrera.Fecha - DateTime.Today).TotalDays >= 0)
                    carreras.Add(carrera);
            }  

            return generarJSONCarrera(carreras, null);
        }

        public List<Carrera> accederCarreras(int idGrupo, string adminGrupo, string usuario)
        {
            List<Carrera> carreras = new List<Carrera>();

            var verCarreras = _context.GrupoCarrera.
                    Where(x => x.Idgrupo == idGrupo && x.Admingrupo == adminGrupo).
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

        public List<CarreraParser> carrerasConPosiciones(string usuario)
        {
            var carrerasInscritas = verCarrerasInscritas(usuario);

            var actividades = _context.Actividad.ToList();

            return generarJSONCarrera(carrerasInscritas, actividades);
        }

        public List<CarreraParser> generarJSONCarrera(List<Carrera> listaCarreras, List<Actividad> actividades)
        {
            var patrocinadores = _context.Patrocinador.ToList();

            List<CarreraParser> carrerasConPosiciones = new List<CarreraParser>();

            foreach (var carrera in listaCarreras)
            {
                var carreraParser = new CarreraParser
                {
                    nombreCarrera = carrera.Nombre,
                    adminDeportista = carrera.Admindeportista,
                    recorridoGPX = carrera.Recorrido,
                    fecha = carrera.Fecha,
                    tipoActividad = carrera.Tipoactividad,
                    costo = carrera.Costo,
                    privacidad = carrera.Privacidad,
                    diasFaltantes = (int)(carrera.Fecha - DateTime.Today).TotalDays
                };

                // se agregan las cuentas bancarias
                foreach(var cuenta in carrera.CarreraCuentabancaria)
                {
                    carreraParser.carreraCuentabancaria.Add(cuenta.Cuentabancaria);
                }

                // se agregan los patrocinadores
                foreach (var patrocinador in carrera.CarreraPatrocinador)
                {
                    var carreraPatrocinador = patrocinadores.
                        Where(x => x.Nombrecomercial == patrocinador.Nombrepatrocinador).
                        FirstOrDefault();

                    carreraParser.carreraPatrocinador.Add(new PatrocinadorParser
                    {
                        Nombrecomercial = carreraPatrocinador.Nombrecomercial,
                        Nombrerepresentante = carreraPatrocinador.Nombrerepresentante,
                        Logo = carreraPatrocinador.Logo,
                        Numerotelrepresentante = carreraPatrocinador.Numerotelrepresentante
                    });
                }

                // se agregan las categorías
                foreach (var categoria in carrera.CarreraCategoria)
                {
                    carreraParser.carreraCategorias.Add(categoria.Nombrecategoria);
                }

                if (actividades != null)
                {
                    // se agregan las actividades
                    foreach (var actividad in actividades)
                    {
                        if (actividad.Nombreretocarrera == carrera.Nombre && actividad.Adminretocarrera == carrera.Admindeportista
                            && (int)(DateTime.Today - carrera.Fecha).TotalDays > 0)
                        {
                            var posActividad = new PosActividad()
                            {
                                usuarioDeportista = actividad.Usuariodeportista,
                                duracion = actividad.Duracion,
                                tipoActividad = actividad.Tipoactividad
                            };
                            carreraParser.actividades.Add(posActividad);
                            carreraParser.finalizada = true;
                        }
                    }
                    carreraParser.actividades = carreraParser.actividades.OrderBy(x => x.duracion).ToList();
                }

                carrerasConPosiciones.Add(carreraParser);
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
