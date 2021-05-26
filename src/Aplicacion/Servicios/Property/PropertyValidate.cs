using Aplicacion.Comun.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class PropertyValidate : AbstractValidator<PropertyRequest>
    {
        public PropertyValidate()
        {
            RuleFor(x => x.IdOwner).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.CodeInternal).NotEmpty();
            RuleFor(x => x.Year).GreaterThan(0);

            /*
            public int IdOwner { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public decimal Price { get; set; }
            public string CodeInternal { get; set; }
            public int Year { get; set; }
            */
        }
    }
}
