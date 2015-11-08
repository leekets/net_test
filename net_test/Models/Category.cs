using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace net_test.Models
{
    public class Category
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public String title { get; set; }
        [Required]
        public int parentID { get; set; }
        [Required]
        public int sortNum { get; set; }
    }
}