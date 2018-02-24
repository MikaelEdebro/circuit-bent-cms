using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace CircuitBentCMS.Models
{
    public class Page
    {
        public int PageId { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string Title { get; set; }

        public bool ShowOnMenu { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        [MaxLength]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        [MaxLength]
        [DataType(DataType.MultilineText)]
        public string SideBarContent { get; set; }

        [ScaffoldColumn(false)]
        public DateTime LastUpdated { get; set; }

        public int SubPageToPageId { get; set; }

        public int Order { get; set; }
        public bool HomePage { get; set; }

        public string CustomPage { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        [MaxLength]
        [DataType(DataType.MultilineText)]
        public string Scripts { get; set; }

        // social media information
        [StringLength(95, ErrorMessage = "Max 95 characters")]
        public string SocialMediaTitle { get; set; }

        [StringLength(297, ErrorMessage = "Max 297 characters")]
        public string SocialMediaDescription { get; set; }
        public string SocialMediaImage { get; set; }

        // widget areas
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string WidgetArea1 { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string WidgetArea2 { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string WidgetArea3 { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string WidgetArea4 { get; set; }

        public string ExternalLinkUrl { get; set; }

    }
}