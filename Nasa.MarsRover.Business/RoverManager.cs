using Nasa.MarsRover.Core.Enums;
using Nasa.MarsRover.Core.Helper;
using Nasa.MarsRover.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nasa.MarsRover.Business
{
    public class RoverManager : IRoverManager
    {
        public Rover TurnLeft(string roverLocation)
        {
            var rover = _GetRoverPosition(roverLocation);
            if (rover != null)
            {
                switch (rover.DirectionType)
                {
                    case DirectionType.N:
                        rover.DirectionType = DirectionType.W;
                        break;
                    case DirectionType.S:
                        rover.DirectionType = DirectionType.E;
                        break;
                    case DirectionType.E:
                        rover.DirectionType = DirectionType.N;
                        break;
                    case DirectionType.W:
                        rover.DirectionType = DirectionType.S;
                        break;
                }
            }

            return rover;
        }

        public Rover TurnRight(string roverLocation)
        {
            var rover = _GetRoverPosition(roverLocation);
            if (rover != null)
            {
                switch (rover.DirectionType)
                {
                    case DirectionType.N:
                        rover.DirectionType = DirectionType.E;
                        break;
                    case DirectionType.S:
                        rover.DirectionType = DirectionType.W;
                        break;
                    case DirectionType.E:
                        rover.DirectionType = DirectionType.S;
                        break;
                    case DirectionType.W:
                        rover.DirectionType = DirectionType.N;
                        break;
                }
            }

            return rover;
        }

        public Rover MoveForward(string roverLocation, string plateauCoordinate)
        {
            var rover = _GetRoverPosition(roverLocation);
            if (rover != null)
            {
                switch (rover.DirectionType)
                {
                    case DirectionType.N:
                        ++rover.YCoordinate;
                        break;
                    case DirectionType.S:
                        --rover.YCoordinate;
                        break;
                    case DirectionType.E:
                        ++rover.XCoordinate;
                        break;
                    case DirectionType.W:
                        --rover.XCoordinate;
                        break;
                }
            }

            var plateau = _GetPlateauCoordinates(plateauCoordinate);
            if (!_IsValidCoordinate(rover, plateau))
            {
                throw new Exception("The rover position is invalid. Rover is outside of the Plateau");
            }

            return rover;
        }

        public IEnumerable<Rover> MoveRovers(List<RoverMovement> roversMovement)
        {
            var rovers = new List<Rover>();
            if (roversMovement?.Any() == true)
            {
                roversMovement.ForEach(roverMovement =>
                {
                    if (!roverMovement.MovementProcess.IsNullOrWhitespace())
                    {
                        var rover = _GetRoverPosition(roverMovement.RoverLocation);
                        var plateau = _GetPlateauCoordinates(roverMovement.PlateauCoordinate);
                        foreach (var c in roverMovement.MovementProcess)
                        {
                            switch (c.ToEnum(MovementType.STOP))
                            {
                                case MovementType.M:
                                    rover = MoveForward(GetRoverLocationString(rover), GetPlateauCoordinateString(plateau));
                                    break;
                                case MovementType.L:
                                    rover = TurnLeft(GetRoverLocationString(rover));
                                    break;
                                case MovementType.R:
                                    rover = TurnRight(GetRoverLocationString(rover));
                                    break;
                            }
                        }
                        rovers.Add(rover);
                    }
                });
            }

            return rovers;
        }

        public string GetRoverLocationString(Rover rover)
        {
            return $"{rover.XCoordinate} {rover.YCoordinate} {rover.DirectionType}";
        }

        public string GetPlateauCoordinateString(Plateau plateau)
        {
            return $"{plateau.Width} {plateau.Height}";
        }


        #region Helper Static Fields

        private static Rover _GetRoverPosition(string location)
        {
            var rover = new Rover();
            var locations = location.Split(" ");
            if (locations?.Length == 3)
            {
                int.TryParse(locations[0], out int xCoordinate);
                int.TryParse(locations[1], out int yCoordinate);

                rover.XCoordinate = xCoordinate;
                rover.YCoordinate = yCoordinate;
                rover.DirectionType = locations[2].ToEnum(defaultValue: DirectionType.STOP);
            }

            return rover;
        }

        private static Plateau _GetPlateauCoordinates(string coordinate)
        {
            var plateau = new Plateau();
            var coordinates = coordinate.Split(" ");
            if (coordinates?.Length == 2)
            {
                plateau.Width = int.Parse(coordinates[0]);
                plateau.Height = int.Parse(coordinates[1]);
            }

            return plateau;
        }

        private bool _IsValidCoordinate(Rover rover, Plateau plateau)
        {
            return rover.XCoordinate >= 0 && rover.XCoordinate <= plateau.Width
                && rover.YCoordinate >= 0 && rover.YCoordinate <= plateau.Height;
        }

        #endregion Helper Static Fields
    }
}
