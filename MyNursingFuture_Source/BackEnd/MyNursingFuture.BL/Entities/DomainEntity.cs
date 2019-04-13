using System;
using System.Collections.Generic;
using System.Web;
using MyNursingFuture.DL.Models;

namespace MyNursingFuture.BL.Entities
{
    public class DomainEntity: Domain, IEntity
    {
        public List<ActionEntity> ActionsList { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public HttpPostedFileBase IconFile { get; set; }

        public string ImagePath { get; set; }
    }
}
