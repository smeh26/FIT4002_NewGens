using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MyNursingFuture.BL.Entities;

namespace MyNursingFuture.Cms.Models
{
    public class DomainViewModel
    {
        public int DomainId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Framework { get; set; }
        [Required]
        [AllowHtml]
        public string Text { get; set; }
        public int LinkId { get; set; }
       
        [Required]
        public string Image { get; set; }

        [Required]
        public string Icon { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public HttpPostedFileBase IconFile { get; set; }

        [Required]
        public string Operation { get; set; }

        public List<ActionEntity> ActionsList { get; set; }
        [AllowHtml]
        public string ActionsListJson { get; set; }
    }

    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<DomainViewModel, DomainEntity>().ReverseMap();
        }
    }
}