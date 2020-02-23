using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
        public DbSet<Team_to_Event> Teams_to_Events { get; set; }
        public DbSet<Connection_Event_Category> Connections_Event_Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connection_Event_Category>().HasKey(u => new { u.Id_Event, u.Id_Categories });
            modelBuilder.Entity<Friend>().HasKey(u => new { u.Id_User_Sender, u.Id_User_Receiver });
            modelBuilder.Entity<MessageModel>().HasKey(u => new { u.Id_User_Sender, u.Id_User_Receiver });
        }
        public DbSet<Criterions_Gender> Criterions_Genders { get; set; }
        public DbSet<Criterions_Age> Criterions_Ages { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<MessageModel> Message { get; set; }

    } 
}
