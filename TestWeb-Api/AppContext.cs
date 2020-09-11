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
            optionsBuilder.UseSqlServer(@"Server=wpl24.hosting.reg.ru;Database=u0933163_GOGOAPPPROJECT;User ID=u0933163_dane4ka;Password=HmPxxHSvj38jNqP"); // connection string to your DB
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friend>().HasKey(u => new { u.Id_User_Sender, u.Id_User_Receiver });
            modelBuilder.Entity<ConnectionEventsCategoriesModel>().HasKey(u => new { u.Id_Event, u.Id_Category });
            modelBuilder.Entity<ConnectionInstitutionsCategoriesModel>().HasKey(u => new { u.Id_Institution, u.Id_Category });
            modelBuilder.Entity<MarksInstitutionsModel>().HasKey(u => new { u.Id_User, u.Id_Institution});
            modelBuilder.Entity<User>().HasKey(u => new { u.Id_User });
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<MessageModel> Message { get; set; }
        public DbSet<EventsModel> Events { get; set; }
        public DbSet<InstitutionsModel> Institutions { get; set; }
        public DbSet<CategoriesModel> Categories { get; set; }
        public DbSet<ConnectionEventsCategoriesModel> Connection_Events_Categories { get; set; }
        public DbSet<ConnectionInstitutionsCategoriesModel> Connection_Institutions_Categories { get; set; }
        public DbSet<MarksInstitutionsModel> Marks_Institutions { get; set; }
    } 
}
