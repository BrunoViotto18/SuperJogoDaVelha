using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuração e serviços de API Web
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Rotas de API Web
            config.MapHttpAttributeRoutes();

            SuperJogoDaVelhaEntities entities = new SuperJogoDaVelhaEntities(); 

            config.Routes.MapHttpRoute(
                name: "User",
                routeTemplate: "api/login/{username}/{senha}",
                new
                {
                    controller = "User",
                    nome = string.Empty,
                    senha = string.Empty
                }
            );
        }
    }
}
