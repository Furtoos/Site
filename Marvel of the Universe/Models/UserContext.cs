using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Marvel_of_the_Universe.Models
{
    public class UserContext : DbContext 
    {
        public UserContext() :
            base("MarvelContext")
        { }
        public DbSet<User> Users { get; set; }
    }
}