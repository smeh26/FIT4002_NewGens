using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AutoMapper;
using MyNursingFuture.BL.Entities;

namespace MyNursingFuture.Cms.Models
{
    public class SectorViewModel : IViewModel
    {
        public int SectorId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Operation { get; set; }

        public SectorViewEntity SectorRn { get; set; }
        public SectorViewEntity SectorEn { get; set; }
    }

    public class SectorProfile : Profile
    {
        public SectorProfile()
        {
            CreateMap<SectorEntity, SectorViewModel>().ReverseMap();
        }
    }
}