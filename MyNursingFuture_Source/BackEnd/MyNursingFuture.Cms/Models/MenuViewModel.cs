using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyNursingFuture.BL.Entities;

namespace MyNursingFuture.Cms.Models
{
    public class MenuViewModel
    {
        public string MenuType { get; set; }
        public string MenusJson { get; set; }
        public List<MenuEntity> MenuList { get; set; }
        public List<LinkEntity> Links { get; set; }
    }
}