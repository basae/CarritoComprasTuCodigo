using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using CarritoComprasTuCodigo.Models;

namespace CarritoComprasTuCodigo
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // Para permitir que los usuarios de este sitio inicien sesión con sus cuentas de otros sitios como, por ejemplo, Microsoft, Facebook y Twitter,
            // es necesario actualizar este sitio. Para obtener más información, visite http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "559184974274230",
                appSecret: "fe92c2add8e9caac269fd6786a079b22");

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
