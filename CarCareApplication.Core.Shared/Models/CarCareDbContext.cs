using Microsoft.EntityFrameworkCore;
using System;

namespace CarCareApplication.Core.Shared.Models
{
    public class CarCareApplicationDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Hour> Hours { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<HourOfWork> HourOfWorks { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Revenue> Revenues { get; set; }
        public DbSet<ExtraPriceSetting> ExtraPriceSettings { get; set; }

        public CarCareApplicationDbContext()
        {
        }
        public CarCareApplicationDbContext(DbContextOptions<CarCareApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            //optionsBuilder.UseMySQL(new MySqlConnectionStringBuilder
            //{
            //    Server = "root@localhost:3306",
            //    Port = 3306,
            //    UserID = "root",
            //    Password = "Ahmed!994"
            //}.ConnectionString);
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasData(new Role { Id = 1, Name = "Admin", IsEnabled = true },
                         new Role { Id = 2, Name = "User", IsEnabled = true },
                         new Role { Id = 3, Name = "Driver", IsEnabled = true });


            modelBuilder.Entity<User>().HasData(new User { Id = 1, FirstName = "Mohamed", SecondName = "Abuelnour", RoleId = 1, Password = "Abuelnour!990", PhoneNumber = "01115089773", IsEnabled = true });

            modelBuilder.Entity<Day>()
                .HasData(new Day { Id = 1, NameAR = "السبت", NameEN = "Saturday", IsEnabled = true, DayOfWeek = DayOfWeek.Saturday },
                         new Day { Id = 2, NameAR = "الاحد", NameEN = "Sunday", IsEnabled = true, DayOfWeek = DayOfWeek.Sunday },
                         new Day { Id = 3, NameAR = "الاثنين", NameEN = "Monday", IsEnabled = true, DayOfWeek = DayOfWeek.Monday },
                         new Day { Id = 4, NameAR = "الثلاثاء", NameEN = "Tuesday", IsEnabled = true, DayOfWeek = DayOfWeek.Tuesday },
                         new Day { Id = 5, NameAR = "الاربعاء", NameEN = "Wednesday", IsEnabled = true, DayOfWeek = DayOfWeek.Wednesday },
                         new Day { Id = 6, NameAR = "الخميس", NameEN = "Thursday", IsEnabled = true, DayOfWeek = DayOfWeek.Thursday },
                         new Day { Id = 7, NameAR = "الجمعة", NameEN = "Friday", IsEnabled = true, DayOfWeek = DayOfWeek.Friday });

            modelBuilder.Entity<Hour>()
                .HasData(
                new Hour { Id = 1, NameEN = "6  AM - 8  AM", NameAR = "6  AM - 8  AM", IsEnabled = true, Start = new TimeSpan(6, 0, 0), End = new TimeSpan(8, 0, 0) },
                new Hour { Id = 2, NameEN = "8  AM - 10 AM", NameAR = "8 ص - 10 ص", IsEnabled = true, Start = new TimeSpan(8, 0, 0), End = new TimeSpan(10, 0, 0) },
                new Hour { Id = 3, NameEN = "10 AM - 12 AM", NameAR = "10 ص - 12 ص", IsEnabled = true, Start = new TimeSpan(10, 0, 0), End = new TimeSpan(12, 0, 0) },
                new Hour { Id = 4, NameEN = "12 PM - 2  PM", NameAR = "12 م - 2 م", IsEnabled = true, Start = new TimeSpan(12, 0, 0), End = new TimeSpan(14, 0, 0) },
                new Hour { Id = 5, NameEN = "2  PM - 4  PM", NameAR = "2 م - 4 م", IsEnabled = true, Start = new TimeSpan(14, 0, 0), End = new TimeSpan(16, 0, 0) },
                new Hour { Id = 6, NameEN = "4  PM - 6  PM", NameAR = "4 م - 6 م", IsEnabled = true, Start = new TimeSpan(16, 0, 0), End = new TimeSpan(18, 0, 0) },
                new Hour { Id = 7, NameEN = "6  PM - 8  PM", NameAR = "6 م - 8 م", IsEnabled = true, Start = new TimeSpan(18, 0, 0), End = new TimeSpan(20, 0, 0) },
                new Hour { Id = 8, NameEN = "8  PM - 10 PM", NameAR = "8 م - 10 م", IsEnabled = true, Start = new TimeSpan(20, 0, 0), End = new TimeSpan(22, 0, 0) });

            modelBuilder.Entity<HourOfWork>()
                .HasData(
                    new HourOfWork { Id = 1, DayId = 1, HourId = 1, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 2, DayId = 1, HourId = 2, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 3, DayId = 1, HourId = 3, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 4, DayId = 1, HourId = 4, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 5, DayId = 1, HourId = 5, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 6, DayId = 1, HourId = 6, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 7, DayId = 1, HourId = 7, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 8, DayId = 1, HourId = 8, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 9, DayId = 2, HourId = 1, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 10, DayId = 2, HourId = 2, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 11, DayId = 2, HourId = 3, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 12, DayId = 2, HourId = 4, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 13, DayId = 2, HourId = 5, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 14, DayId = 2, HourId = 6, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 15, DayId = 2, HourId = 7, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 16, DayId = 2, HourId = 8, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 17, DayId = 3, HourId = 1, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 18, DayId = 3, HourId = 2, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 19, DayId = 3, HourId = 3, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 20, DayId = 3, HourId = 4, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 21, DayId = 3, HourId = 5, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 22, DayId = 3, HourId = 6, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 23, DayId = 3, HourId = 7, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 24, DayId = 3, HourId = 8, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 25, DayId = 4, HourId = 1, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 26, DayId = 4, HourId = 2, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 27, DayId = 4, HourId = 3, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 28, DayId = 4, HourId = 4, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 29, DayId = 4, HourId = 5, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 30, DayId = 4, HourId = 6, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 31, DayId = 4, HourId = 7, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 32, DayId = 4, HourId = 8, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 33, DayId = 5, HourId = 1, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 34, DayId = 5, HourId = 2, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 35, DayId = 5, HourId = 3, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 36, DayId = 5, HourId = 4, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 37, DayId = 5, HourId = 5, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 38, DayId = 5, HourId = 6, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 39, DayId = 5, HourId = 7, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 40, DayId = 5, HourId = 8, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 41, DayId = 6, HourId = 1, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 42, DayId = 6, HourId = 2, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 43, DayId = 6, HourId = 3, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 44, DayId = 6, HourId = 4, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 45, DayId = 6, HourId = 5, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 46, DayId = 6, HourId = 6, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 47, DayId = 6, HourId = 7, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 48, DayId = 6, HourId = 8, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 49, DayId = 7, HourId = 1, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 50, DayId = 7, HourId = 2, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 51, DayId = 7, HourId = 3, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 52, DayId = 7, HourId = 4, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 53, DayId = 7, HourId = 5, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 54, DayId = 7, HourId = 6, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 55, DayId = 7, HourId = 7, AvailableMinutes = 120, IsEnabled = true },
                    new HourOfWork { Id = 56, DayId = 7, HourId = 8, AvailableMinutes = 120, IsEnabled = true });
            base.OnModelCreating(modelBuilder);
        }


    }
}
