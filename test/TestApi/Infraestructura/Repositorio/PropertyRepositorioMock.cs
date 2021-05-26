using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Interfaz.Repositorio;
using Dominio.Entidades;

namespace TestApi.Infraestructura.Repositorio
{
    public class PropertyRepositorioMock : IPropertyRepositorio
    {
        private List<PropertyEntidad> propertiesList;
        public PropertyRepositorioMock()
        {
            propertiesList = new List<PropertyEntidad>
            {
                new PropertyEntidad
                {
                    Id = 1,
                    Name = "Property de prueba",
                    Address = "calle 100",
                    CodeInternal = "12312312asd",
                    Price  = 60000000,
                    Year = 2015,
                    Images = new List<PropertyImageEntidad>
                    {
                        new PropertyImageEntidad
                        {
                            Id = 1 ,
                            File = "url foto",
                            Enabled = true
                        }
                    }
                }
            };
        }

        public async Task<List<PropertyEntidad>> BuscarPropertiesPorOwnerId(int idOwner)
        {
            return await Task.FromResult(propertiesList);
        }

        public async Task<PropertyImageEntidad> CrearImageProperty(int idProperty, PropertyImageEntidad imageCrear)
        {
            throw new NotImplementedException();
        }

        public async Task<PropertyEntidad> CrearProperty(int idOwner, PropertyEntidad propertyCrear)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExisteProperty(int idOwner, int idProperty)
        {
            throw new NotImplementedException();
        }
    }
}
