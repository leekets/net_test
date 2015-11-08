using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace net_test.Models
{
    public class List
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int categoryID { get; set; }
        [Required]
        public string subTitle { get; set; }
        [Required]
        public string subject { get; set; }
        public string sen { get; set; }
        public string image { get; set; }
        public string hint { get; set; }
        public int sort { get; set; }
    }
}