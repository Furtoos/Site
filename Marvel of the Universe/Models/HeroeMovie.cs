using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marvel_of_the_Universe.Models
{
    public class HeroeMovie
    {
        public int Id { get; set; }
        public int HeroeId { get; set; }
        public int MovieId { get; set; }
    }
}