using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebApiTemplate.Models.ResponseModels
{
    public class TodoResponseModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
    }
}
