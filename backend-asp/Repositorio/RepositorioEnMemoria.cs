using backend_asp.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_asp.Repositorio
{

    public class RepositorioEnMemoria : IRepositorio
    {

        private List<Genero> _generos;

        public Guid _guid;

        public RepositorioEnMemoria() {
            _generos = new List<Genero>() {

                new Genero(){ Id = 1, Nombre = "Comedia"},
                new Genero(){ Id = 2, Nombre = "Acción"}

            };

            this._guid = Guid.NewGuid();
        }


        public List<Genero> ObtenerTodosLosGeneros() {

            return this._generos;
        }

        public async Task<Genero> ObtenerPorId(int Id) 
        {
            await Task.Delay(1);
            //return this._generos.Where(g => g.Id == Id).FirstOrDefault();      
            return this._generos.FirstOrDefault(g => g.Id == Id);      
        }

        public Guid ObtenerGuid() 
        {
            return this._guid;
        }

        public void CrearGenero(Genero genero) {

            genero.Id = this._generos.Count() + 1;
            this._generos.Add(genero);
        }

    }
}
