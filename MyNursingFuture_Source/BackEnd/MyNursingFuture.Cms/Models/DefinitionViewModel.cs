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
    public class DefinitionViewModel
    {
        public int DefinitionId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, AllowHtml]
        public string Text { get; set; }
        public string Operation { get; set; }
    }

    public class DefinitionProfile : Profile
    {
        public DefinitionProfile()
        {
            CreateMap<DefinitionViewModel, DefinitionEntity>().ReverseMap();
        }
    }
}