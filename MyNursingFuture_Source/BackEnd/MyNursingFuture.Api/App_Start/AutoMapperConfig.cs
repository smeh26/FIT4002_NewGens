using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.DL;
using MyNursingFuture.Util;
using MyNursingFuture.Api.Models;
using MyNursingFuture.BL.Managers;
using AutoMapper;
using AutoMapper.Configuration;

namespace MyNursingFuture.Api
{
    public class AutoMapperConfig
    {

        public static void Initialize()
        {

            var cfg = new MapperConfigurationExpression();
            cfg.CreateMap<UserEntity, UserModel>().ReverseMap();
            cfg.CreateMap<UserEntity, UserModelSecured>().ReverseMap();
            cfg.CreateMap<EmployerEntity, EmployerModel>().ReverseMap();
            cfg.CreateMap<UserEntity, EmployerModelSecured>().ReverseMap();



            Mapper.Initialize(cfg);

        }
    }
}