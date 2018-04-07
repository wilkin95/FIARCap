namespace FIARCap.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FIARCap.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FIARCap.Models.ApplicationDbContext context)
        {
            context.Books.AddOrUpdate(b => b.Title,
                new Models.Book()
                {
                    ISBN = "1234567890",
                    Title = "The Story About Ping",
                    Author = "Marjorie Flack",
                    Illustrator = "Kurt Wiese",
                    Copyright = 1933,
                    Category = Models.BookCategory.Classic,
                    Summary = "A duck learns there are worse things than taking the consequences for one’s mistakes.",
                    Topics = "Discernment, China, Ducks, Buoyancy, Health and Safety, Reflection of Light"
                },
            new Models.Book()
            {
                ISBN = "2345678901",
                Title = "Lentil",
                Author = "Robert McCloskey",
                Illustrator = "Robert McCloskey",
                Copyright = 1940,
                Category = Models.BookCategory.Classic,
                Summary = "A boy discovers that he has a special talent and using it makes a difference.",
                Topics = "Ohio, Human Relationships, War Monuments, Patriotism, Harmonica, Architecture, Taste Buds, Sound and Acoustics"
            },
            new Models.Book()
            {
                ISBN = "3456789012",
                Title = "A Pair of Red Clogs",
                Author = "Masako Matsuno",
                Illustrator = "Kazue Mizumura",
                Copyright = 1960,
                Category = Models.BookCategory.Fiction,
                Summary = "Mako receives a new pair of shoes and learns the importance of honesty.",
                Topics = "Japan, Honesty, Weather, Architecture, Islands"
            }
            );
        }
    }
}
