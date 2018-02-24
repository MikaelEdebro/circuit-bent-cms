using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CircuitBentCMS.Models
{
    public class SocialMedia
    {
        public int SocialMediaId { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string Email { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string Facebook { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string Myspace { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string Soundcloud { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string Twitter { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string Instagram { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string GooglePlus { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string Youtube { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string Spotify { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string Vimeo { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string Bandcamp { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string Flickr { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string LinkedIn { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string LastFm { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string Pinterest { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string Tumblr { get; set; }

    }
}