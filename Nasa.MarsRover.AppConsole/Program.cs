using Microsoft.Extensions.DependencyInjection;
using Nasa.MarsRover.Business;
using Nasa.MarsRover.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nasa.MarsRover.AppConsole
{
    class Program
    {
        static IRoverManager _RoverManager;

        public static void Initialize()
        {
            var bootstrap = new Bootstrapper();
            _RoverManager = bootstrap.ServiceProvider.GetService<IRoverManager>();
        }

        static void Main(string[] args)
        {
            Initialize();

            var rovers = new List<RoverMovement>
            {
                new RoverMovement { PlateauCoordinate="5 5", RoverLocation ="1 2 N",  MovementProcess="LMLMLMLMM" },
                new RoverMovement { PlateauCoordinate="5 5", RoverLocation ="3 3 E",  MovementProcess="MMRMMRMRRM" }
            };

            var result = _RoverManager.MoveRovers(rovers).ToList();

            var sb = new StringBuilder();
            for (int i = 0; i < rovers.Count; i++)
            {
                sb.AppendLine("INPUTS: ")
                .Append("Plateau Coordinate: ").Append("\t").AppendLine(rovers[i].PlateauCoordinate)
                .Append("Rover Location: ").Append("\t").AppendLine(rovers[i].RoverLocation)
                .Append("Movement Process: ").Append("\t").AppendLine(rovers[i].MovementProcess)
                .Append("OUTPUT: ").Append("\t\t").AppendLine(_RoverManager.GetRoverLocationString(result[i]))
                .AppendLine("-------------------------------------------------").AppendLine();
            }

            Console.Write(sb.ToString());
            Console.ReadKey();
        }
    }
}
