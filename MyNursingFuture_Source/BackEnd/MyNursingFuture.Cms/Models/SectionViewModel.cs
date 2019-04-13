using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AutoMapper;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.DL.Models;
using Newtonsoft.Json;

namespace MyNursingFuture.Cms.Models
{
    public interface IViewModel
    {
        string Operation { get; set; }
    }

    public class SectionViewModel : IViewModel
    {
        public int SectionId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Operation { get; set; }
        public List<ContentItemEntity> ContentItems { get; set; }

        public string ContentItemsJson { get; set; }
    }

    /// <summary>
    /// Profile used for automapper
    /// </summary>
    public class SectionProfile : Profile
    {
        public SectionProfile()
        {
            CreateMap<SectionEntity, SectionViewModel>().ReverseMap();
        }
    }
    
}