using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace net_test.Models
{
    public class TestDbContext : DbContext
    {
        public TestDbContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<List> List { get; set; }
    }
}