using Aplicacion.Comun.Request;
using Aplicacion.Comun.Respuestas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaz.Servicios
{
    public interface IOwnerServicio
    {
        Task<RespuestaGeneral<List<OwnerRespuesta>>> ObtenerListaOwners();

        Task<RespuestaGeneral<List<OwnerRespuesta>>> BuscarOwnerPorNombre(string nombre);

        Task<RespuestaGeneral<OwnerRespuesta>> BuscarOwnerPorId(int id);

        Task<RespuestaGeneral<OwnerRespuesta>> CrearOwner(OwnerRequest ownerCrear);
    }
}
