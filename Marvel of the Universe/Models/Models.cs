using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Marvel_of_the_Universe.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Write email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Write password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords is not compare")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string PasswordConfirm { get; set; }
    }
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get { return Convert.ToInt32(Math.Ceiling((decimal)TotalItems / PageSize)); }
        }
    }
    public class IndexViewHeroes
    {
        public IEnumerable<Heroe> Heroes { get; set; }
        public PageInfo PageInfo { get; set; }
    }
    public class IndexViewMovies
    {
        public IEnumerable<Movie> Movies { get; set; }
        public PageInfo PageInfo { get; set; }
    }
    public class ListInfo
    {
        public int ListNumber { get; set; }
        public int ListSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalList
        {
            get { return Convert.ToInt32(Math.Ceiling((decimal)TotalItems / ListSize)); }
        }
    }
    public class ViewHeroe
    {
        public Heroe Heroe { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
        public ListInfo ListInfo { get; set; }
    }
    public class ViewMovie
    {
        public Movie Movie { get; set; }
        public IEnumerable<Heroe> Heroes { get; set; }
        public ListInfo ListInfo { get; set; }
    }
    public class ViewAdminHeroe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Actor { get; set; }
        public string Skills { get; set; }
        public string[] Movies { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}