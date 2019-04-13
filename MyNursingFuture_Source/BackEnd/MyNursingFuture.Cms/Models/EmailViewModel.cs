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
 
    public class EmailViewModel : IViewModel
    {
        public int EmailId { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [DisplayName("Subject")]
        public string Title { get; set; }
        [Required]
        [AllowHtml]
        public string Body { get; set; }

        public string Operation { get; set; }
    }

    public class EmailProfile : Profile
    {
        public EmailProfile()
        {
            CreateMap<EmailEntity, EmailViewModel>().ReverseMap();
        }
    }
}