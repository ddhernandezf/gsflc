using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using Transporte.BL;
using Transporte.Model.Security;
using UserBL = Transporte.BL.Security.User;

namespace Transporte.Security
{
    public static class BasicAuthentication
    {
        public static string GetUserName(string basicCredential)
        {
            return GetCredential(basicCredential)["userName"];
        }

        public static Login GetLogin(string basicCredential)
        {
            Dictionary<string, string> login = GetCredential(basicCredential);
            return new Login { user = login["userName"], password = login["password"] };
        }

        public static bool Authenticate(string basicAuth)
        {
            Dictionary<string, string> credential = GetCredential(basicAuth);

            UserInfo result = new UserBL(Tool.configuration).Authenticate(new Login()
                {user = credential["userName"], password = credential["password"]});

            return result != null;
        }

        private static Dictionary<string, string> GetCredential(string basicAuth)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (basicAuth.IsNull())
                throw new Exception("No se reconoce la cadena de conexión al API");

            string[] credential = GetCredentialString(basicAuth);
            
            string userName = credential[0],
                password = credential[1];

            result.Add("userName", userName);
            result.Add("password", password);

            return result;
        }

        private static string[] GetCredentialString(string basicAuth)
        {
            string[] credential = null;

            try
            {
                credential = Encoding.ASCII.GetString(Convert.FromBase64String(basicAuth)).Split(":");
            }
            catch
            {
                byte[] bytes = Convert.FromBase64String(AuthenticationHeaderValue.Parse(basicAuth).Parameter);
                credential = Encoding.UTF8.GetString(bytes).Split(new[] { ':' }, 2);
            }

            return credential;
        }
    }
}
