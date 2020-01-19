using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApiTemplate.Models.ResponseModels
{
    public class TodoResponseModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
    }
}
