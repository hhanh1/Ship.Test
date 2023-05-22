
using AutoMapper;
using EntityFrameworkCore.Testing.Moq.Helpers;
using Microsoft.EntityFrameworkCore;
using Ship.Bussiness.Entities;
using Ship.Bussiness.Models;
using Ship.Bussiness.Services;
using Ship.Common.Exceptions;
using Ship.Common.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ship.Bussiness.Test.Services
{
    public class ShipServiceTests
    {
        private TestContext mockTestContext;
        private IMapper mockMapper;

        public ShipServiceTests()
        {
            var options = new DbContextOptionsBuilder<TestContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContextToMock = new TestContext(options);
            this.mockTestContext = new MockedDbContextBuilder<TestContext>().UseDbContext(dbContextToMock).UseConstructorWithParameters(options).MockedDbContext;

            var myProfile = new Mapping();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            this.mockMapper = new Mapper(configuration);
        }

        private ShipService CreateService()
        {
            return new ShipService(
                this.mockTestContext,
                this.mockMapper);
        }

        [Fact]
        public async Task Add_ValidInput_ShouldSaveToDBContext()
        {
            // Arrange
            var service = this.CreateService();

            ShipViewModel model = new ShipViewModel() { Name = "Test1", Latitude = 77, Longitude = 55, Velocity = 50 };

            // Act
            await service.Add(
                model);

            // Assert
            Assert.True(mockTestContext.Ships?.First().Name == "Test1");
        }

        [Fact]
        public async Task Add_NullInput_ShouldThrowError()
        {
            // Arrange
            var service = this.CreateService();

            ShipViewModel model = null;

            // Act
            var exception = await Record.ExceptionAsync(async () =>
                await service.Add(model));
            // Assert
            Assert.IsType(typeof(NullReferenceException), exception);
        }

        [Fact]
        public async Task EstimationArrival_ValidInput_ShouldReturnValidType()
        {
            // Arrange
            var service = this.CreateService();

            var ShipId = Guid.NewGuid();
            var PortId = Guid.NewGuid();
            mockTestContext.Ships.Add(new Entities.Ship() { Name = "Test1", Latitude = 77, Longitude = 55, Velocity = 50, Id = ShipId });
            mockTestContext.Ports.Add(new Port() { Id = PortId, Name = "Test1", Latitude = 66, Longitude = 88 });
            mockTestContext.SaveChanges();

            EstimatedArrivalModel model = new EstimatedArrivalModel() { ShipId = ShipId, PortId = PortId };

            // Act
            var result = await service.EstimationArrival(
                model);

            // Assert
            Assert.IsType<string>(result);
        }

        [Fact]
        public async Task EstimationArrival_PortNotExist_ShouldThrowError()
        {
            // Arrange
            var service = this.CreateService();

            var ShipId = Guid.NewGuid();
            var PortId = Guid.NewGuid();
            mockTestContext.Ships.Add(new Entities.Ship() { Name = "Test1", Latitude = 77, Longitude = 55, Velocity = 50, Id = ShipId });
            mockTestContext.SaveChanges();

            EstimatedArrivalModel model = new EstimatedArrivalModel() { ShipId = ShipId, PortId = Guid.NewGuid() };

            // Act
            var exception = await Record.ExceptionAsync(async () => await service.EstimationArrival(model));
            // Assert
            Assert.IsType(typeof(NotFoundException), exception);
        }

        [Fact]
        public async Task EstimationArrival_ShipNotExist_ShouldThrowError()
        {
            // Arrange
            var service = this.CreateService();

            var ShipId = Guid.NewGuid();
            var PortId = Guid.NewGuid();

            EstimatedArrivalModel model = new EstimatedArrivalModel() { ShipId = ShipId, PortId = Guid.NewGuid() };

            // Act
            var exception = await Record.ExceptionAsync(async () => await service.EstimationArrival(
                           model));
            // Assert
            Assert.IsType(typeof(NotFoundException), exception);
        }

        [Fact]
        public async Task Search_ValidInput_ShouldReturnValidType()
        {
            // Arrange
            var service = this.CreateService();
            SearchShipViewModel model = new SearchShipViewModel() { Name = "Test1" };

            // Act
            var result = await service.Search(
                model);

            // Assert
            Assert.IsType<PageList<ShipReponses>>(result);
        }

        [Fact]
        public async Task Update_ValidInput_ShouldSaveChangesOnDBContext()
        {
            // Arrange
            var service = this.CreateService();
            var ShipId = Guid.NewGuid();
            var PortId = Guid.NewGuid();
            mockTestContext.Ships.Add(new Entities.Ship() { Name = "Test1", Latitude = 77, Longitude = 55, Velocity = 50, Id = ShipId });
            mockTestContext.Ports.Add(new Port() { Id = PortId, Name = "Test1", Latitude = 66, Longitude = 88 });
            mockTestContext.SaveChanges();
            ShipViewModel model = new ShipViewModel() { Name = "Test2", Latitude = 1000, Longitude = 1100, Velocity = 50 };

            await service.Add(model);

            // Act
            await service.Update(
                ShipId,
                model);

            // Assert
            Assert.NotNull(mockTestContext.Ships.Where(x => x.Name == "Test2").First());
        }

        [Fact]
        public async Task UpdateVelocity_ValidInput_ShouldSaveChangesOnDBContext()
        {
            // Arrange
            var service = this.CreateService();
            var ShipId = Guid.NewGuid();
            var PortId = Guid.NewGuid();
            mockTestContext.Ships.Add(new Entities.Ship() { Name = "Test1", Latitude = 77, Longitude = 55, Velocity = 50, Id = ShipId });
            mockTestContext.Ports.Add(new Port() { Id = PortId, Name = "Test1", Latitude = 66, Longitude = 88 });
            mockTestContext.SaveChanges();
            ShipViewModel model = new ShipViewModel() { Name = "Test2", Latitude = 1000, Longitude = 1100, Velocity = 50 };

            await service.Add(model);

            // Act
            await service.UpdateVelocity(
                ShipId,
                49);

            // Assert
            Assert.NotNull(mockTestContext.Ships.Where(x => x.Velocity == 49).First());
        }

        [Fact]
        public async Task UpdateVelocity_ShipNotFound_ShouldThrowException()
        {
            // Arrange
            var service = this.CreateService();
            var ShipId = Guid.NewGuid();
            var PortId = Guid.NewGuid();
            mockTestContext.Ships.Add(new Entities.Ship() { Name = "Test1", Latitude = 77, Longitude = 55, Velocity = 50, Id = ShipId });
            mockTestContext.Ports.Add(new Port() { Id = PortId, Name = "Test1", Latitude = 66, Longitude = 88 });
            mockTestContext.SaveChanges();
            ShipViewModel model = new ShipViewModel() { Name = "Test2", Latitude = 1000, Longitude = 1100, Velocity = 50 };

            await service.Add(model);

            // Act
            var exception = await Record.ExceptionAsync(async () =>
                await service.UpdateVelocity(
                    Guid.NewGuid(),
                    49)
                );
            // Assert
            Assert.IsType(typeof(NotFoundException), exception);
        }
    }
}