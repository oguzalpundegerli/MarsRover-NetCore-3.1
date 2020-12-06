using Microsoft.Extensions.DependencyInjection;
using Nasa.MarsRover.Business;
using System;

namespace Nasa.MarsRover.AppConsole
{
    public class Bootstrapper
    {
        public IServiceProvider ServiceProvider
        {
            get
            {
                return new ServiceCollection()
                    .AddSingleton<IRoverManager, RoverManager>()
                    .BuildServiceProvider();
            }
        }
    }
}
