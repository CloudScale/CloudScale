using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudScale.Api.Models
{
    public class ParsedExternalAccessToken
    {
        public string user_id { get; set; }
        public string app_id { get; set; }
    }
}
