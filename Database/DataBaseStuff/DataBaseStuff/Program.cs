using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseStuff
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CatalogContainer())
            {
                var artists = from artist in context.Artists select artist;
                var song = new Song()
                {
                    Title = "Eye of the Tiger",
                    Duration = 1000,
                    Artist = artists.First()
                };

                context.Songs.Add(song);

                context.Artists.First().Country = "USA";

                foreach (var artist in context.Artists)
                {
                    if (artist.Name == "Prince")
                    {
                        artist.Name = "David Bowie";
                    }
                }

                context.SaveChanges();

                var query = from artist in context.Artists select artist.Name;

                foreach (var result in query)
                {
                    Console.WriteLine(result);
                }

                query = from artist in context.Songs select song.Title;
                foreach (var result in query)
                {
                    Console.WriteLine(result);
                }
                Console.ReadKey();
            }
        }
    }
}
