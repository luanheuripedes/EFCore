using InMemory.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

//Classe de configuração do EF
namespace InMemory.DAL
{
    public class LivroContext : DbContext
    {
        public LivroContext(DbContextOptions options) : base(options) { }

        public LivroContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("InMemoryProvider"); //O banco que vai usar é o ImMemory
        }


        public DbSet<Livro> Livros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Livro>().HasAlternateKey(c => c.Titulo);
        }

    }
}