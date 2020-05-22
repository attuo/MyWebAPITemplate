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
        [Required]
        public string Description { get; set; }
        [DefaultValue(false)]
        public bool IsDone { get; set; }

    }
}
