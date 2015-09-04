using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEpicGame
{
    using System.Threading;

    class Program
    {
        private const int Height = 30;

        private const int Width = 70;

        private const char Player = '☻';

        private static int playerY = Height - 1;

        private static int playerX = Width / 2;

        private const char rockSymbol = '♦';

        private static int[,] rocks = new int[2, 1000];

        private const int Xcoordinates = 0;

        private const int Ycoordinates = 1;

        private static int numberOfRocks = 0;

        private static bool isAlive = true;

        private static int hidenZones = 0;

        private static int countRocksInLine = 0;



        static void Main(string[] args)
        {
            Console.SetWindowSize(Width, Height);
            Console.SetBufferSize(Width, Height);
            Console.CursorVisible = false;

            GameLoop();

        }

        public static void GameLoop()
        {

            int count = 0;
            while (isAlive)
            {
                count++;
                Console.Clear(); // clear console first
                CheckMotion(); // check for pressed button and do what happend with current button
                Console.SetCursorPosition(playerX, playerY); // put coordinates of the cursor
                Console.Write(Player); // draw the player
                if (count == 5)
                {
                    AddNewRock();
                    count = 0;
                }
                Thread.Sleep(50);
            }
        }

        public static void CheckMotion()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key;
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.RightArrow)
                {
                    if (playerX < Width - 2)
                    {
                        playerX++;
                    }

                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    if (playerX > 0)
                    {
                        playerX--;
                    }
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    isAlive = false;
                }
            }

        }

        public static void AddNewRock()
        {
            if (numberOfRocks < 1000)
            {
                Random num = new Random();
                int rockX = num.Next(0, 69);
                int rockY = 0;
                rocks[Xcoordinates, numberOfRocks] = rockX;
                rocks[Ycoordinates, numberOfRocks] = rockY;
                numberOfRocks++;
                for (int i = 0; i < 1000; i++)
                {

                    Console.SetCursorPosition(rocks[Xcoordinates, i], rocks[Ycoordinates, i]);

                    if (rocks[Ycoordinates, i] < Height - 1)
                    {
                        rocks[Ycoordinates, i]++;
                        Console.Write(rockSymbol);
                    }
                    else
                    {
                        Console.Write(' ');
                        if (countRocksInLine < Height - 1)
                        {
                            rocks[Xcoordinates, i] = hidenZones;
                            rocks[Ycoordinates, i] = 0;
                            countRocksInLine++;
                        }
                        else
                        {
                            countRocksInLine = 0;
                            if (hidenZones > 20)
                            {
                                hidenZones++;
                            }
                        }

                    }
                    if (rocks[Xcoordinates, i] == playerX && rocks[Ycoordinates, i] == playerY)
                    {
                        isAlive = false;
                        Console.Clear();
                        Console.SetCursorPosition(Width / 2, Height / 2);
                        Console.WriteLine("GAME OVER");
                        Console.ReadKey();
                    }
                }
            }
        }
        }
}
