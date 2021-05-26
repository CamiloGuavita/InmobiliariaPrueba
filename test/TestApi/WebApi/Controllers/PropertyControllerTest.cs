using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Comun.Respuestas;
using Aplicacion.Interfaz.Repositorio;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TestApi.Infraestructura.Repositorio;
using WebApi;

namespace TestApi.WebApi.Controllers
{
    [TestFixture]
    public class PropertyControllerTest
    {
        private static WebApplicationFactory<Startup> _factory;

        [OneTimeSetUp]
        public void IniciarObjetos()
        {
            _factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting("https_port", "5001").UseEnvironment("Testing");
                    builder.ConfigureServices(services =>
                    {
                        services.AddScoped<IOwnerRepositorio, OwnerRepositorioMock>();
                        services.AddScoped<IPropertyRepositorio, PropertyRepositorioMock>();
                    });
                });
        }


        [Test]
        public async Task GetAllByOwnwer()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/Property/GetAllByOwnwer?idOwner=1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            RespuestaGeneral<List<PropertyRespuesta>> resultado = Newtonsoft.Json.JsonConvert.DeserializeObject<RespuestaGeneral<List<PropertyRespuesta>>>(result);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(true, resultado.Exito);
            Assert.IsNull(resultado.Errores);
            Assert.IsNotNull(resultado.Datos);
            //Assert.AreEqual(1, resultado.Datos.Count);
        }
    }
}
