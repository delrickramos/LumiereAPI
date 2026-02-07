using Lumiere.Models;
using Microsoft.EntityFrameworkCore;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

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


    }
}
