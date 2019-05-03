using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marvel_of_the_Universe.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public int YearRelease { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Heroe> Heroes { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public Movie()
        {
            Heroes = new List<Heroe>();
            Likes = new List<Like>();
            Comments = new List<Comment>();
        }
    }
}