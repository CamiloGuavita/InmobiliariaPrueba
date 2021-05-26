using Aplicacion.Comun.Request;
using Aplicacion.Comun.Respuestas;
using Aplicacion.Interfaz.Repositorio;
using Aplicacion.Interfaz.Servicios;
using AutoMapper;
using Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class PropertyServicio : IPropertyServicio
    {
        private IPropertyRepositorio _repositorioProperties;
        private IOwnerRepositorio _repositorioOwner;
        private IMapper _mapper;

        public PropertyServicio(IPropertyRepositorio repositorioProperties, IOwnerRepositorio repositorioOwner, IMapper mapper)
        {
            this._repositorioProperties = repositorioProperties;
            this._repositorioOwner = repositorioOwner;
            this._mapper = mapper;
        }

        public async Task<RespuestaGeneral<List<PropertyRespuesta>>> BuscarPropertiesPorOwnerId(int idOwner)
        {
            RespuestaGeneral<List<PropertyRespuesta>> resultado = new RespuestaGeneral<List<PropertyRespuesta>>();
            if (idOwner < 0)
            {
                resultado.StatusCodeOperation = StatusCodes.Status400BadRequest;
                resultado.Exito = false;
                resultado.Mensaje = "El Id del Owner no es valido";
                resultado.Errores = new List<string>
                {
                    "El Id no puede ser null",
                    "El Id debe corresponder a un Owner valido",
                };
                resultado.Datos = null;
                return resultado;
            }
            try
            {
                bool existeOwner = await _repositorioOwner.OwnerExiste(idOwner);
                if (!existeOwner)
                {
                    return new RespuestaGeneral<List<PropertyRespuesta>>
                    {
                        Exito = false,
                        Mensaje = $"El Owner con id [{idOwner}] no existe",
                        Errores = null,
                        Datos = null,
                        StatusCodeOperation = StatusCodes.Status400BadRequest
                    };
                }
                var resultadoBusqueda = await _repositorioProperties.BuscarPropertiesPorOwnerId(idOwner);
                resultado.StatusCodeOperation = StatusCodes.Status200OK;
                resultado.Exito = true;
                resultado.Mensaje = "Consulta exitosa";
                resultado.Errores = null;
                resultado.Datos = _mapper.Map<List<PropertyRespuesta>>(resultadoBusqueda);
            }
            catch (Exception ex)
            {
                resultado.StatusCodeOperation = StatusCodes.Status400BadRequest;
                resultado.Exito = false;
                resultado.Mensaje = "Ha ocurrido un problema al consultar las properties";
                resultado.Errores = new List<string>
                {
                    ex.Message
                };
            }
            return resultado;
        }

        public async Task<RespuestaGeneral<PropertyRespuesta>> CrearProperty(PropertyRequest propertyCrear)
        {
            RespuestaGeneral<PropertyRespuesta> resultado = new RespuestaGeneral<PropertyRespuesta>();
            PropertyValidate validar = new PropertyValidate();
            var resultadoValidacion = validar.Validate(propertyCrear);
            if (resultadoValidacion.IsValid)
            {
                bool existeOwner = await _repositorioOwner.OwnerExiste(propertyCrear.IdOwner);
                if (!existeOwner)
                {
                    return new RespuestaGeneral<PropertyRespuesta>
                    {
                        Exito = false,
                        Mensaje = $"El Owner con id [{propertyCrear.IdOwner}] no existe",
                        Errores = null,
                        Datos = null,
                        StatusCodeOperation = StatusCodes.Status400BadRequest
                    };
                }
                PropertyEntidad entidadCrear = _mapper.Map<PropertyEntidad>(propertyCrear);
                var propertyCreada = await _repositorioProperties.CrearProperty(propertyCrear.IdOwner, entidadCrear);
                if (propertyCreada == null)
                {
                    resultado = new RespuestaGeneral<PropertyRespuesta>
                    {
                        Exito = false,
                        Datos = null,
                        Mensaje = "Ha ocurrido uno o varios errores",
                        Errores = new List<string>()
                        {
                            "No ha sido posible crear la Property"
                        },
                        StatusCodeOperation = StatusCodes.Status400BadRequest
                    };
                }
                else
                {
                    resultado = new RespuestaGeneral<PropertyRespuesta>
                    {
                        Exito = true,
                        Mensaje = "Property creada con exito",
                        Datos = _mapper.Map<PropertyRespuesta>(propertyCreada),
                        Errores = null,
                        StatusCodeOperation = StatusCodes.Status201Created
                    };
                }
            }
            else
            {
                resultado = new RespuestaGeneral<PropertyRespuesta>
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

        public async Task<RespuestaGeneral<PropertyImageRespuesta>> CrearImageProperty(PropertyImagesRequest imageCrear)
        {
            RespuestaGeneral<PropertyImageRespuesta> resultado = new RespuestaGeneral<PropertyImageRespuesta>();
            PropertyImageValidate validarImage = new PropertyImageValidate();
            var resultadoValidacion = validarImage.Validate(imageCrear);
            if (resultadoValidacion.IsValid)
            {
                bool existeOwner = await _repositorioOwner.OwnerExiste(imageCrear.IdOwner);
                if (!existeOwner)
                {
                    return new RespuestaGeneral<PropertyImageRespuesta>
                    {
                        Exito = false,
                        Mensaje = $"El Owner con id [{imageCrear.IdOwner}] no existe",
                        Errores = null,
                        Datos = null,
                        StatusCodeOperation = StatusCodes.Status400BadRequest
                    };
                }
                bool existeProperty = await _repositorioProperties.ExisteProperty(imageCrear.IdOwner,imageCrear.IdProperty);
                if (!existeProperty)
                {
                    return new RespuestaGeneral<PropertyImageRespuesta>
                    {
                        Exito = false,
                        Mensaje = $"Han ocurido algunos errores",
                        Errores = new List<string>()
                        {
                            $"El IdProperty [{imageCrear.IdProperty}] no esta registrado",
                            $"El IdProperty [{imageCrear.IdProperty}] no pertenece a el IdOwner [{imageCrear.IdOwner}]",
                        },
                        Datos = null,
                        StatusCodeOperation = StatusCodes.Status400BadRequest
                    };
                }

                PropertyImageEntidad entidadCrear = _mapper.Map<PropertyImageEntidad>(imageCrear);
                var propertyImageCreada = await _repositorioProperties.CrearImageProperty(imageCrear.IdProperty, entidadCrear);
                if (propertyImageCreada == null)
                {
                    resultado = new RespuestaGeneral<PropertyImageRespuesta>
                    {
                        Exito = false,
                        Datos = null,
                        Mensaje = "Ha ocurrido uno o varios errores",
                        Errores = new List<string>()
                        {
                            "No ha sido posible crear la ImageProperty"
                        },
                        StatusCodeOperation = StatusCodes.Status400BadRequest
                    };
                }
                else
                {
                    resultado = new RespuestaGeneral<PropertyImageRespuesta>
                    {
                        Exito = true,
                        Mensaje = "ImageProperty creada con exito",
                        Datos = _mapper.Map<PropertyImageRespuesta>(propertyImageCreada),
                        Errores = null,
                        StatusCodeOperation = StatusCodes.Status201Created
                    };
                }
            }
            else
            {
                resultado = new RespuestaGeneral<PropertyImageRespuesta>
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
