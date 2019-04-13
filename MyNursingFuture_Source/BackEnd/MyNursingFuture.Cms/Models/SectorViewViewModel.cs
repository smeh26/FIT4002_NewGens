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
    public class SectorViewViewModel
    {
        public int SectorViewId { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Framework { get; set; }
        [Required]
        public int SectorId { get; set; }
        [AllowHtml]
        [Required]
        public string Intro { get; set; }
        public string Video { get; set; }
        [AllowHtml]
        [DisplayName("More stories")]
        public string MoreStories { get; set; }
        [AllowHtml]
        public string CareerPathways { get; set; }
        [AllowHtml]
        [DisplayName("Work environments")]
        public string WorkEnvironments { get; set; }
        [AllowHtml]
        [DisplayName("Career opportunities")]
        public string CareerOpportunities { get; set; }
        [AllowHtml]
        [DisplayName("Education opportunities")]
        public string EducationOpportunities { get; set; }
        [AllowHtml]
        [DisplayName("Contact text")]
        public string ContactText { get; set; }

        [AllowHtml]
        [DisplayName("Online Resources")]
        public string OnlineResources { get; set; }
    }

    public class SectorViewProfile : Profile
    {
        public SectorViewProfile()
        {
            CreateMap<SectorViewEntity, SectorViewViewModel>().ReverseMap();
        }
    }
}