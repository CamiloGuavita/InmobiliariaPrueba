using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Interfaz.Repositorio;
using Dominio.Entidades;

namespace TestApi.Infraestructura.Repositorio
{
    public class OwnerRepositorioMock : IOwnerRepositorio
    {
        private List<OwnerEntidad> ownerList;

        public OwnerRepositorioMock()
        {
            ownerList = new List<OwnerEntidad>()
            {
                new OwnerEntidad
                {
                    Id = 1,
                    Name = "Owner prueba",
                    Address = "Direccion prubea",
                    Birthday = new DateTime(1992,10,2),
                    Photo = ""
                }
            };
        }

        public async Task<OwnerEntidad> BuscarOwnerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OwnerEntidad>> BuscarOwnerPorNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        public async Task<OwnerEntidad> CrearOwner(OwnerEntidad ownerCrear)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OwnerEntidad>> ObtenerListaOwners()
        {
            return await Task.FromResult(ownerList);
        }

        public async Task<bool> OwnerExiste(int id)
        {
            var owner = ownerList.Where(w => w.Id == id).FirstOrDefault();
            return await Task.FromResult(owner != null ? true : false);
        }
    }
}
