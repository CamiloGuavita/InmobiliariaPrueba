using Aplicacion.Comun.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class PropertyImageValidate : AbstractValidator<PropertyImagesRequest>
    {
        public PropertyImageValidate()
        {
            RuleFor(x => x.IdOwner).GreaterThan(0);
            RuleFor(x => x.IdProperty).GreaterThan(0);
            RuleFor(x => x.File).NotEmpty();
        }
    }
}
