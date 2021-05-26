using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaz.Repositorio
{
    public interface IPropertyRepositorio
    {
        Task<List<PropertyEntidad>> BuscarPropertiesPorOwnerId(int idOwner);

        Task<PropertyEntidad> CrearProperty(int idOwner,PropertyEntidad propertyCrear);

        Task<bool> ExisteProperty(int idOwner,int idProperty);

        Task<PropertyImageEntidad> CrearImageProperty(int idProperty, PropertyImageEntidad imageCrear);
    }
}
