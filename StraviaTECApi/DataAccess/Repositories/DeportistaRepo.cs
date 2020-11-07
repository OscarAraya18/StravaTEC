using Microsoft.EntityFrameworkCore;
using StraviaTECApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFConsole.DataAccess.Repositories
{
    public class DeportistaRepo
    {

        private readonly StraviaContext _context;

        // se inyecta el DB Context 
        public DeportistaRepo(StraviaContext context)
        {
            _context = context;
        }

        /**
         * ------------------------------
         *         MÉTODOS CRUD
         * ------------------------------
         */
            
        public void Create(Deportista deportista)
        {
            if (deportista == null)
                throw new System.ArgumentNullException(nameof(deportista));
            
            _context.Deportista.Add(deportista);

        }

        public void Update(Deportista deportista)
        {
            if (deportista == null)
                throw new System.ArgumentNullException(nameof(deportista));

            _context.Deportista.Update(deportista);
            _context.Entry(deportista).State = EntityState.Modified;

        }

        public void Delete(string usuario)
        {
            var deportista = _context.Deportista.FirstOrDefault(x => x.Usuario == usuario);

            if (deportista == null)
            {
                throw new System.ArgumentNullException(nameof(deportista));
            }

            _context.Deportista.Remove(deportista);

        }

        public Deportista obtenerPorUsuario(string usuario)
        {
            //l
            return _context.Deportista.FirstOrDefault(x => x.Usuario == usuario);
        }

        public bool verificarLogin(Login login)
        {
            var deportista = _context.Deportista.Where(x => x.Usuario == login.Usuario
                              && x.Claveacceso == login.ClaveAcceso).ToList();

            if (deportista.Count == 1)
                return true;

            return false;
        }

        public List<Actividad> verActividadesAmigos(string usuario)
        {
            List<Actividad> actividades = new List<Actividad>();
            
            // Hay que buscar los amigos y por cada uno retornar las actividades
            var amigoDeportista = _context.AmigoDeportista.
                    Where(x => x.Usuariodeportista == usuario).
                    Include(x => x.Amigo).
                    ThenInclude(x => x.Actividad).ToList();

            foreach (var amigo in amigoDeportista)
            {
                Console.Write(amigo.Amigo.Nombre + " ");
                Console.Write(amigo.Amigo.Apellido1 + " ");
                Console.Write(amigo.Amigo.Apellido2 + " ");
                Console.WriteLine();
                foreach(var actividad in amigo.Amigo.Actividad)
                {
                    actividades.Add(actividad);
                }
                
            }
            return actividades;
        }

        public void seguirDeportista(string usuarioDeportista, string usuarioAmigo)
        {
            var amigoDeportista = new AmigoDeportista();
            amigoDeportista.Usuariodeportista = usuarioDeportista;
            amigoDeportista.Amigoid = usuarioAmigo;

            _context.Add(amigoDeportista);
        }

        public List<Carrera> verCarrerasInscritas(string UsuarioDeportista)
        {
            List<Carrera> carreras = new List<Carrera>();

            var carrerasInscritas = _context.DeportistaCarrera.
                    Where(x => x.Usuariodeportista == UsuarioDeportista).
                    Include(x => x.Carrera).ToList();

            foreach (var carrera in carrerasInscritas)
            {
                Console.WriteLine(carrera.Carrera.Nombre);
                carreras.Add(carrera.Carrera);
            }
            // se debe retornar el resultado
            return carreras;
        }

        // creo que no es necesaria
        public void agregarCarreraDeportista(string UsuarioDeportista, Carrera carrera)
        {
            var deportista = _context.Deportista.FirstOrDefault(x => x.Usuario == UsuarioDeportista);

            var deportistaCarrera = new DeportistaCarrera();
            deportistaCarrera.Admindeportista = carrera.Admindeportista;
            deportistaCarrera.Nombrecarrera = carrera.Nombre;
            deportistaCarrera.Usuariodeportista = deportista.Usuario;
            deportistaCarrera.Completada = false;

            _context.Add(deportistaCarrera);
        }

        public List<Deportista> verAmigosAsociados(string usuarioDeportista)
        {
            List<Deportista> amigos = new List<Deportista>();

            var amigoDeportista = _context.AmigoDeportista.
                   Where(x => x.Usuariodeportista == usuarioDeportista).
                   Include(x => x.Amigo).ToList();

            foreach(var amigo in amigoDeportista)
            {
                amigos.Add(amigo.Amigo);
            }

            return amigos;
        }

        public List<Deportista> mostrarTodosDeportistasNoAmigos(string usuarioDeportista)
        {
            // se deben acceder todos los deportistas amigos
            var amigos = verAmigosAsociados(usuarioDeportista);

            // se acceden a todos los deportistas
            var deportistasTotales = _context.Deportista.Where(x => x.Usuario != usuarioDeportista).ToList();

            List<Deportista> deportistasNoAmigos = new List<Deportista>();

            foreach (var deportista in deportistasTotales)
            {
                if (!amigos.Contains(deportista))
                    deportistasNoAmigos.Add(deportista);
            }

            return deportistasNoAmigos;
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
