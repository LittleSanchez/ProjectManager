using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Application.Constans
{
    public static class NetworkConfig
    {
        public static string ServerUrl { get; private set; } = "https://localhost:5001/";//"https://expbag.tk/";
        //APIs
        public static string ApiAuthPath { get; private set; } = "api/auth/";
        public static string ApiModulesPath { get; private set; } = "api/module/";

    }
}
