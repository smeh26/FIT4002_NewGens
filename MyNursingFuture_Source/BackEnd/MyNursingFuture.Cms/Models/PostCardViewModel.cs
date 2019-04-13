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
    public class PostCardViewModel
    {
        public int PostCardId { get; set; }
        [AllowHtml]
        [Required]
        public string Text { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Operation { get; set; }
    }

    public class PostCardProfile:Profile
    {
        public PostCardProfile()
        {
            CreateMap<PostCardViewModel, PostCardEntity>().ReverseMap();
        }
    }
}