using Microsoft.EntityFrameworkCore;
using StraviaTECApi.Models;
using StraviaTECApi.Parsers;
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
                throw new ArgumentNullException(nameof(grupo));

            _context.Grupo.Add(grupo);

        }

        public void Update(Grupo grupo)
        {
            if (grupo == null)
                throw new ArgumentNullException(nameof(grupo));

            _context.Grupo.Update(grupo);
            _context.Entry(grupo).State = EntityState.Modified;
        }

        public void Delete(int idGrupo, string admin)
        {
            var grupo = _context.Grupo.FirstOrDefault(x => x.Id == idGrupo && x.Admindeportista == admin);

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
            grupoAgregado.Idgrupo = grupo.Id;
            grupoAgregado.Usuariodeportista = deportista.Usuario;
            _context.Add(grupoAgregado);
        }

        public List<Grupo> verTodosLosGrupos()
        {
            return _context.Grupo.ToList();
        }
        public List<Grupo> verTodosLosGruposNoInscritos(string usuarioDeportista)
        {
            List<Grupo> gruposNoInscritos = new List<Grupo>();
            
            List<Grupo> gruposInscritos = new List<Grupo>();

            var grupoDeportista = _context.GrupoDeportista.Where(x => x.Usuariodeportista == usuarioDeportista).
                Include(x => x.Grupo).ToList();

            var gruposTotales = _context.Grupo.ToList();

            foreach(var grupo in grupoDeportista)
            {
                gruposInscritos.Add(grupo.Grupo);
            }

            foreach(var grupo in gruposTotales)
            {
                if (!gruposInscritos.Contains(grupo))
                    gruposNoInscritos.Add(grupo);
            }

            return gruposNoInscritos;
        }

        public List<Reto> accederRetos(int idGrupo, string usuario)
        {
            List<Reto> retos = new List<Reto>();

            var verRetos = _context.GrupoReto.
                    Where(x => x.Idgrupo == idGrupo).
                    Include(x => x.Reto).
                    Where(x => x.Reto.Privacidad == true).ToList();

            var retosInscritos = verRetosInscritos(usuario);

            var retosPublicos = _context.Reto.Where(x => x.Privacidad == false).ToList();

            foreach (var reto in verRetos)
            {
                if (!retosInscritos.Contains(reto.Reto))
                    retos.Add(reto.Reto);
            }

            foreach (var reto in retosPublicos)
            {
                if (!retosInscritos.Contains(reto))
                    retos.Add(reto);
            }

            return retos;
            // se debe retornar el resultado
        }

        public List<Carrera> accederCarreras(int idGrupo, string usuario)
        {
            List<Carrera> carreras = new List<Carrera>();

            var verCarreras = _context.GrupoCarrera.
                    Where(x => x.Idgrupo == idGrupo).
                    Include(x => x.Carrera).
                    ThenInclude(x => x.CarreraCuentabancaria).
                    Where(x => x.Carrera.Privacidad == true).ToList();

            var carrerasInscritas = verCarrerasInscritas(usuario);

            var carrerasPublicas = _context.Carrera.Where(x => x.Privacidad == false).
                                    Include(x => x.CarreraCuentabancaria).ToList();

            foreach (var carrera in verCarreras)
            {
                if(!carrerasInscritas.Contains(carrera.Carrera))
                    carreras.Add(carrera.Carrera);
            }

            foreach(var carrera in carrerasPublicas)
            {
                if (!carrerasInscritas.Contains(carrera))
                    carreras.Add(carrera);
            }

            return carreras;
            // se debe retornar el resultado
        }

        public List<Carrera> verCarrerasInscritas(string usuarioDeportista)
        {
            List<Carrera> carreras = new List<Carrera>();

            var carrerasInscritas = _context.DeportistaCarrera.
                    Where(x => x.Usuariodeportista == usuarioDeportista).
                    Include(x => x.Carrera).ToList();

            foreach (var carrera in carrerasInscritas)
            {

                carreras.Add(carrera.Carrera);


            }
            // se debe retornar el resultado
            return carreras;
        }

        public List<Reto> verRetosInscritos(string usuarioDeportista)
        {
            List<Reto> retos = new List<Reto>();

            var retosInscritos = _context.DeportistaReto.
                    Where(x => x.Usuariodeportista == usuarioDeportista).
                    Include(x => x.Reto).ToList();

            foreach (var reto in retosInscritos)
            {

                retos.Add(reto.Reto);


            }
            // se debe retornar el resultado
            return retos;
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

        public List<Grupo> buscarPorNombre(string nombreGrupo, string usuario)
        {
            List<Grupo> gruposEncontradosNoAsociados = new List<Grupo>();

            var gruposAsociados = verMisGruposAsociados(usuario);

            var gruposEncontrados = _context.Grupo.Where(x => x.Nombre.Contains(nombreGrupo)).ToList();

            foreach (var grupo in gruposEncontrados)
            {
                if (!gruposAsociados.Contains(grupo))
                    gruposEncontradosNoAsociados.Add(grupo);
            }

            return gruposEncontradosNoAsociados;
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
