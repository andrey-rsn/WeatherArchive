﻿using Microsoft.EntityFrameworkCore;
using WeatherArchive.Models;

namespace WeatherArchive.DBContext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<WeatherConditions> weatherConditions;
    }
}
