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
            modelBuilder.Entity<BadCategoryBuilding>().HasKey(u => new { u.badCategoryId, u.buildingId });
            modelBuilder.Entity<CompositionCategory>().HasKey(u => new { u.badCategoryId, u.newCategoryId });
            modelBuilder.Entity<EventBuilding>().HasKey(u => new { u.buildingId, u.phoneNumberCreator, u.dateCreated });
            modelBuilder.Entity<Event>().HasKey(u => new { u.phoneNumberCreator, u.dateCreated});
            modelBuilder.Entity<EventInfo>().HasKey(u => new { u.phoneNumberCreator, u.dateCreated });
            modelBuilder.Entity<MessageModel>().HasKey(u => new { u.phoneNumberSender, u.phoneNumberReceiver });
            modelBuilder.Entity<MessageInfoModel>().HasKey(u => new { u.phoneNumberSender, u.phoneNumberReceiver});
            modelBuilder.Entity<ReviewModel>().HasKey(u => new { u.buildingId, u.phoneNumber});
            modelBuilder.Entity<UndergroundBuildingModel>().HasKey(u => new { u.buildingId, u.name});
            modelBuilder.Entity<NewCategoryBuilding>().HasKey(u => new { u.buildingId, u.newCategoryId});
            modelBuilder.Entity<UserRatingModel>().HasKey(u => new { u.buildingId, u.phoneNumber});
            modelBuilder.Entity<UserReviewModel>().HasKey(u => new { u.phoneNumberSender, u.phoneNumberReceiver});
            modelBuilder.Entity<UserToUserModel>().HasKey(u => new { u.phoneNumberSender, u.phoneNumberReceiver});
        }
        public DbSet<BadCategory> BadCategories { get; set; }
        public DbSet<BadCategoryBuilding> BadCategoriesBuildings { get; set; }
        public DbSet<BuildingImage> BuildingImages { get; set; }
        public DbSet<BuildingInfoModel> BuildingInfo { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<BuildingType> BuildingTypes { get; set; }
        public DbSet<CompositionCategory> CompositionCategories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventBuilding> EventsBuilding { get; set; }
        public DbSet<EventInfo> EventsInfo { get; set; }
        public DbSet<MessageModel> Message { get; set; }
        public DbSet<MessageInfoModel> MessageInfo { get; set; }
        public DbSet<NewCategory> NewCategories { get; set; }
        public DbSet<NewCategoryBuilding> NewCategoriesBuildings { get; set; }
        public DbSet<NewCategoryInfo> NewCategoriesInfo { get; set; }
        public DbSet<ReviewModel> Review { get; set; }
        public DbSet<Underground> Undergrounds { get; set; }
        public DbSet<UndergroundBuildingModel> UndergroundBuilding { get; set; }
        public DbSet<UserInfoModel> UserInfo { get; set; }
        public DbSet<UserRatingModel> UserRating { get; set; }
        public DbSet<UserReviewModel> UserReview { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToUserModel> UserToUser { get; set; }
    } 
}
