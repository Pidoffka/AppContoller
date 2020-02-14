using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWeb_Api.Models;

namespace TestWeb_Api
{
    public class AppContext : DbContext
    {
        public AppContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=wpl24.hosting.reg.ru;Database=u0933163_dan1a;User ID=u0933163_dan1a;Password=EfWe4QD7djDuRqh"); // connection string to your DB
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Event_Institution> Event_Institutions { get; set; }
        public DbSet<Category> Categories { get; set; }

    } 
}
