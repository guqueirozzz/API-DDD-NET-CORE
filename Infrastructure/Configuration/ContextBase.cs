using Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options) 
        {
            
        }    
        public DbSet<Message> Message { get; set; } 
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public string ObterStringConexao()
        {
            return "Server=DESKTOP-1RR2K2L\\MSSQLSERVER01;Database=API_DDD_2023;Trusted_Connection=True; trustServerCertificate=true";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ObterStringConexao());

            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //buildando a tabela de usuarios e adicionando a chave primaria "Id"
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);
            base.OnModelCreating(builder);
        }
    }
}
