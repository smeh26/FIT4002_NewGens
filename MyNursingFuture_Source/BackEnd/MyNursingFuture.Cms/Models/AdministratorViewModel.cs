using AutoMapper;
using MyNursingFuture.BL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNursingFuture.Cms.Models
{
    public class AdministratorViewModel
    {
        public int AdministratorId { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
        public string Hash { get; set; }
        public string Name { get; set; }
        [Required]
        public string Operation { get; set; }
    }

    public class AdministratorProfile : Profile
    {
        public AdministratorProfile()
        {
            CreateMap<AdministratorViewModel, AdministratorEntity>().ReverseMap();
        }
    }
}