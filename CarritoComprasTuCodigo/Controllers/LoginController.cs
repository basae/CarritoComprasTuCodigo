using CarritoComprasTuCodigo.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace CarritoComprasTuCodigo.Controllers
{
    [InitializeSimpleMembership]
    [ValidateAntiForgeryToken]
    public class LoginController : ApiController
    {
        // GET api/login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/login
        public Models.UserMovil Post([FromBody]Models.LoginModel _login)
        {
            if (ModelState.IsValid)
            {
                //var pass = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String( "AJZ/LPqRtUuKjA2vHzCwmbPSXFFqMj5ja4jitT2YJqdXZ+9N1sdfKmj1VG6FZO7Stg=="));
                
                if (WebSecurity.Login(_login.UserName, _login.Password))
                {
                    byte[] TokenByte=System.Text.Encoding.UTF8.GetBytes(_login.UserName+"|"+_login.Password);
                    WebSecurity.Logout();
                    return new Models.UserMovil { Token = Convert.ToBase64String(TokenByte), Username = _login.UserName };
                }
                else
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "contraseña o password incorrectos"));
            }
            else
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Objeto invalido"));
        }

        // PUT api/login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/login/5
        public void Delete(int id)
        {
        }
    }
}
