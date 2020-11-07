using Microsoft.EntityFrameworkCore;
using StraviaTECApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    Include(x => x.Carrera).
                    ThenInclude(x => x.CarreraCuentabancaria).
                    Where(x => x.Carrera.Privacidad == true).ToList();

            var carrerasPublicas = _context.Carrera.Where(x => x.Privacidad == false).
                                    Include(x => x.CarreraCuentabancaria).ToList();

            foreach (var carrera in verCarreras)
            {
                carreras.Add(carrera.Carrera);
            }

            foreach(var carrera in carrerasPublicas)
            {
                carreras.Add(carrera);
            }
            return carreras;
            // se debe retornar el resultado
        }

        public List<Grupo> verMisGruposAdministrados(string usuarioDeportista)
        {
            return _context.Grupo.Where(x => x.Admindeportista == usuarioDeportista).ToList();
        }
        public List<Grupo> verMisGruposAsociados(string usuarioDeportista)
        {
            List<Grupo> grupos = new List<Grupo>();

            var gruposDeportista = _context.GrupoDeportista.
                Where(x => x.Usuariodeportista == usuarioDeportista).
                Include(x => x.Grupo).ToList();

            foreach(var grupo in gruposDeportista)
            {
                grupos.Add(grupo.Grupo);
            }

            return grupos;
        }


        public List<Grupo> verTodosNoAsociados(string nombreUsuario)
        {
            var gruposAsociados = verMisGruposAsociados(nombreUsuario);

            var gruposTotales = _context.Grupo.ToList();

            List<Grupo> gruposNoAsociados = new List<Grupo>();

            foreach(var grupo in gruposTotales)
            {
                if (!gruposAsociados.Contains(grupo))
                    gruposNoAsociados.Add(grupo);
            }

            return gruposNoAsociados;
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
