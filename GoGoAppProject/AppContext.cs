using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoGoAppProject.Models;

namespace GoGoAppProject
{
    public class AppContext : DbContext
    {
        public AppContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(local)\SQLEXPRESS;Database=GoGoAppProject; Trusted_Connection = True"); // connection string to your DB
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BadCategoryBuilding>().HasKey(u => new { u.badCategoryId, u.buildingId });
            modelBuilder.Entity<CompositionCategory>().HasKey(u => new { u.badCategoryId, u.newCategoryId });
            modelBuilder.Entity<EventBuilding>().HasKey(u => new { u.buildingId, u.phoneNumberCreator, u.dateCreated });
            modelBuilder.Entity<Event>().HasKey(u => new { u.phoneNumberCreator, u.dateCreated});
            modelBuilder.Entity<MessageModel>().HasKey(u => new { u.phoneNumberSender, u.phoneNumberReceiver });
            modelBuilder.Entity<MessageInfoModel>().HasKey(u => new { u.phoneNumberSender, u.phoneNumberReceiver });
            modelBuilder.Entity<ReviewModel>().HasKey(u => new { u.buildingId, u.phoneNumber });
            modelBuilder.Entity<UndergroundBuildingModel>().HasKey(u => new { u.buildingId, u.name });
            modelBuilder.Entity<NewCategoryBuilding>().HasKey(u => new { u.buildingId, u.newCategoryId });
            modelBuilder.Entity<UserRatingModel>().HasKey(u => new { u.buildingId, u.phoneNumber });
            modelBuilder.Entity<UserReviewModel>().HasKey(u => new { u.phoneNumberSender, u.phoneNumberReceiver });
            modelBuilder.Entity<UserToUserModel>().HasKey(u => new { u.phoneNumberSender, u.phoneNumberReceiver });
            modelBuilder.Entity<BuildingImage>().HasKey(u => new { u.buildingId});
            modelBuilder.Entity<BuildingInfoModel>().HasKey(u => new { u.buildingId});
            modelBuilder.Entity<BuildingType>().HasKey(u => new { u.buildingId});
            modelBuilder.Entity<NewCategoryInfo>().HasKey(u => new { u.newCategoryId });
            modelBuilder.Entity<EventInfo>().HasKey(u => new { u.dateCreated, u.phoneNumberCreator});
            modelBuilder.Entity<UserInfoModel>().HasKey(u => new { u.phoneNumber});
            modelBuilder.Entity<Underground>().HasKey(x => new { x.name});
            modelBuilder.Entity<BadCategory>().HasKey(u => new { u.badCategoryId});
            modelBuilder.Entity<Building>().HasKey(u => new { u.buildingId });
            modelBuilder.Entity<NewCategory>().HasKey(u => new { u.newCategoryId });
            modelBuilder.Entity<User>().HasKey(u => new { u.phoneNumber });
            modelBuilder.Entity<FavoritePlace>().HasKey(u => new { u.phoneNumber, u.buildingId});
            modelBuilder.Entity<UserInEvent>().HasKey(u => new { u.dateCreated, u.phoneNumber, u.phoneNumberCreator});
            modelBuilder.Entity<UserReviewPoint>().HasKey(u => new { u.phoneNumber});





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
        public DbSet<FavoritePlace> FavoritePlaces { get; set; }
        public DbSet<UserInEvent> UsersInEvents { get; set; }
        public DbSet<UserReviewPoint> UserReviewPoints { get; set; }
    } 
}
