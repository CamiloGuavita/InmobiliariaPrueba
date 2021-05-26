using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaz.Repositorio
{
    public interface IOwnerRepositorio
    {
        Task<List<OwnerEntidad>> ObtenerListaOwners();

        Task<List<OwnerEntidad>> BuscarOwnerPorNombre(string nombre);

        Task<OwnerEntidad> BuscarOwnerPorId(int id);

        Task<OwnerEntidad> CrearOwner(OwnerEntidad ownerCrear);

        Task<bool> OwnerExiste(int id);
    }
}
