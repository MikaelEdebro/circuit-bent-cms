using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CircuitBentCMS.Models
{
    public class MailSettings
    {
        public int MailSettingsId { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 characters")]
        [RegularExpression(@"^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$", ErrorMessage = "Not a valid e-mail address")]
        public string Email { get; set; }
    }
}