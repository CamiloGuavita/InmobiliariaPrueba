using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Interfaz.Repositorio;
using AutoMapper;
using Dominio.Entidades;
using Infraestructura.Persistencia;
using Infraestructura.Persistencia.Modelos;
using Microsoft.EntityFrameworkCore;
using Aplicacion.Comun.Exceptions;

namespace Infraestructura.Repositorio
{
    public class OwnerRepositorio : IOwnerRepositorio
    {
        private InmobiliariaContext _contexto;
        private IMapper _mapper;

        public OwnerRepositorio(InmobiliariaContext contexto,IMapper mapper)
        {
            this._contexto = contexto;
            this._mapper = mapper;
        }

        public async Task<bool> OwnerExiste(int id)
        {
            Owner owner = await _contexto.Owners.FindAsync(id);
            return owner != null ? true : false;
        }

        public async Task<OwnerEntidad> BuscarOwnerPorId(int id)
        {
            Owner owner = await _contexto.Owners.Where(w => w.IdOwner == id).FirstOrDefaultAsync();
            return _mapper.Map<OwnerEntidad>(owner);
        }

        public async Task<List<OwnerEntidad>> BuscarOwnerPorNombre(string nombre)
        {
            List<Owner> owners = await _contexto.Owners.Where(w => w.Name.Contains(nombre)).ToListAsync();
            return _mapper.Map<List<OwnerEntidad>>(owners);
        }

        public async Task<List<OwnerEntidad>> ObtenerListaOwners()
        {
            List<Owner> owners = await _contexto.Owners.ToListAsync();
            return _mapper.Map<List<OwnerEntidad>>(owners);
        }

        public async Task<OwnerEntidad> CrearOwner(OwnerEntidad ownerCrear)
        {
            Owner ownerDB = _mapper.Map<Owner>(ownerCrear); 
            _contexto.Owners.Add(ownerDB);
            await _contexto.SaveChangesAsync();
            if(ownerDB.IdOwner > 0)
            {
                ownerCrear.Id = ownerDB.IdOwner;
            }
            else
            {
                ownerCrear = null;
            }
            return ownerCrear;
        }
    }
}
