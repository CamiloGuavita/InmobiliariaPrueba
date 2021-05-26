using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Comun.Respuestas;
using Dominio.Entidades;
using Aplicacion.Comun.Request;

namespace Aplicacion.Comun.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<OwnerEntidad, OwnerRespuesta>().ReverseMap();
            CreateMap<OwnerRequest, OwnerEntidad>();

            CreateMap<PropertyEntidad, PropertyRespuesta>().ReverseMap();
            CreateMap<PropertyImageEntidad, PropertyImageRespuesta>().ReverseMap();

            CreateMap<PropertyRequest, PropertyEntidad>();
            CreateMap<PropertyImagesRequest, PropertyImageEntidad>();
        }
    }
}
