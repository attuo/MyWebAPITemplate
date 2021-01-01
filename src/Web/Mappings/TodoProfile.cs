using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyWebAPITemplate.Core.Dtos;
using MyWebAPITemplate.Core.Entities;
using MyWebAPITemplate.Models.ResponseModels;
using MyWebAPITemplate.Web.Models.RequestModels;

namespace MyWebAPITemplate.Web.Mappings
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<TodoRequestModel, TodoDto>().ForMember(c => c.Id, opt => opt.Ignore());


            CreateMap<TodoDto, TodoResponseModel>();
            CreateMap<TodoDto, TodoEntity>();
            CreateMap<TodoEntity, TodoDto>();
        }
    }
}
