﻿using AutoMapper;
using progresssoft_task.Server.DTOs;
using progresssoft_task.Server.Models;

namespace progresssoft_task.Server.Common.Mapping
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper() 
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BusinessCard, BusinessCardDto>();
                cfg.CreateMap<BusinessCard, BusinessCardDto>().ReverseMap();
                cfg.CreateMap<BusinessCard, CreateCardRequestDto>();
                cfg.CreateMap<BusinessCard, CreateCardRequestDto>().ReverseMap();
                cfg.CreateMap<BusinessCardDto, CreateCardRequestDto>();
                cfg.CreateMap<BusinessCardDto, CreateCardRequestDto>().ReverseMap();
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
