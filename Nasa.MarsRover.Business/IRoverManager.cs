using Nasa.MarsRover.Entities;
using System.Collections.Generic;

namespace Nasa.MarsRover.Business
{
    public interface IRoverManager
    {
        Rover TurnLeft(string roverLocation);
        Rover TurnRight(string roverLocation);
        Rover MoveForward(string roverLocation, string plateauCoordinate);
        IEnumerable<Rover> MoveRovers(List<RoverMovement> roversMovement);
        string GetRoverLocationString(Rover rover);
        string GetPlateauCoordinateString(Plateau plateau);
    }
}
