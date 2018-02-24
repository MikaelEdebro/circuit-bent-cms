using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace CircuitBentCMS.Models
{
    public class Blog
    {

        public int BlogId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(100, ErrorMessage = "Max 100 characters")]
        public string Headline { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Column(TypeName = "ntext")]
        [MaxLength]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Text { get; set; }

        public bool EnableComments { get; set; }
    }
}