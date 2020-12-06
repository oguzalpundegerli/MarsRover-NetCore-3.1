using Nasa.MarsRover.Core.Enums;

namespace Nasa.MarsRover.Entities
{
    public class Rover
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public DirectionType DirectionType { get; set; }
    }
}
