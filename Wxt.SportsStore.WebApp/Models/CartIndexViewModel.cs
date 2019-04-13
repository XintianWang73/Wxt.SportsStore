using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wxt.SportsStore.Domain.Entities;

namespace Wxt.SportsStore.WebApp.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}