using Aplicacion.Interfaz.Repositorio;
using AutoMapper;
using Dominio.Entidades;
using Infraestructura.Persistencia;
using Infraestructura.Persistencia.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Repositorio
{
    public class PropertyRepositorio : IPropertyRepositorio
    {
        private InmobiliariaContext _contexto;
        private IMapper _mapper;

        public PropertyRepositorio(InmobiliariaContext contexto, IMapper mapper)
        {
            this._contexto = contexto;
            this._mapper = mapper;
        }

        public async Task<List<PropertyEntidad>> BuscarPropertiesPorOwnerId(int idOwner)
        {
            List<Property> propiedades = await _contexto.Properties.Where(w => w.IdOwner == idOwner).ToListAsync();
            if(propiedades.Count > 0)
            {
                foreach (Property propiedad in propiedades)
                {
                    propiedad.PropertyImages = await _contexto.PropertyImages
                                                        .Where(w => w.IdProperty == propiedad.IdProperty && w.Enabled == true)
                                                        .ToListAsync();
                }
            }
            return _mapper.Map<List<PropertyEntidad>>(propiedades);
        }

        public async Task<PropertyEntidad> CrearProperty(int idOwner, PropertyEntidad propertyCrear)
        {
            Property propertyDB = _mapper.Map<Property>(propertyCrear);
            propertyDB.IdOwner = idOwner;
            _contexto.Properties.Add(propertyDB);
            await _contexto.SaveChangesAsync();
            if (propertyDB.IdProperty > 0)
            {
                propertyCrear.Id = propertyDB.IdProperty;
            }
            else
            {
                propertyCrear = null;
            }
            return propertyCrear;
        }

        public async Task<bool> ExisteProperty(int idOwner, int idProperty)
        {
            Property property = await _contexto.Properties.Where(w => w.IdOwner == idOwner && w.IdProperty == idProperty).FirstOrDefaultAsync();
            return property != null ? true : false;
        }

        public async Task<PropertyImageEntidad> CrearImageProperty(int idProperty, PropertyImageEntidad imageCrear)
        {
            PropertyImage propertyImageDB = _mapper.Map<PropertyImage>(imageCrear);
            propertyImageDB.IdProperty = idProperty;
            propertyImageDB.Enabled = true;
            _contexto.PropertyImages.Add(propertyImageDB);
            await _contexto.SaveChangesAsync();
            if (propertyImageDB.IdPropetryImage > 0)
            {
                imageCrear.Id = propertyImageDB.IdPropetryImage;
            }
            else
            {
                imageCrear = null;
            }
            return imageCrear;
        }

    }
}
