using Clean.UpIndia.Data;
using Clean.UpIndia.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanUpIndia.xTest
{
    public static class DbContextMocker
    {
        public static ApplicationDbContext GetApplicationDbContext(string dbName)
        {
           
            var serviceProvider = new ServiceCollection()
                                  .AddEntityFrameworkInMemoryDatabase()
                                  .BuildServiceProvider();

            // Create a new options instance telling the context
           
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                           .UseInMemoryDatabase(dbName)
                           .UseInternalServiceProvider(serviceProvider)
                           .Options;

            // Create the instance of the DbContext
            var dbContext = new ApplicationDbContext(options);

            // Add entities to the InMemory Database to seed the test data
            dbContext.SeedData();

            return dbContext;
        }

 
        internal static readonly Locality[] TestData_Localities
            = {
                new Locality
                {
                    LocalityId = 1,
                    LocalityName = "First One"
                },
                new Locality
                {
                    LocalityId = 2,
                    LocalityName = "Second one"
                },
                new Locality
                {
                    LocalityId = 3,
                    LocalityName = "Third One"
                },
            };


        /// <summary>
        ///     An extension Method for the DbContext object to Seed the Test Data.
        /// </summary>
        /// <param name="context">Application Db Context object.</param>
        private static void SeedData(this ApplicationDbContext context)
        {
            context.Localities.AddRange(TestData_Localities);

            // Commit the Changes to the database
            context.SaveChanges();
        }
    }
}
