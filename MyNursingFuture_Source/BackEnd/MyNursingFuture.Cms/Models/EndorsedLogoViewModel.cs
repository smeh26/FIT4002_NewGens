using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MyNursingFuture.BL.Entities;

namespace MyNursingFuture.Cms.Models
{
    public class EndorsedLogoViewModel
    {
        public int EndorsedLogoId { get; set; }
        [AllowHtml]
        [Required]
        public string Name { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        public string Image { get; set; }

        [Required]
        public string Operation { get; set; }
    }

    public class EndorsedLogoProfile : Profile
    {
        public EndorsedLogoProfile()
        {
            CreateMap<EndorsedLogoViewModel, EndorsedLogoEntity>().ReverseMap();
        }
    }
}