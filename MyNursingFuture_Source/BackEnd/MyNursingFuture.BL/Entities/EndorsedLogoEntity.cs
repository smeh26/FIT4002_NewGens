using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.DL.Models;
using System.Web;

namespace MyNursingFuture.BL.Entities
{
    public class EndorsedLogoEntity:EndorsedLogo, IEntity
    {
        public HttpPostedFileBase ImageFile { get; set; }
        public string ImagePath { get; set; }
    }
}
