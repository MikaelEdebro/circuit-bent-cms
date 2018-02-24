using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CircuitBentCMS.Models
{
    public class Show
    {
        public int ShowId { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string Venue { get; set; }

        [StringLength(20, ErrorMessage = "Max 20 characters")]
        public string Time { get; set; }

        [StringLength(20, ErrorMessage = "Max 20 characters")]
        public string Price { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string LinkToEvent { get; set; }

        public bool Cancelled { get; set; }

    }
}