using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApplication.Controllers
{
    public class UserController : ApiController
    {
        public Usuario Get(string username, string senha)
        {
            SuperJogoDaVelhaEntities entities = new SuperJogoDaVelhaEntities();
            Usuario logado = entities.Usuario.FirstOrDefault(u => u.username == username && u.senha == senha);
            if (logado == null)
                return null;
            return logado;
        }
    }
}