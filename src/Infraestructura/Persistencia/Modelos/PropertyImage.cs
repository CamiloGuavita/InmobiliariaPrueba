using System;
using System.Collections.Generic;

#nullable disable

namespace Infraestructura.Persistencia.Modelos
{
    public partial class PropertyImage
    {
        public int IdPropetryImage { get; set; }
        public int IdProperty { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }

        public virtual Property IdPropertyNavigation { get; set; }
    }
}
