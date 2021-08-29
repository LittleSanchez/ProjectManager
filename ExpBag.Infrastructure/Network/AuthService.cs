using ExpBag.Application.Constans;
using ExpBag.Application.Interfaces;
using ExpBag.Domain.CQRSObjects;
using ExpBag.Domain.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExpBag.Infrastructure.Network
{

    public class AuthService : IAuthService
    {

        private readonly string ServerUrl = NetworkConfig.ServerUrl;
        private readonly string ApiAuthPath = NetworkConfig.ApiAuthPath;
        private string LoginUrl;
        private string RegistrationUrl;

        public AuthService()
        {
            LoginUrl = Path.Combine(ServerUrl, ApiAuthPath, "login");
            RegistrationUrl = Path.Combine(ServerUrl, ApiAuthPath, "registration");
        }

        public async Task<UserDTO> Login(LoginQuery loginQuery)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(LoginUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(loginQuery);
                    await streamWriter.WriteAsync(json);
                }
                var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();

                if (httpResponse == null)
                {
                    return null;
                }
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = JsonConvert.DeserializeObject<UserDTO>(await streamReader.ReadToEndAsync());
                    return result;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<UserDTO> Register(RegistrationCommand registrationCommand)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(LoginUrl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(registrationCommand);
                await streamWriter.WriteAsync(json);
            }

            var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = JsonConvert.DeserializeObject<UserDTO>(await streamReader.ReadToEndAsync());
                return result;
            }
        }
    }
}
