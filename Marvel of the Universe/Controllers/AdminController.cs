using Marvel_of_the_Universe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marvel_of_the_Universe.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private MarvelContext context = new MarvelContext();

        // GET: Admin/Create
        public ActionResult Index()
        {
            IEnumerable<Heroe> heroes = context.Heroes.ToList();
            return View(heroes);
        }
        public ActionResult CreateHeroe()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult CreateHeroe(ViewAdminHeroe _heroe)
        {
            if(ModelState.IsValid && _heroe.Image != null)
            {
                string savedFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Images\\Heroes\\",
                   Path.GetFileName(_heroe.Image.FileName));
                _heroe.Image.SaveAs(savedFileName);
                Heroe heroe = new Heroe
                {
                    Name = _heroe.Name,
                    Actor = _heroe.Actor,
                    Skills = _heroe.Skills,
                    Image = _heroe.Image.FileName
                };
                foreach(var id_movie in _heroe.Movies)
                {
                    heroe.Movies.Add(context.Movies.Find(Convert.ToInt32(id_movie)));
                }
                context.Heroes.Add(heroe);
                context.SaveChanges();
                Heroe lastHeroe = context.Heroes.Where(h => h.Name == heroe.Name).FirstOrDefault();
                return RedirectToAction("Heroe", "Marvel", new { id = lastHeroe.Id });
            }            
            return View();
        }

        // GET: Admin/Edit
        public ActionResult EditHeroe(int? id)
        {
            if (id == null) return HttpNotFound();
            Heroe heroe = context.Heroes.Find(id);
            return View(heroe);
        }

        // POST: Admin/Edit
        [HttpPost]
        public ActionResult EditHeroe(ViewAdminHeroe _heroe)
        {
            if (ModelState.IsValid && _heroe.Image != null)
            {
                Heroe heroe = context.Heroes.Find(_heroe.Id);
                //удаляем старую картинку
                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Images\\Heroes\\",
                   Path.GetFileName(heroe.Image));
                FileInfo image = new FileInfo(imagePath);
                if(image.Exists) image.Delete();
                //добавляем новую
                imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Images\\Heroes\\",
                   Path.GetFileName(_heroe.Image.FileName));
                _heroe.Image.SaveAs(imagePath);
                heroe.Name = _heroe.Name;
                heroe.Actor = _heroe.Actor;
                heroe.Skills = _heroe.Skills;
                heroe.Image = _heroe.Image.FileName;
                heroe.Movies = null;
                foreach (var id_movie in _heroe.Movies)
                {
                    heroe.Movies.Add(context.Movies.Find(Convert.ToInt32(id_movie)));
                }
                context.Entry(heroe).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Heroe", "Marvel", new { id = heroe.Id });
            }
            return View();
        }

        public ActionResult DeleteHeroe(int id)
        {
            Heroe heroe = context.Heroes.Find(id);
            context.Heroes.Remove(heroe);
            context.SaveChanges();
            return RedirectToAction("Index","Admin",null);
        }
    }
}
