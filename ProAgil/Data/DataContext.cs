using Microsoft.EntityFrameworkCore;
using ProAgil.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProAgil.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=localhost\\SQLEXPRESS;initial catalog=PRO_AGIL;persist security info=True;user id=sa;password=root;MultipleActiveResultSets=True;App=EntityFramework");
        }

        public DbSet<Evento> Eventos { get; set; }
    }
}
