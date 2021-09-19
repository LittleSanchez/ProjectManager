using ExpBag.UI.Startup;
using ExpBag.UI.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace ExpBag.UI.Extentions
{
    public static class HttpTokenHeaderExtention
    {
        public static void Authorize(this HttpWebRequest httpWebRequest)
        {
            var sp = ServiceProviderFactory.ServiceProvider;

            var store = sp.GetService<ApplicationStore>();
            if (store.Profile != null)
            {
                httpWebRequest.Headers.Add("Authorization: Bearer " + store.Profile.Token);
            }
        }

        public static void Authorize(this HttpRequestHeaders httpHeaders)
        {
            var sp = ServiceProviderFactory.ServiceProvider;

            var store = sp.GetService<ApplicationStore>();
            if (store.Profile != null)
            {
                httpHeaders.Add("Authorization", "Bearer " + store.Profile.Token);
            }
        }
    }
}
