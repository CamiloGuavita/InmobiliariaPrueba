using Aplicacion.Comun.Request;
using Aplicacion.Comun.Respuestas;
using Aplicacion.Interfaz.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private IPropertyServicio _servicioProperty;
        public PropertyController(IPropertyServicio servicioProperty)
        {
            this._servicioProperty = servicioProperty;
        }

        [HttpGet]
        [ActionName("GetAllByOwnwer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RespuestaGeneral<List<PropertyRespuesta>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RespuestaGeneral<List<PropertyRespuesta>>))]
        public async Task<IActionResult> GetAllByOwnwer(string idOwner)
        {
            int idConsulta = -1;
            if (int.TryParse(idOwner, out idConsulta) == false)
            {
                return BadRequest(new RespuestaGeneral<List<PropertyRespuesta>>
                {
                    Exito = false,
                    Mensaje = $"El id Ownner [{idOwner}] para la busqueda no es valido",
                    Errores = null,
                    Datos = null
                });
            }
            var result = await this._servicioProperty.BuscarPropertiesPorOwnerId(idConsulta);
            switch (result.StatusCodeOperation)
            {
                case StatusCodes.Status200OK:
                    return Ok(result);
                    break;
                case StatusCodes.Status400BadRequest:
                    return BadRequest(result);
                    break;
                default:
                    return BadRequest();
            }
        }

        // POST api/<PropertyController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RespuestaGeneral<PropertyRespuesta>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RespuestaGeneral<PropertyRespuesta>))]
        public async Task<IActionResult> CreateProperty([FromBody] PropertyRequest propiedad)
        {
            if (propiedad == null)
            {
                return BadRequest(new RespuestaGeneral<PropertyRespuesta>
                {
                    Exito = false,
                    Mensaje = $"No hay informacion para crear la propiedad",
                    Errores = null,
                    Datos = null
                });
            }
            var resultado = await _servicioProperty.CrearProperty(propiedad);
            switch (resultado.StatusCodeOperation)
            {
                case StatusCodes.Status201Created:
                    return CreatedAtAction("GetByOwnwer", new { idOwner = propiedad.IdOwner },resultado);
                    break;
                case StatusCodes.Status400BadRequest:
                    return BadRequest(resultado);
                    break;
                default:
                    return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RespuestaGeneral<PropertyImageRespuesta>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RespuestaGeneral<PropertyImageRespuesta>))]
        public async Task<IActionResult> CreateImageProperty([FromBody] PropertyImagesRequest imagen)
        {
            if (imagen == null)
            {
                return BadRequest(new RespuestaGeneral<PropertyImageRespuesta>
                {
                    Exito = false,
                    Mensaje = $"No hay informacion para crear la Image",
                    Errores = null,
                    Datos = null
                });
            }
            var resultado = await _servicioProperty.CrearImageProperty(imagen);
            switch (resultado.StatusCodeOperation)
            {
                case StatusCodes.Status201Created:
                    return CreatedAtAction("GetByOwnwer", new { idOwner = imagen.IdOwner }, resultado);
                    break;
                case StatusCodes.Status400BadRequest:
                    return BadRequest(resultado);
                    break;
                default:
                    return BadRequest();
            }
        }

    }
}
