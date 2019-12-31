using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeDLL
{
    public class Picture
    {
        private int roomSize = 10;
        private int connectorSize = 10;
        private int connectorThickeness = 2;

        public void Draw(Maze maze)
        {
            int maxX = maze.Rooms.GetLength(0);
            int maxY = maze.Rooms.GetLength(1);

            int xDimension = maxX * (roomSize + connectorSize) + 2 * connectorSize;
            int yDimension = maxY * (roomSize + connectorSize) + 2 * connectorSize;


            using (Image<Rgba32> image = new Image<Rgba32>(xDimension, yDimension))
            {
                DrawRooms(maze, image);
                DrawConnections(maze, image);


                image.Save("Maze.png");
            }
        }

        private void DrawRooms(Maze maze, Image<Rgba32> image)
        {
            foreach (Room room in maze.Rooms)
            {
                int xRooms = maze.Rooms.GetLength(0);
                int yRooms = maze.Rooms.GetLength(1);
                for (int xRoom = 0; xRoom < xRooms; xRoom++)
                {
                    for (int yRoom = 0; yRoom < yRooms; yRoom++)
                    {
                        RoomPosition roomPosition = new RoomPosition(xRoom, yRoom, roomSize, connectorSize);

                        for (int x = 0; x < roomSize; x++)
                        {
                            for (int y = 0; y < roomSize; y++)
                            {
                                int xPos = roomPosition.Left + x;
                                int yPos = roomPosition.Bottom + y;

                                image[xPos, ReverseY(yPos, image)] = Rgba32.Black;
                            }
                        }
                    }
                }
            }
        }

        private int ReverseY(int y, Image<Rgba32> image)
        {
            //not sure why but off by 1 so we offset here
            return image.Height - y - 1;
        }

        private void DrawConnections(Maze maze, Image<Rgba32> image)
        {
            int xRooms = maze.Rooms.GetLength(0);
            int yRooms = maze.Rooms.GetLength(1);

            for (int xRoom = 0; xRoom < xRooms; xRoom++)
            {
                for (int yRoom = 0; yRoom < yRooms; yRoom++)
                {
                    RoomPosition roomPosition = new RoomPosition(xRoom, yRoom, roomSize, connectorSize);

                    int yPos = roomPosition.Bottom + roomSize / 2;
                    int xPos = 0;


                    if (maze.Rooms[xRoom, yRoom].East)
                    {
                        for (int i = 0; i < connectorSize; i++)
                        {
                            xPos = roomPosition.Right + i;
                            image[xPos, ReverseY(yPos, image)] = Rgba32.Black;
                        }
                    }

                    if (maze.Rooms[xRoom, yRoom].West)
                    {
                        for (int i = 0; i < connectorSize; i++)
                        {
                            xPos = roomPosition.Left - 1 - i;
                            image[xPos, ReverseY(yPos, image)] = Rgba32.Black;
                        }
                    }


                    xPos = roomPosition.Left + roomSize / 2;

                    if (maze.Rooms[xRoom, yRoom].North)
                    {
                        for (int i = 0; i < connectorSize; i++)
                        {
                            yPos = roomPosition.Bottom - 1 - i; //because y is reversed
                            image[xPos, ReverseY(yPos, image)] = Rgba32.Black;
                        }
                    }

                    if (maze.Rooms[xRoom, yRoom].South)
                    {
                        for (int i = 0; i < connectorSize; i++)
                        {
                            yPos = roomPosition.Top + i;  //because y is reversed
                            image[xPos, ReverseY(yPos, image)] = Rgba32.Black;
                        }
                    }
                }
            }
        }

        private class RoomPosition
        {
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
            public int Left { get; set; }

            public RoomPosition(int x, int y, int roomSize, int connectorSize)
            {
                Top = connectorSize + y * (roomSize + connectorSize) + roomSize;
                Right = connectorSize + x * (roomSize + connectorSize) + roomSize;
                Bottom = connectorSize + y * (roomSize + connectorSize);
                Left = connectorSize + x * (roomSize + connectorSize);
            }
        }
    }
}
