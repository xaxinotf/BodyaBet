using BodyaBet.Contexts;
using BodyaBet.Controllers;
using BodyaBet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BodyaBet
{
    public class CountriesControllerTests
    {
        [Fact]
        public async Task GetCountries_ReturnsListOfCountries()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VolleyballContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new VolleyballContext(options))
            {
                var controller = new CountriesController(context);

                // Act
                var result = await controller.GetCountries();

                // Assert
                var actionResult = Assert.IsType<ActionResult<IEnumerable<Country>>>(result);
                var countries = Assert.IsAssignableFrom<IEnumerable<Country>>(actionResult.Value);
                Assert.NotEmpty(countries);
            }
        }

        [Fact]
        public async Task GetCountry_WithValidId_ReturnsCountry()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VolleyballContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new VolleyballContext(options))
            {
                // Add sample data to the in-memory database
                var country = new Country { Id = 1, Name = "Test Country" };
                context.Countries.Add(country);
                await context.SaveChangesAsync();

                var controller = new CountriesController(context);

                // Act
                var result = await controller.GetCountry(1);

                // Assert
                var actionResult = Assert.IsType<ActionResult<Country>>(result);
                var returnedCountry = Assert.IsAssignableFrom<Country>(actionResult.Value);
                Assert.Equal(country.Id, returnedCountry.Id);
                Assert.Equal(country.Name, returnedCountry.Name);
            }
        }

        [Fact]
        public async Task GetCountry_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VolleyballContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new VolleyballContext(options))
            {
                var controller = new CountriesController(context);

                // Act
                var result = await controller.GetCountry(1);

                // Assert
                Assert.IsType<NotFoundResult>(result.Result);
            }
        }

        [Fact]
        public async Task PutCountry_WithValidId_ReturnsNoContent()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VolleyballContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new VolleyballContext(options))
            {
                // Add sample data to the in-memory database
                var country = new Country { Id = 1, Name = "Test Country" };
                context.Countries.Add(country);
                await context.SaveChangesAsync();

                var controller = new CountriesController(context);
                var updatedCountry = new Country { Id = 1, Name = "Updated Country" };

                // Act
                var result = await controller.PutCountry(1, updatedCountry);

                // Assert
                Assert.IsType<NoContentResult>(result);
                Assert.Equal(updatedCountry.Name, context.Countries.Single().Name);
            }
        }

        [Fact]
        public async Task PutCountry_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VolleyballContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new VolleyballContext(options))
            {
                var controller = new CountriesController(context);
                var country = new Country { Id = 1, Name = "Test Country" };

                // Act
                var result = await controller.PutCountry(2, country);

                // Assert
                Assert.IsType<BadRequestResult>(result);
            }
        }

        [Fact]
        public async Task PostCountry_WithValidData_ReturnsCreatedAtAction()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VolleyballContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new VolleyballContext(options))
            {
                var controller = new CountriesController(context);
                var country = new Country { Id = 1, Name = "Test Country" };

                // Act
                var result = await controller.PostCountry(country);

                // Assert
                var actionResult = Assert.IsType<ActionResult<Country>>(result);
                var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
                Assert.Equal(country.Id, createdAtActionResult.RouteValues["id"]);
                Assert.Equal(country, createdAtActionResult.Value);
            }
        }

        [Fact]
        public async Task PostCountry_WithEmptyName_ReturnsNoContent()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VolleyballContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new VolleyballContext(options))
            {
                var controller = new CountriesController(context);
                var country = new Country { Id = 1, Name = "" };

                // Act
                var result = await controller.PostCountry(country);

                // Assert
                Assert.IsType<NoContentResult>(result.Result);
            }
        }

        [Fact]
        public async Task DeleteCountry_WithValidId_ReturnsNoContent()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VolleyballContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new VolleyballContext(options))
            {
                // Add sample data to the in-memory database
                var country = new Country { Id = 1, Name = "Test Country" };
                context.Countries.Add(country);
                await context.SaveChangesAsync();

                var controller = new CountriesController(context);

                // Act
                var result = await controller.DeleteCountry(1);

                // Assert
                Assert.IsType<NoContentResult>(result);
                Assert.Empty(context.Countries);
            }
        }

        [Fact]
        public async Task DeleteCountry_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VolleyballContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new VolleyballContext(options))
            {
                var controller = new CountriesController(context);

                // Act
                var result = await controller.DeleteCountry(1);

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }
        }
    }
}
