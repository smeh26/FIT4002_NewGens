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
    public class ActionViewModel
    {
        public int ActionId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [AllowHtml]
        public string Text { get; set; }
        [Required]
        public string Type { get; set; }
        public string Operation { get; set; }
    }

    public class ActionProfile : Profile
    {
        public ActionProfile()
        {
            CreateMap<ActionViewModel, ActionEntity>().ReverseMap();
        }
    }
}