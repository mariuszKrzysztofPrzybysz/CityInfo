﻿namespace CityInfo.Api.DbContexts
{
    using CityInfo.Api.Entities;
    using Microsoft.EntityFrameworkCore;

    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; }

        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            : base(options)
        {
            Database.Migrate();
        }
    }
}