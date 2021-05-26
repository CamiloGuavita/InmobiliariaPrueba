using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Interfaz.Servicios;
using Microsoft.AspNetCore.Http;
using Aplicacion.Comun.Respuestas;
using Aplicacion.Comun.Request;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private IOwnerServicio _servicioOwner;
        public OwnerController(IOwnerServicio servicioOwner)
        {
            this._servicioOwner = servicioOwner;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(RespuestaGeneral<List<OwnerRespuesta>>))]
        public async Task<IActionResult> GetAll()
        {
            var result =  await this._servicioOwner.ObtenerListaOwners();
            return Ok(result);
        }

        // GET api/<OwnerController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RespuestaGeneral<OwnerRespuesta>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RespuestaGeneral<OwnerRespuesta>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(RespuestaGeneral<OwnerRespuesta>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
        {
            int idConsulta = -1;
            if (int.TryParse(id,out idConsulta) == false)
            {
                return BadRequest(new RespuestaGeneral<OwnerRespuesta>
                {
                    Exito = false,
                    Mensaje = $"El id [{id}] para la busqueda no es valido",
                    Errores = null,
                    Datos = null
                });
            }
            var result = await this._servicioOwner.BuscarOwnerPorId(idConsulta);

            switch (result.StatusCodeOperation)
            {
                case StatusCodes.Status200OK:
                    return Ok(result);
                    break;
                case StatusCodes.Status400BadRequest:
                    return BadRequest(result);
                    break;
                case StatusCodes.Status404NotFound:
                    return NotFound(result);
                    break;
                default:
                    return BadRequest();
            }
        }

        // POST api/<OwnerController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RespuestaGeneral<OwnerRespuesta>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RespuestaGeneral<OwnerRespuesta>))]
        public async Task<IActionResult> CreateOwner([FromBody] OwnerRequest owner)
        {
            if (owner == null)
            {
                return BadRequest(new RespuestaGeneral<OwnerRespuesta>
                {
                    Exito = false,
                    Mensaje = $"No hay informacion para crear el Owner",
                    Errores = null,
                    Datos = null
                });
            }
            var resultado = await _servicioOwner.CrearOwner(owner);
            switch (resultado.StatusCodeOperation)
            {
                case StatusCodes.Status201Created:
                    return CreatedAtAction("Get",resultado);
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
