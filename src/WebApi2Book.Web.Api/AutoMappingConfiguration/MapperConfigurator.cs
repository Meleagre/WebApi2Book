using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Dtos = WebApi2Book.Api.Models;
using Entities = WebApi2Book.Data.Entities;

namespace WebApi2Book.Web.Api.AutoMappingConfiguration
{
    public static class MapperConfigurator
    {
        public static MapperConfiguration Configure()
        {
            var mapConfig = new MapperConfiguration(ConfigureMap);
            mapConfig.AssertConfigurationIsValid();
            return mapConfig;
        }

        private static void ConfigureMap(IMapperConfiguration cfg)
        {
            cfg.CreateMap<Dtos.NewTask, Entities.Task>()
                .ForMember(opt => opt.Version, x => x.Ignore())
                .ForMember(opt => opt.CreatedBy, x => x.Ignore())
                .ForMember(opt => opt.TaskId, x => x.Ignore())
                .ForMember(opt => opt.CreatedDate, x => x.Ignore())
                .ForMember(opt => opt.CompletedDate, x => x.Ignore())
                .ForMember(opt => opt.Status, x => x.Ignore())
                .ForMember(opt => opt.Users, x => x.Ignore());

            cfg.CreateMap<Entities.Task, Dtos.Task>()
                .ForMember(opt => opt.Links, x => x.Ignore())
                .ForMember(opt => opt.Assignees, x => x.ResolveUsing<TaskAssigneesResolver>());

            cfg.CreateMap<Dtos.User, Entities.User>()
                .ForMember(opt => opt.Version, x => x.Ignore());

            cfg.CreateMap<Entities.User, Dtos.User>()
                .ForMember(opt => opt.Links, x => x.Ignore());

            cfg.CreateMap<Dtos.Status, Entities.Status>()
                .ForMember(opt => opt.Version, x => x.Ignore());

            cfg.CreateMap<Entities.Status, Dtos.Status>();
        }
    }
}