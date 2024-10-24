using CursoIdiomasAPI.Entitites;
using Microsoft.EntityFrameworkCore;

namespace CursoIdiomasAPI.Data
{
    public class IdiomasDB : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }





        public IdiomasDB(DbContextOptions<IdiomasDB> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().HasIndex(x => x.CPF).IsUnique(); // Configurando que o cpf seja unico ao ser cadastrado


            




            base.OnModelCreating(modelBuilder);
        }




    }
}
