using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using UniSA.Api.Controller;
using UniSA.Api.Services;
using UniSA.Data;
using UniSA.Api.ViewModels;

namespace UniSA.Api
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<MappingProfile>();
            });
        }
    }



    public class MappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public MappingProfile()
        {
            CreateMap<ApplicationUser, RegisterViewModel>()
                .ForMember(m => m.FirstName, map => map.MapFrom(vm => vm.FirstName))
                .ForMember(m => m.LastName, map => map.MapFrom(vm => vm.LastName))
                .ForMember(m => m.Email, map => map.MapFrom(vm => vm.Email))
                .ForMember(m => m.UserName, map => map.MapFrom(vm => vm.UserName))
                .ForMember(m => m.Roles, map => map.MapFrom(vm => vm.Roles));

            CreateMap<RegisterViewModel, ApplicationUser>()
                .ForMember(vm => vm.FirstName, map => map.MapFrom(m => m.FirstName))
                .ForMember(vm => vm.LastName, map => map.MapFrom(m => m.LastName))
                .ForMember(vm => vm.Email, map => map.MapFrom(m => m.Email))
                .ForMember(vm => vm.UserName, map => map.MapFrom(m => m.UserName))
                .ForMember(vm => vm.Roles, map => map.MapFrom(m => m.Roles));
        }
    }

}