using System;

namespace LifeSimulators
{
    class World
    {
        // Laws of the world
        private const long MaxRuns = 500;

        private static void Main(string[] args)
        {
            int Height = Console.WindowHeight - 1;
            int Width = Console.WindowWidth - 1;
            long runs = 0;
            MarkLifeSim mls = new MarkLifeSim(Height, Width);

             while (runs++ < MaxRuns)
             {
                 mls.PrintAndEvolve();

                 // Speed of world
                 System.Threading.Thread.Sleep(100);
             }
        }
    }

    public class MarkLifeSim
    {
        private int Height;
        private int Width;
        private bool[,] pods;

        /// <summary>
        /// Create your world Physics using randomization
        /// </summary>
        private void RandomizeWorldSeed()
        {
            Random generator = new Random();
            int number;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    number = generator.Next(2);
                    pods[i, j] = ((number == 0) ? false : true);
                }
            }
        }

        /// <summary>
        /// Create new world using basic canvas then add physics
        /// </summary>
        /// <param name="Height">Height of the pod.</param>
        /// <param name="Width">Width of the pod.</param>
        public MarkLifeSim(int Height, int Width)
        {
            this.Height = Height;
            this.Width = Width;
            pods = new bool[Height, Width];
            RandomizeWorldSeed();
        }

        /// <summary>
        /// Checks how many alive neighbors are around cell
        /// </summary>
        /// <param name="x">X-coordinate of the cell.</param>
        /// <param name="y">Y-coordinate of the cell.</param>
        /// <returns>Number of alive neighbors.</returns>
        private int GetNeighbors(int x, int y)
        {
            int NumOfAliveNeighbors = 0;

            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (!((i < 0 || j < 0) || (i >= Height || j >= Width)))
                    {
                        if (pods[i, j] == true) NumOfAliveNeighbors++;
                    }
                }
            }
            return NumOfAliveNeighbors;
        }

        /// <summary>
        /// Create Evolveth rules for next generation
        /// </summary>
        private void Evolve()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    int numOfAliveNeighbors = GetNeighbors(i, j);

                    if (pods[i, j])
                    {
                        if (numOfAliveNeighbors < 2)
                        {
                            pods[i, j] = false;
                        }

                        if (numOfAliveNeighbors > 3)
                        {
                            pods[i, j] = false;
                        }
                    }
                    else
                    {
                        if (numOfAliveNeighbors == 3)
                        {
                            pods[i, j] = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Proceeds to the next generation and prints to console.
        /// </summary>
        public void PrintAndEvolve()
        {
            PrintGame();
            Evolve();
        }

        /// <summary>
        /// Prints the game to the console.
        /// </summary>
        private void PrintGame()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(pods[i, j] ? "x" : " ");
                    if (j == Width - 1) Console.WriteLine("\r");
                }
            }
            Console.SetCursorPosition(0, Console.WindowTop);
        }
    }
}
