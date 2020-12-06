
using Microsoft.Extensions.DependencyInjection;
using Nasa.MarsRover.Business;
using Nasa.MarsRover.Core.Enums;
using Nasa.MarsRover.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Nasa.MarsRover.Tests
{
    public class RoverMovementTests
    {
        readonly IRoverManager _RoverManager;
        public RoverMovementTests()
        {
            var bootstrap = new Bootstrapper();
            _RoverManager = bootstrap.ServiceProvider.GetService<IRoverManager>();
        }

        [Fact]
        public void TurnLeft()
        {
            // Arrange
            string roverLocation = "1 2 N";

            // Act
            var rover = _RoverManager.TurnLeft(roverLocation);

            // Assert
            Assert.Equal(DirectionType.W, rover.DirectionType);
        }

        [Fact]
        public void TurnRight()
        {
            // Arrange
            string roverLocation = "1 2 N";

            // Act
            var rover = _RoverManager.TurnRight(roverLocation);

            // Assert
            Assert.Equal(DirectionType.E, rover.DirectionType);
        }

        [Fact]
        public void MoveForward()
        {
            // Arrange
            string roverLocation = "1 2 N";
            string plateauCoordinate = "5 5";

            // Act
            var rover = _RoverManager.MoveForward(roverLocation, plateauCoordinate);

            // Assert
            Assert.Equal(3, rover.YCoordinate);
            Assert.Equal(1, rover.XCoordinate);
        }

        [Fact]
        public void CommandRovers()
        {
            // Arrange
            var rovers = new List<RoverMovement>
            {
                new RoverMovement { PlateauCoordinate="5 5", RoverLocation ="1 2 N",  MovementProcess="LMLMLMLMM" },
                new RoverMovement { PlateauCoordinate="5 5", RoverLocation ="3 3 E",  MovementProcess="MMRMMRMRRM" }
            };

            // Act
            var result = _RoverManager.CommandRovers(rovers).ToList();

            // Assert
            Assert.Equal("1 3 N", $"{result[0].XCoordinate} {result[0].YCoordinate} {result[0].DirectionType}");
            Assert.Equal("5 1 E", $"{result[1].XCoordinate} {result[1].YCoordinate} {result[1].DirectionType}");
        }
    }
}
