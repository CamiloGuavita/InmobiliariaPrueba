using Aplicacion.Comun.Request;
using Aplicacion.Comun.Respuestas;
using Aplicacion.Interfaz.Repositorio;
using Aplicacion.Interfaz.Servicios;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Comun.Exceptions;
using Microsoft.AspNetCore.Http;
using Dominio.Entidades;

namespace Aplicacion.Servicios
{
    public class OwnerServicio : IOwnerServicio
    {
        private IOwnerRepositorio _repositorio;
        private IMapper _mapper;

        public OwnerServicio(IOwnerRepositorio repositorio, IMapper mapper)
        {
            this._repositorio = repositorio;
            this._mapper = mapper;
        }

        public async Task<RespuestaGeneral<OwnerRespuesta>> BuscarOwnerPorId(int id)
        {
            RespuestaGeneral<OwnerRespuesta> resultado = new RespuestaGeneral<OwnerRespuesta>();
            if(id < 0)
            {
                resultado.StatusCodeOperation = StatusCodes.Status400BadRequest;
                resultado.Exito = false;
                resultado.Mensaje = "El Id a buscar no es valido";
                resultado.Errores = new List<string>
                {
                    "El Id no puede ser null",
                    "El Id debe ser mayor a 0",
                };
                resultado.Datos = null;
                return resultado;
            }
            try
            {
                var resultadoBusqueda = await _repositorio.BuscarOwnerPorId(id);
                if(resultadoBusqueda == null)
                {
                    resultado.StatusCodeOperation = StatusCodes.Status404NotFound;
                    resultado.Exito = false;
                    resultado.Mensaje = "Owner no encontrado";
                    resultado.Errores = new List<string>
                    {
                        $"El owner con el Id {id} no fue encontrado"
                    };
                    resultado.Datos = null;
                }
                else
                {
                    resultado.StatusCodeOperation = StatusCodes.Status200OK;
                    resultado.Exito = true;
                    resultado.Mensaje = "Owner encontrado";
                    resultado.Errores = null;
                    resultado.Datos = _mapper.Map<OwnerRespuesta>(resultadoBusqueda);
                }
            }
            catch(EntidadNoExisteException ex)
            {
                resultado.StatusCodeOperation = StatusCodes.Status400BadRequest;
                resultado.Exito = false;
                resultado.Mensaje = "Owner no encontrado";
                resultado.Errores = new List<string>
                {
                    ex.Message
                };
            }
            return resultado;    
        }

        public async Task<RespuestaGeneral<List<OwnerRespuesta>>> BuscarOwnerPorNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        public async Task<RespuestaGeneral<List<OwnerRespuesta>>> ObtenerListaOwners()
        {
            var lista = await _repositorio.ObtenerListaOwners();
            RespuestaGeneral<List<OwnerRespuesta>> repuesta = new RespuestaGeneral<List<OwnerRespuesta>>()
            {
                Exito = true,
                Mensaje = "Consulta correcta",
                Errores = null,
                Datos = _mapper.Map<List<OwnerRespuesta>>(lista)
            };
            return repuesta;
        }

        public async Task<RespuestaGeneral<OwnerRespuesta>> CrearOwner(OwnerRequest ownerCrear)
        {
            RespuestaGeneral<OwnerRespuesta> resultado = new RespuestaGeneral<OwnerRespuesta>();
            OwnerValidate validar = new OwnerValidate();
            var resultadoValidacion = validar.Validate(ownerCrear);
            if (resultadoValidacion.IsValid)
            {
                OwnerEntidad entidadCrear = _mapper.Map<OwnerEntidad>(ownerCrear);
                var ownerCreado = await _repositorio.CrearOwner(entidadCrear);
                if(ownerCreado == null)
                {
                    resultado = new RespuestaGeneral<OwnerRespuesta>
                    {
                        Exito = false,
                        Datos = null,
                        Mensaje = "Ha ocurrido uno o varios errores",
                        Errores = new List<string>()
                        {
                            "No ha sido posible crear el Owner"
                        },
                        StatusCodeOperation = StatusCodes.Status400BadRequest
                    };
                }
                else
                {
                    resultado = new RespuestaGeneral<OwnerRespuesta>
                    {
                        Exito = true,
                        Datos = _mapper.Map<OwnerRespuesta>(ownerCreado),
                        Mensaje = "Owner creado con exito",
                        Errores = null,
                        StatusCodeOperation = StatusCodes.Status201Created
                    };
                }
            }
            else
            {
                resultado = new RespuestaGeneral<OwnerRespuesta>
                {
                    Exito = false,
                    Datos = null,
                    Errores = resultadoValidacion.Errors?.Select(s => s.ErrorMessage).ToList(),
                    Mensaje = "Ha ocurrido uno o varios errores",
                    StatusCodeOperation = StatusCodes.Status400BadRequest
                };
            }
            return resultado;
        }
    }
}
