using System.ComponentModel.DataAnnotations;

namespace CloudScale.Web.Models
{
    public class ExternalLoginListViewModel
    {
        public string Action { get; set; }
        public string ReturnUrl { get; set; }
    }
}
