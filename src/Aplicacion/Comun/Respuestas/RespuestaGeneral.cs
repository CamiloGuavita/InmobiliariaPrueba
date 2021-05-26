using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Aplicacion.Comun.Respuestas
{
    public class RespuestaGeneral <T>
    {
        public bool Exito { get; set; } 
        public string Mensaje { get; set; }
        public List<string> Errores { get; set; }
        public T Datos { get; set; }

        [JsonIgnore]
        public int StatusCodeOperation { get; set; }
        
        public RespuestaGeneral()
        {

        }
    }
}
