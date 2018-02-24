using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CircuitBentCMS.Models
{
    public class File
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}