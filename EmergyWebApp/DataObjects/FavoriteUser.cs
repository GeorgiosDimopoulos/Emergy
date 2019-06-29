using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace EmergyService.DataObjects
{
    public class FavoriteUser : EntityData
    {
        public string UserId { get; set; }
        public string FavoritePersonId { get; set; }
    }
}