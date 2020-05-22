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
        [DefaultValue(false)]
        public bool IsDone { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
