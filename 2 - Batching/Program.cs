using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Batching
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new LivrosContext())
            {
                db.Database.EnsureDeleted(); // deleta a database caso ela exista
                db.Database.EnsureCreated(); // caso exista alguma linha na tabela ele deleta

                if (db.Livros.Any())
                {
                    db.Database.ExecuteSqlRawAsync("DELETE FROM dbo.Livros");
                }

                db.Livros.Add(new Livro { Titulo = "Domain-Driven Design: Tackling Complexity in the Heart of Software", Autor = "Eric Evans", AnoPublicacao = 2003 });
                db.Livros.Add(new Livro { Titulo = "Agile Principles, Patterns, and Practices in C#", Autor = "Robert C. Martin", AnoPublicacao = 2006 });
                db.Livros.Add(new Livro { Titulo = "Clean Code: A Handbook of Agile Software Craftsmanship", Autor = "Robert C. Martin", AnoPublicacao = 2008 });
                db.Livros.Add(new Livro { Titulo = "Implementing Domain-Driven Design", Autor = "Vaughn Vernon", AnoPublicacao = 2013 });
                db.Livros.Add(new Livro { Titulo = "Patterns, Principles, and Practices of Domain-Driven Design", Autor = "Scott Millet", AnoPublicacao = 2015 });
                db.Livros.Add(new Livro { Titulo = "Refactoring: Improving the Design of Existing Code ", Autor = "Martin Fowler", AnoPublicacao = 2012 });

                db.SaveChanges(); // envia as informações para o banco
            }
        }

        public class LivrosContext : DbContext
        {
            public DbSet<Livro> Livros { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=localhost;Database=EFCoreDemo;User Id=sa;Password=yourStrong(!)Password;",
                    options => options.MaxBatchSize(2)); // Define o maximo de operaçoes que vai chegar no banco por chamada
            }
        }

        public class Livro
        {
            public int LivroId { get; set; }
            public string Titulo { get; set; }
            public string Autor { get; set; }
            public int AnoPublicacao { get; set; }
        }
    }
}
