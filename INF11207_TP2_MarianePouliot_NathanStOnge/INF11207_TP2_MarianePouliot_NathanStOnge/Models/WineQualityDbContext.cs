using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INF11207_TP2_MarianePouliot_NathanStOnge.Models
{
    internal class WineQualityDbContext : DbContext
    {
        public DbSet<Models.User>? Users { get; set; }
        public DbSet<Models.Prediction>? Predictions { get; set; }
        public DbSet<Models.Setting>? Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            string connexionString = "server=localhost;"+
                                     "port=3306;" +
                                     "database=wine_quality_db;"+
                                     "user=nathanstonge;"+
                                     "password=nathanstonge;";
            dbContextOptionsBuilder.UseMySql(connexionString, ServerVersion.AutoDetect(connexionString));
        }
    }
}
