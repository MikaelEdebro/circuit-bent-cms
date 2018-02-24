using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace CircuitBentCMS.Models
{
    public class SiteSettings
    {
        public int SiteSettingsId { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Max 500 characters")]
        public string MetaDescription { get; set; }

        [StringLength(5, ErrorMessage = "Max 5 characters")]
        public string MetaLanguage { get; set; }
        
        public string Favicon { get; set; }
        
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [MaxLength]
        [DataType(DataType.MultilineText)]
        public string FooterText { get; set; }
        
        // Images
        public string BackgroundImage { get; set; }
        public bool BackgroundImageFullScreen { get; set; }

        public string HeaderImage { get; set; }

        public string FooterImage { get; set; }

        public string MobileImage { get; set; }

        

        // Color settings
        [StringLength(11)]
        public string BackgroundColor { get; set; }

        [StringLength(11)]
        public string ContainerColor { get; set; }

        [Range(0, 100, ErrorMessage = "Value must be between 0 and 100")]
        public int ContainerOpacity { get; set; }

        [StringLength(11)]
        public string HeadingColor { get; set; }

        [StringLength(11)]
        public string TextColor { get; set; }

        [StringLength(11)]
        public string LinkColor { get; set; }

        // Fonts & sizes
        [StringLength(150)]
        public string HeadingFont { get; set; }
        public string HeadingSize { get; set; }

        [StringLength(150)]
        public string TextFont { get; set; }
        public string TextSize { get; set; }

        [Range(400, 1800, ErrorMessage = "Must be between 400 and 1800")]
        public int ContainerWidth { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Column(TypeName = "ntext")]
        [MaxLength]
        public string CustomCSS { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Column(TypeName = "ntext")]
        [MaxLength]
        public string CustomHtml { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string GoogleAnalytics { get; set; }


    }
}