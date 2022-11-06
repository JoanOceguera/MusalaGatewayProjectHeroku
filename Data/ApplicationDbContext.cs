using Microsoft.EntityFrameworkCore;
using MusalaGatewayProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusalaGatewayProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Gateway> Gateways { get; set; }
        public DbSet<PeripheralDevice> Devices { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<string> Ids = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                Ids.Add(Guid.NewGuid().ToString().ToUpper());
            }
            Random gatewayGenerator = new Random();
            modelBuilder.Entity<Gateway>().HasData(
                new Gateway
                {
                    GatewayId = Ids[0],
                    Address = "127.0.0.1",
                    Name = "TestGateway#01",
                },
                new Gateway
                {
                    GatewayId = Ids[1],
                    Address = "192.168.172.5",
                    Name = "TestGateway#02"
                },
                new Gateway
                {
                    GatewayId = Ids[2],
                    Address = "255.255.255.128",
                    Name = "TestGateway#03"
                },
                new Gateway
                {
                    GatewayId = Ids[3],
                    Address = "129.145.175.145",
                    Name = "TestGateway#04"
                },
                new Gateway
                {
                    GatewayId = Ids[4],
                    Address = "197.245.127.247",
                    Name = "TestGateway#05"
                });

            modelBuilder.Entity<PeripheralDevice>().HasData(
                new PeripheralDevice
                {
                    UID = 1,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Offline,
                    Vendor = "Asus",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                },
                new PeripheralDevice
                {
                    UID = 2,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Online,
                    Vendor = "Amazon",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                },
                new PeripheralDevice
                {
                    UID = 3,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Online,
                    Vendor = "Intel",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                },
                new PeripheralDevice
                {
                    UID = 4,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Online,
                    Vendor = "Intel",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                },
                new PeripheralDevice
                {
                    UID = 5,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Offline,
                    Vendor = "Cisco",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                },
                new PeripheralDevice
                {
                    UID = 6,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Offline,
                    Vendor = "Amazon",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                },
                new PeripheralDevice
                {
                    UID = 7,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Offline,
                    Vendor = "Nvidia",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                },
                new PeripheralDevice
                {
                    UID = 8,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Offline,
                    Vendor = "Microsoft",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                },
                new PeripheralDevice
                {
                    UID = 9,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Offline,
                    Vendor = "Apple",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                },
                new PeripheralDevice
                {
                    UID = 10,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Online,
                    Vendor = "Apple",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                },
                new PeripheralDevice
                {
                    UID = 11,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Offline,
                    Vendor = "Panasonic",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                },
                new PeripheralDevice
                {
                    UID = 12,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Offline,
                    Vendor = "Sony",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                },
                new PeripheralDevice
                {
                    UID = 13,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Offline,
                    Vendor = "AOC",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                },
                new PeripheralDevice
                {
                    UID = 14,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Offline,
                    Vendor = "Cooler Master",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                },
                new PeripheralDevice
                {
                    UID = 15,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Online,
                    Vendor = "AMD",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                },
                new PeripheralDevice
                {
                    UID = 16,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Online,
                    Vendor = "Intel",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                },
                new PeripheralDevice
                {
                    UID = 17,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Offline,
                    Vendor = "Google",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                },
                new PeripheralDevice
                {
                    UID = 18,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Offline,
                    Vendor = "Nvidia",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                },
                new PeripheralDevice
                {
                    UID = 19,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Offline,
                    Vendor = "Oracle",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                },
                new PeripheralDevice
                {
                    UID = 20,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Offline,
                    Vendor = "AMD",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                },
                new PeripheralDevice
                {
                    UID = 21,
                    DateCreated = DateTime.Now,
                    StatusOfDevice = Status.Online,
                    Vendor = "Nvidia",
                    GatewayId = Ids[gatewayGenerator.Next(0, Ids.Count)]
                });

            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                modelBuilder.Entity<User>().HasData(

                new User
                {
                    Id = 1,
                    UserName = "Admin",
                    PasswordSalt = hmac.Key,
                    PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Admin"))
                });
            }
        }
    }
}
