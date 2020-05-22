using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebApiTemplate.Models.ResponseModels
{
    public class TodoResponseModel
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [DefaultValue(false)]
        public bool IsDone { get; set; }
    }
}
