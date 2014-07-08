using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Configuration;

namespace Tests
{
    public class Class1
    {


        public static void Main()
        {
            var authenticationUrl = ConfigurationManager.AppSettings["identityApiTokensURL"];
            var username = ConfigurationManager.AppSettings["username"];
            var  password = ConfigurationManager.AppSettings["password"];
            var  tenantId = ConfigurationManager.AppSettings["tenantId"];
            var request = WebRequest.Create(authenticationUrl);
            request.ContentType = "application/json";
            request.Method = "POST";


            string authString = "{\"auth\":{"
            + "\"passwordCredentials\":{"
            + "\"username\":\"" + username + "\","
            + "\"password\":\"" + password + "\""
            + "},\"tenantName\":\"" + tenantId + "\"}}";

            var encoding = new ASCIIEncoding();
            var bytes = encoding.GetBytes(authString);
            
            using (var stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            var response = request.GetResponse();
            string token;
            if (response != null)
            {
                string jsonResponse;
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    jsonResponse = sr.ReadToEnd();
                }
                
            }
        }

    }
}
