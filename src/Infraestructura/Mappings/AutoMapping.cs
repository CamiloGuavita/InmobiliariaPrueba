using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dominio.Entidades;
using Infraestructura.Persistencia.Modelos;

namespace Infraestructura.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Owner, OwnerEntidad>()
                .ForMember(dest => dest.Id, source => source.MapFrom(s => s.IdOwner))
                .ReverseMap();

            CreateMap<Property, PropertyEntidad>()
                .ForMember(dest => dest.Id, source => source.MapFrom(s => s.IdProperty))
                .ForMember(dest => dest.Images, source => source.MapFrom(s => s.PropertyImages))
                .ReverseMap();

            CreateMap<PropertyImage, PropertyImageEntidad>()
                .ForMember(dest => dest.Id, source => source.MapFrom(s => s.IdPropetryImage))
                .ReverseMap();
        }
    }
}
