using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApiTemplate.Web.Models.RequestModels
{
    public class TodoRequestModel
    {
        public string Description { get; set; }
        public bool IsDone { get; set; }

    }
}
