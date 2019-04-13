using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MyNursingFuture.BL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyNursingFuture.Cms.Models
{
    public class ArticleViewModel
    {
        public int ArticleId { get; set; }
        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string Operation { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string DateString { get; set; }
        public List<ContentItemEntity> ContentItems { get; set; }
        public string Type { get; set; }
    }

    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleViewModel, ArticleEntity>().ReverseMap();
        }
    }
}