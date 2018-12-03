using System;
using System.IO;
using System.Reflection;

namespace RoverApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string LocalPath = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            string TestDataLocation = Path.Combine(LocalPath, "Test.txt");
            string[] Commands = File.ReadAllLines(TestDataLocation);

            Map Map = null;
            Rover ActiveRover = null;

            for (int i = 0; i < Commands.Length; i++)
            {
                if (i == 0)
                {
                    string[] MapSize = Commands[i].Split(' ');
                    Map = new Map(MapSize[0], MapSize[1]);
                    continue;
                }

                if (i % 2 == 0)
                {
                    char[] MoveCommands = Commands[i].ToCharArray();

                    if (ActiveRover == null  || Map == null)
                    {
                        Console.WriteLine("Uninitialised objects");
                        continue;
                    }

                    foreach(char Command in MoveCommands)
                    {
                        Command RequestedCommand = RoverApplication.Command.M;
                        try
                        {
                            Enum.TryParse(Command.ToString(), out RequestedCommand);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Unrecognised Rover Command");
                            continue;
                        }

                        switch (RequestedCommand)
                        {
                            case RoverApplication.Command.M:
                                if (Map.CanDoMove(ActiveRover.GetCurrentPosition(), ActiveRover.GetCurrentDirection()))
                                {
                                    ActiveRover.DoMove(RequestedCommand);
                                }
                                break;
                            case RoverApplication.Command.L:
                            case RoverApplication.Command.R:
                                ActiveRover.DoTurn(RequestedCommand);
                                break;
                            default:
                                Console.WriteLine("Unrecognised Rover Command");
                                continue;
                        }
                    }

                    Position FinalRoverPosition = ActiveRover.GetCurrentPosition();

                    Console.WriteLine(FinalRoverPosition.X + " " + FinalRoverPosition.Y + 
                        " " + ActiveRover.GetCurrentDirection().ToString("g"));
                }
                else
                {
                    string[] InitialRoverParameters = Commands[i].Split(' ');
                    ActiveRover = new Rover(InitialRoverParameters[0], InitialRoverParameters[1], InitialRoverParameters[2]);
                }
            }

            Console.ReadLine();
        }
    }
}
