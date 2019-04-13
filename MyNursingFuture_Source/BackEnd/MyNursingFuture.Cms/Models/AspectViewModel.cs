using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MyNursingFuture.BL.Entities;

namespace MyNursingFuture.Cms.Models
{
    public class AspectViewModel
    {
        public int AspectId { get; set; }
        [Required]
        [DisplayName("Domain")]
        public int DomainId { get; set; }
        [Required]
        public string Title { get; set; }
        [AllowHtml]
        public string Text { get; set; }
        [AllowHtml]
        public string Examples { get; set; }
        [AllowHtml]
        [DisplayName("Online Resources")]
        public string OnlineResources { get; set; }
        [AllowHtml]
        [DisplayName("Further Education")]
        public string FurtherEducation { get; set; }
        [AllowHtml]
        [DisplayName("People to Contact")]
        public string PeopleContact { get; set; }
        [AllowHtml]
        public string Levels { get; set; }
        [Required]
        public string Operation { get; set; }
        public List<ActionEntity> ActionsList { get; set; }
        [AllowHtml]
        public string ActionsListJson { get; set; }
    }

    public class AspectsProfile : Profile
    {
        public AspectsProfile()
        {
            CreateMap<AspectViewModel, AspectEntity>().ReverseMap();
        }
    }
}