using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
namespace Marvel_of_the_Universe.Models
{
    public class MarvelDbInitializer : DropCreateDatabaseAlways<MarvelContext>
    {
        public string connect_string;
        MySqlConnection conn;
        protected override void Seed(MarvelContext context)
        {
            connect_string = "server=localhost;user=root;database=marvel;password=Furtoo380977856617ss;";
            conn = new MySqlConnection(connect_string);
            MarvelContext db = new MarvelContext();
            string zapros;
            MySqlCommand command;
            MySqlDataReader reader;
            conn.Open();
            zapros = "select * from heroe";
            command = new MySqlCommand(zapros, conn);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Heroe heroe = new Heroe();
                heroe.Id = Convert.ToInt32(reader[0]);
                heroe.Name = reader[1].ToString();
                heroe.Actor = reader[2].ToString();
                heroe.Skills = reader[3].ToString();
                string image = reader[4].ToString();
                int ind = 0;
                for (int i = image.Length - 1; i > 0; i--)
                {
                    if (image[i] != '-')
                    {
                        ind++;
                    }
                    else
                    {
                        break;
                    }
                }
                image = image.Remove(0, image.Length - ind);
                heroe.Image = image;
                db.Heroes.Add(heroe);
            }
            conn.Close();
            db.SaveChanges();
            conn.Open();
            zapros = "select * from movie";
            command = new MySqlCommand(zapros, conn);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Movie movie = new Movie();
                movie.Id = Convert.ToInt32(reader[0]);
                movie.Name = reader[1].ToString();
                movie.Genre = reader[2].ToString();
                movie.YearRelease = Convert.ToInt32(reader[3]);
                string image = reader[4].ToString();
                int ind = 0;
                for (int i = image.Length - 1; i > 0; i--)
                {
                    if (image[i] != '-')
                    {
                        ind++;
                    }
                    else
                    {
                        break;
                    }
                }
                image = image.Remove(0, image.Length - ind);
                movie.Image = image;
                db.Movies.Add(movie);
            }
            conn.Close();
            db.SaveChanges();
            //Добавление в фильмы колекцию героев
            for (int i = 1; i <= 20; i++)
            {
                conn.Open();
                zapros = "select heroe.id from heroe,heroe_movie where heroe_movie.heroe_id = heroe.id and heroe_movie.movie_id =" + i;
                command = new MySqlCommand(zapros, conn);
                reader = command.ExecuteReader();
                Movie movie = db.Movies.Find(i);
                while (reader.Read())
                {
                    foreach (var heroe in db.Heroes)
                    {
                        if (heroe.Id == Convert.ToInt32(reader[0]))
                        {
                            movie.Heroes.Add(heroe);
                        }
                    }
                }
                conn.Close();
                db.SaveChanges();
            }
            //Добавление в героя колекцию фильмов
            for (int i = 1; i <= 8; i++)
            {
                conn.Open();
                zapros = "select movie.id from movie,heroe_movie where heroe_movie.movie_id = movie.id and heroe_movie.heroe_id =" + i;
                command = new MySqlCommand(zapros, conn);
                reader = command.ExecuteReader();
                Heroe heroe = db.Heroes.Find(i);
                while (reader.Read())
                {
                    foreach (var movie in db.Movies)
                    {
                        if (movie.Id == Convert.ToInt32(reader[0]))
                        {
                            heroe.Movies.Add(movie);
                        }
                    }
                }
                conn.Close();
                db.SaveChanges();
            }         
            base.Seed(context);
        }

    }
}