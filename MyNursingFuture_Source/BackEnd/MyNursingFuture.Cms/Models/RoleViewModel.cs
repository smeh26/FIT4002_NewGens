using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyNursingFuture.BL.Entities;

namespace MyNursingFuture.Cms.Models
{
    public class RoleViewModel
    {
        public int RoleId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [DisplayName("Link name")]
        public string LinkName { get; set; }
        [AllowHtml]
        [DisplayName("What is")]
        public string WhatIs { get; set; }
        [AllowHtml]
        [DisplayName("What is their role")]
        public string WhatIsTheirRole { get; set; }
        [AllowHtml]
        public string Accountabilities { get; set; }
        [AllowHtml]
        public string Examples { get; set; }
        [AllowHtml]
        [DisplayName("Further information")]
        public string FurtherInformation { get; set; }

        [AllowHtml]
        [DisplayName("Career Pathways")]
        public string Pathways { get; set; }

        [Required]
        public string Operation { get; set; }
    }
    /// <summary>
    /// Profile used for automapper
    /// </summary>
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleEntity, RoleViewModel>().ReverseMap();
        }
    }
}