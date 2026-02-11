using Lumiere.Models;
using Microsoft.EntityFrameworkCore;

namespace Lumiere.API.Database
{
    public class LumiereContext : DbContext
    {
        public LumiereContext(DbContextOptions<LumiereContext> options) : base(options)
        {

        }
        #region Conexão sem distinção de ambientes de Execução
        /*
         * Conexão sem distinção de ambientes de Execução
         */
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Lumiere; Integrated Security = True;");
        }
        */
        #endregion

        public DbSet<Assento> Assentos { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<FormatoSessao> FormatosSessao { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Ingresso> Ingressos { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }
        public DbSet<TipoIngresso> TiposIngresso { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Assento>()
                .HasOne(a => a.Sala)
                .WithMany(s => s.Assentos)
                .HasForeignKey(a => a.SalaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Sessao>()
                .HasOne(s => s.Sala)
                .WithMany(sala => sala.Sessoes)
                .HasForeignKey(s => s.SalaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ingresso>()
                .HasOne(i => i.Sessao)
                .WithMany(s => s.Ingressos)
                .HasForeignKey(i => i.SessaoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ingresso>()
                .HasOne(i => i.Assento)
                .WithMany(a => a.Ingressos)
                .HasForeignKey(i => i.AssentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===== GENEROS (fixo) =====
            modelBuilder.Entity<Genero>().HasData(
                new Genero { Id = 1, Nome = "Ação" },
                new Genero { Id = 2, Nome = "Comédia" },
                new Genero { Id = 3, Nome = "Drama" },
                new Genero { Id = 4, Nome = "Terror" },
                new Genero { Id = 5, Nome = "Ficção" },
                new Genero { Id = 6, Nome = "Animação" }
            );

            // ===== TIPOS INGRESSO (fixo) =====
            modelBuilder.Entity<TipoIngresso>().HasData(
                new TipoIngresso { Id = 1, Nome = "Inteira",   DescontoPercentual = 0.00m },
                new TipoIngresso { Id = 2, Nome = "Meia",      DescontoPercentual = 50.00m },
                new TipoIngresso { Id = 3, Nome = "Estudante", DescontoPercentual = 50.00m },
                new TipoIngresso { Id = 4, Nome = "Idoso",     DescontoPercentual = 50.00m },
                new TipoIngresso { Id = 5, Nome = "Criança",   DescontoPercentual = 30.00m }
            );

            // ===== FORMATOS SESSAO (fixo) =====
            modelBuilder.Entity<FormatoSessao>().HasData(
                new FormatoSessao { Id = 1, Nome = "2D" },
                new FormatoSessao { Id = 2, Nome = "3D" },
                new FormatoSessao { Id = 3, Nome = "IMAX" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
