using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Marvel_of_the_Universe.Models
{
    public class Heroe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Actor { get; set; }
        public string Skills { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public Heroe()
        {
            Movies = new List<Movie>();
            Likes = new List<Like>();
            Comments = new List<Comment>();
        }
    }

}