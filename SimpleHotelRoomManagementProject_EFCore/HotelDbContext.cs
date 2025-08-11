using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleHotelRoomManagementProject_EFCore.Models;


namespace SimpleHotelRoomManagementProject_EFCore
{
    // HotelDbContext: Represents the session with the database for the Hotel Room Management System
    // Inherits from Entity Framework Core's DbContext to manage database operations
    public class HotelDbContext : DbContext
    {

        // Configures the connection string for the SQL Server database
        // This method is called by EF Core to set up the database connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-2EL6LAV ;Initial Catalog=HotelRoomManagementDB;Integrated Security=True;TrustServerCertificate=True"); // Specifies the SQL Server database connection with server name, database name, and security settings
        }


        public DbSet<Room> Rooms { get; set; }   // Represents the Rooms table in the database
        public DbSet<Booking> booking { get; set; } //Represents the Booking table in the database
        public DbSet<Guest> Guests { get; set; } //Represents the Guests table in the database
        public DbSet<Review> Reviews { get; set; }


    }
}
