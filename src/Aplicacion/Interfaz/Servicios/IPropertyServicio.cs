using Aplicacion.Comun.Request;
using Aplicacion.Comun.Respuestas;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaz.Servicios
{
    public interface IPropertyServicio
    {
        Task<RespuestaGeneral<List<PropertyRespuesta>>> BuscarPropertiesPorOwnerId(int idOwner);

        Task<RespuestaGeneral<PropertyRespuesta>> CrearProperty(PropertyRequest propertyCrear);

        Task<RespuestaGeneral<PropertyImageRespuesta>> CrearImageProperty(PropertyImagesRequest imageCrear);
    }
}
