using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class PropertyTraceEntidad
    {
        public int Id { get; set; }
        public DateTime? DateSale { get; set; }
        public string Name { get; set; }
        public decimal? Value { get; set; }
        public decimal? Tax { get; set; }
    }
}
