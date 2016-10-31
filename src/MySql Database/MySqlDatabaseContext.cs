using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain_Model;
using Microsoft.EntityFrameworkCore;

namespace MySql_Database
{
    public class MySqlDatabaseContext: DbContext
    {
        public virtual DbSet<Movie> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=movies;uid=root;pwd=Nepal12");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().ToTable("movie");
            modelBuilder.Entity<Movie>().Property(p => p.Id).HasColumnName("movieId");

           
            base.OnModelCreating(modelBuilder);
        }
    }
}
