using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace EmergyService.DataObjects
{
    public class Service : EntityData
    {
        public string ServiceName { get; set; }
        public ServiceOwn Own { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}