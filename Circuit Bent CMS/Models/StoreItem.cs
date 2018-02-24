using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CircuitBentCMS.Models
{
    public class StoreItem
    {
        public int StoreItemId { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(100, ErrorMessage = "Max 100 characters")]
        public string Title { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        [MaxLength]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(10, ErrorMessage = "Max 10 characters")]
        public string Price { get; set; }
        
        public bool SoldOut { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Column(TypeName = "ntext")]
        [MaxLength]
        public string PaypalCode { get; set; }

        public int Order { get; set; }

        public virtual IList<StoreItemImage> StoreItemImages { get; set; }

    }
}