using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FastFood.Data
{
    public class FastFoodContextDesignTimeFactory : IDesignTimeDbContextFactory<FastFoodContext>
    {
        public FastFoodContext CreateDbContext(string[] args)
        {
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .Build();

            var builder = new DbContextOptionsBuilder<FastFoodContext>();

            //var connectionString = configuration.GetConnectionString("DefaultConnections");

            builder.UseSqlServer("Server=.;Database=FastFood;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new FastFoodContext(builder.Options);
        }
    }
}
