using ApplicationCore.Dtos;
using AspNetCoreWebApiTemplate.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApiTemplate.Mappings.ToModel
{
    public static class TodoToModel
    {
        public static TodoResponseModel ToResponseModel(this TodoDto dto)
        {
            return new TodoResponseModel
            {
                Id = dto.Id,
                Text = dto.Text,
                IsDone = dto.IsDone
            };
        }

        public static IEnumerable<TodoResponseModel> ToResponseModels(this IEnumerable<TodoDto> dtos)
        {
            return dtos.Select(c => c.ToResponseModel());
        }
    }
}
