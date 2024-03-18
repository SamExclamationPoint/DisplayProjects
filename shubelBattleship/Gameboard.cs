using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shubelBattleship
{


    // should keep track of where each ship is and shots fired
    public class Gameboard
    {
       static int row = 10;
       static int col = 10;
       public int[,] gameboard = new int[row, col];


        /// <summary>
        /// display the 10X10 board
        /// should look like
        /// [~][~][~] for unknown water
        /// [X][X][X] for misses
        /// [O][O][O] for hits
        /// [S][S][S] for cheat mode revealing ships
        /// </summary>
        public void DisplayBoard()
        {
            // display row and column numbers for anysize board
            for (int i = 0; i < row; i++)
            {
                Console.Write($" {i + 1}  ");
            }
            // newline for board spaces
            Console.WriteLine();
            for (int i = 0; i < row; i++)
            {
                // iterate thru columns
                for (int j = 0; j < col; j++)
                {
                    if (gameboard[i,j] == 0 || gameboard[i,j] == 1)
                    {
                        Console.Write("[~] ");
                    } else if (gameboard[i,j] == 2)
                    {
                        Console.Write("[X] ");
                    }
                    else if (gameboard[i,j] == 3) 
                    {
                        Console.Write("[O] ");
                    }
                    
                }
                // next row
                Console.WriteLine($"{i + 1} ");
            }
            Console.WriteLine();
        } // end DisplayBoard


        // cheat funct
        public void DisplayBoardCheat()
        {
            // display row and column numbers for anysize board
            for (int i = 0; i < row; i++)
            {
                Console.Write($" {i + 1}  ");
            }
            // newline for board spaces
            Console.WriteLine();
            for (int i = 0; i < row; i++)
            {
                // iterate thru columns
                for (int j = 0; j < col; j++)
                {
                    if (gameboard[i,j] == 1)
                    {
                        Console.Write("[S] ");
                    }
                     else if (gameboard[i, j] == 2)
                    {
                        Console.Write("[X] ");
                    }
                    else if (gameboard[i, j] == 3)
                    {
                        Console.Write("[O] ");
                    }
                    else
                    {
                        Console.Write("[~] ");
                    }

                }
                // next row
                Console.WriteLine($"{i + 1} ");
            }
            Console.WriteLine();
        } // end DisplayBoard test

        /// <summary>
        /// fill board with ~
        /// </summary>
        public void InitializeBoard()
        {
            for (int i = 0; i < row; i++) // go thru column
            {
                for(int j = 0; j < col; j++) // go thru row
                {
                    gameboard[i,j] = 0;
                }
            }
        } // end InitializeBoard

        public void PlaceShips(Ship shipType)
        {
            Random ranGen = new Random();

            // loop until ship is built
            while (true)
            {
                // set bow coords
                // will be used to start the ship's construction in a random direction (vertical 0 or horizontal 1)
                shipType.SetCoords();
                int growDirection = ranGen.Next(2);

                // Check if the initial position is available for ship placement
                if (gameboard[shipType.bowX, shipType.bowY] == 0)
                {
                    bool canBuild = true; // Flag to track if ship can be built without overlap or running off the board

                    if (growDirection == 0)
                    {
                        // will build down
                        if (shipType.bowX + shipType.shipLength <= gameboard.GetLength(1))
                        {
                            for (int i = shipType.bowX; i < shipType.bowX + shipType.shipLength; i++)
                            {
                                // check if spot is open and on the board
                                if (gameboard[i, shipType.bowY] == 0)
                                {
                                    gameboard[i, shipType.bowY] = 1; // put ship part
                                }
                                else
                                {
                                    canBuild = false; // Set flag to false if overlap occurs
                                    break; // Exit inner loop
                                }
                            }

                            // Set stern coordinates if ship is successfully built vertically
                            if (canBuild)
                            {
                                shipType.sternX = shipType.bowX + shipType.shipLength - 1;
                                shipType.sternY = shipType.bowY;
                            }
                        }
                        else
                        {
                            canBuild = false; // Set flag to false if ship runs off the board
                        }
                    }
                    else
                    {
                        // will build left to right
                        if (shipType.bowY + shipType.shipLength <= gameboard.GetLength(0))
                        {
                            for (int i = shipType.bowY; i < shipType.bowY + shipType.shipLength; i++)
                            {
                                // check if spot is open 
                                if (gameboard[shipType.bowX, i] == 0)
                                {
                                    gameboard[shipType.bowX, i] = 1; // put ship part
                                }
                                else
                                {
                                    canBuild = false; // Set flag to false if overlap occurs
                                    break; // Exit inner loop
                                }
                            }

                            // Set stern coordinates if ship is successfully built horizontally
                            if (canBuild)
                            {
                                shipType.sternX = shipType.bowX;
                                shipType.sternY = shipType.bowY + shipType.shipLength - 1;
                            }
                        }
                        else
                        {
                            canBuild = false; // Set flag to false if ship runs off the board
                        }
                    }

                    // If ship can be built without overlap or running off the board, exit the loop
                    if (canBuild)
                        break;
                }
            } // End while loop

        } // End PlaceShips



    } // end class
} // end namespace