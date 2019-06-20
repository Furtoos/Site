using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Marvel_of_the_Universe.Models
{
    public class Heroe
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Actor { get; set; }
        [Required]
        public string Skills { get; set; }
        [Required]
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