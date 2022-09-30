using Clean.UpIndia.Controllers;
using Clean.UpIndia.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CleanUpIndia.xTest
{
    public partial class LocalitiesApiTests
    {
        [Fact]
        public void GetLocalities_OkResult()
        {
            // ARRANGE
            var dbName = nameof(LocalitiesApiTests.GetLocalities_OkResult);
            var logger = Mock.Of<ILogger<LocalitiesController>>();
            var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var controller = new LocalitiesController(dbContext);

            // ACT
           var actionResult = controller.GetLocalities().Result;

            // ASSERT
            // --- Check if the ActionResult is of the type OkObjectResult
            Assert.IsType<OkObjectResult>(actionResult);

            // --- Check if the HTTP Response Code is 200 "Ok"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            int actualStatusCode = (actionResult as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetLocalities_CheckCorrectResult()
        {
            // ARRANGE
            var dbName = nameof(LocalitiesApiTests.GetLocalities_OkResult);
            var logger = Mock.Of<ILogger<LocalitiesController>>();
            var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var apiController = new LocalitiesController(dbContext);

            // ACT
            IActionResult actionResult = apiController.GetLocalities().Result;

            // ASSERT: Check if the ActionResult is of the type OkObjectResult
            Assert.IsType<OkObjectResult>(actionResult);


            // ACT: Extract the OkResult result 
            var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;

            // ASSERT: if the OkResult contains the object of the Correct Type
            Assert.IsAssignableFrom<List<Locality>>(okResult.Value);


            //// ACT: Extract the Categories from the result of the action
            //var categoriesFromApi = okResult.Value.Should().BeAssignableTo<List<Locality>>().Subject;

            //// ASSERT: if the categories is NOT NULL
            //Assert.NotNull(categoriesFromApi);

            //// ASSERT: if the number of categories in the DbContext seed data
            ////         is the same as the number of categories returned in the API Result
            //Assert.Equal<int>(expected: DbContextMocker.TestData_Localities.Length,
            //                  actual: categoriesFromApi.Count);

            //// ASSERT: Test the data received from the API against the Seed Data
            //int ndx = 0;
            //foreach (Locality locality in DbContextMocker.TestData_Localities)
            //{
            //    // ASSERT: check if the Category ID is correct
            //    Assert.Equal<int>(expected: locality.LocalityId,
            //                      actual: localititesFromApi[ndx].Locality);

            //    // ASSERT: check if the Category Name is correct
            //    Assert.Equal(expected: locality.LocalityName,
            //                 actual: localitiesFromApi[ndx].LocalityName);

            //    _testOutputHelper.WriteLine($"Compared Row # {ndx} successfully");

            //    ndx++;          // now compare against the next element in the array
            //}
        }
    }
}
