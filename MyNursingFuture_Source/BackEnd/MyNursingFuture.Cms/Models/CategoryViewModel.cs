using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AutoMapper;
using MyNursingFuture.BL.Entities;

namespace MyNursingFuture.Cms.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Operation { get; set; }
    }

    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryViewModel, CategoryEntity>().ReverseMap();
        }
    }
}