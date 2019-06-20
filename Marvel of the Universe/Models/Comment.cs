using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Marvel_of_the_Universe.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public int? HeroeId { get; set; }
        public int? MovieId { get; set; }
        public Heroe Heroe { get; set; }
        public Movie Movie { get; set; }
    }
}