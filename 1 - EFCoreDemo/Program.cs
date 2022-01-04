using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EFCoreDemo // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            using(var db = new LivroContexto()){

                //Faz com que eu crie o banco de dados sem migration
                //db.Database.EnsureCreated();

                //Migrations primeiro ver como modelou a tabela para depois transmitir e criar o banco previamente
                //Livro l1 = new Livro("Teste", "Marquinho", 1979);
                //Livro l2 = new Livro("Escolha", "Joaquim", 1999);

                //Adicionar os livros na propriedade no contexto do db
                //db.Livros.Add(new Livro {Titulo = "Teste", Autor = "Marquinho", AnoPublicacao = 1979});
                //db.Livros.Add(new Livro {Titulo = "Escolha", Autor = "Joaquim", AnoPublicacao = 1999});

                //Salva no banco os dados
                //db.SaveChanges();


                //Consulta dos dados que eu inseridos
                System.Console.WriteLine(  " ************** Resultados *********");
                //Traz tudo da tabela livro do banco
                db.Livros.ForEachAsync(x => Console.WriteLine($"Título: {x.Titulo} | Autor: {x.Autor}"));
            }




        }
    }

    //Contexto para conversar com o banco -- configuração do EF
    public class LivroContexto : DbContext{
        
        //faz com manipulemos os dados na nossa tabela --consultas - inserir dados
        public DbSet<Livro> Livros { get; set; }

        //configuração e modelagem dos dados que vao ser refletidos no banco
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder){
            optionBuilder
            .UseSqlServer(@"Server=localhost;Database=EFCoreDemo;User Id=sa;Password=yourStrong(!)Password;");
        }

        //Metodo de modelagem da opçoes do bd //Configurar e Modelar a tabela na hora de criar no banco
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Livro>().ToTable("Livro");

            //Definindo a chave primaria da tabela
            modelBuilder.Entity<Livro>().HasKey(p => p.LivroId);

            //Adiciona um tipo a coluna
            modelBuilder.Entity<Livro>().Property(p => p.Titulo).HasColumnType("varchar(50)");
        }


    }

    public class Livro{

        
        public int LivroId { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int AnoPublicacao { get; set; }

    }
}