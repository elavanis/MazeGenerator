using MazeDLL;
using System;
using System.Diagnostics;

namespace MazeGenerator
{
    class Program
    {
        private static void Main(string[] args)
        {
            for (int x = 0; x < 1; x++)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Maze maze = new Maze();

                maze.Generate(10, 10, 100);
                sw.Stop();
                Console.WriteLine(sw.ElapsedMilliseconds);

                sw.Restart();
                Picture mazeDrawer = new Picture();
                mazeDrawer.Draw(maze);
                sw.Stop();
                Console.WriteLine(sw.ElapsedMilliseconds);
            }
        }
    }
}

