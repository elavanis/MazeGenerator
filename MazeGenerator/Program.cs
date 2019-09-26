using MazeDLL;
using System;
using System.Diagnostics;

namespace MazeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int x = 0; x < 1; x++)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Maze maze = new Maze();

                maze.Generate(10, 10, 50);
                sw.Stop();

                Console.WriteLine(sw.ElapsedMilliseconds);
            }
        }
    }
}
