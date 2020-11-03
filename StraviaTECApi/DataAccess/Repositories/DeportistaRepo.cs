using EFConsole.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace EFConsole.DataAccess.Repositories
{
    class DeportistaRepo
    {

        private readonly StravaContext _context;

        // se inyecta el DB Context 
        public DeportistaRepo(StravaContext context)
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
            return _context.Deportista.FirstOrDefault(x => x.Usuario == usuario);
        }

        public void verActividadesAmigos(string usuario)
        {
            var deportista = _context.Deportista.FirstOrDefault(x => x.Usuario == usuario);
            
            // Hay que buscar los amigos y por cada uno retornar las actividades
            var amigoDeportista = _context.AmigoDeportista.
                    Where(x => x.Usuariodeportista == usuario).
                    Include(x => x.Amigo).ToList();

            foreach (var amigo in amigoDeportista)
            {
                Console.Write(amigo.Amigo.Nombre + " ");
                Console.Write(amigo.Amigo.Apellido1 + " ");
                Console.Write(amigo.Amigo.Apellido2 + " ");
                Console.WriteLine();
            }

        }

        public void seguirDeportista(string usuarioDeportista, string usuarioAmigo)
        {
            var amigoDeportista = new AmigoDeportista();
            amigoDeportista.Usuariodeportista = usuarioDeportista;
            amigoDeportista.Amigoid = usuarioAmigo;

            _context.Add(amigoDeportista);
        }

        public void verCarrerasInscritas(string UsuarioDeportista)
        {
            var carrerasInscritas = _context.DeportistaCarrera.
                    Where(x => x.Usuariodeportista == UsuarioDeportista).
                    Include(x => x.Carrera).ToList();

            foreach (var carrera in carrerasInscritas)
            {
                Console.WriteLine(carrera.Carrera.Nombre);
            }
            // se debe retornar el resultado
        }

        public void agregarCarreraDeportista(string UsuarioDeportista, string nombreCarrera)
        {
            var deportista = _context.Deportista.FirstOrDefault(x => x.Usuario == UsuarioDeportista);
            var carrera = _context.Carrera.FirstOrDefault(x => x.Nombre == nombreCarrera);

            var deportistaCarrera = new DeportistaCarrera();
            deportistaCarrera.Admindeportista = carrera.Admindeportista;
            deportistaCarrera.Nombrecarrera = carrera.Nombre;
            deportistaCarrera.Usuariodeportista = deportista.Usuario;
            deportistaCarrera.Completada = false;

            _context.Add(deportistaCarrera);
        }

        public void agregarGrupo(string usuarioDeportista, string nombreGrupo)
        {
            var deportista = _context.Deportista.FirstOrDefault(x => x.Usuario == usuarioDeportista);
            var grupo = _context.Grupo.FirstOrDefault(x => x.Nombre == nombreGrupo);

            var grupoAgregado = new GrupoDeportista();
            grupoAgregado.Admindeportista = grupo.Admindeportista;
            grupoAgregado.Nombregrupo = grupo.Nombre;
            grupoAgregado.Usuariodeportista = deportista.Usuario;
            _context.Add(grupoAgregado);
        }

        public void mostrarTodosDeportistasNoAmigos(string usuarioDeportista)
        {
            // se deben mostrar todos los deportistas
            // que no sean amigos de ese usuario
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
