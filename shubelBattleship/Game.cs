using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shubelBattleship
{
    public class Game
    {
        // properties needed
        Gameboard board = new Gameboard();
        Destroyer destroyer1 = new Destroyer();
        Destroyer destroyer2 = new Destroyer();
        Submarine submarine = new Submarine();
        Submarine submarine2 = new Submarine();
        Battleship battleship = new Battleship();
        Carrier carrier = new Carrier();
        string superSecretPW = "WallhacksPLZ";

        /// <summary>
        /// contains all logic for game
        /// </summary>
        public void PlayGame()
        {
            // array to store our ships
            Ship[] myShips = {carrier, battleship, submarine, submarine2, destroyer1, destroyer2};

            Console.WriteLine("Welcome to *one sided* Battleship!");
            board.InitializeBoard();
            Console.WriteLine("Do you have anything to say?");
            string inputPW = Console.ReadLine();
            if (inputPW == superSecretPW)
            {
                Console.WriteLine("Cheat mode activated");
                // set coords and place ships
                for (int i = 0; i < myShips.Length; i++)
                {
                    board.PlaceShips(myShips[i]);
                }

                Console.WriteLine("Lets play a game...");
                // actual game loop
                // ends when all ships get sunk
                // hit >= length
                while (!AllShipsSunk(myShips))
                {
                    board.DisplayBoardCheat();
                    int xToShoot = TakeInput("Row", board);
                    int yToShoot = TakeInput("Column", board);

                    CheckIfHit(myShips, board, xToShoot, yToShoot);




                }




            } // end cheat mode game
            else
            {
                Console.WriteLine("Oh okay");


                // set coords and place ships
                for (int i = 0; i < myShips.Length; i++)
                {
                    board.PlaceShips(myShips[i]);
                }

                Console.WriteLine("Lets play a game...");
                // actual game loop
                // ends when all ships get sunk
                // hit >= length
                while (!AllShipsSunk(myShips))
                {
                    board.DisplayBoard();
                    int xToShoot = TakeInput("Row", board);
                    int yToShoot = TakeInput("Column", board);

                    CheckIfHit(myShips, board, xToShoot, yToShoot);




                }
            } // end basic mode


            // end game, you won!
            Console.WriteLine("Victory!");


        } // end PlayGame

        // update to validate input....
        public int TakeInput(string rowOrCol, Gameboard board)
        {
            bool validInput = false;
            int coordToShoot = 0;
            while (!validInput)
            {
                Console.WriteLine($"Enter the {rowOrCol} you wish to shoot");
                string input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input)) // Check if input is not null or empty
                {
                    if (int.TryParse(input, out coordToShoot)) // Check if input can be parsed as an integer
                    {
                        coordToShoot--; // decrement value to fit within the array
                                        // does coord fall on the board?
                        if (coordToShoot >= 0 && coordToShoot < board.gameboard.GetLength(1))
                        {
                            validInput = true;
                        }
                        else
                        {
                            Console.WriteLine("Error, Coordinate is not on the board");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error, Please enter a valid integer");
                    }
                }
                else
                {
                    Console.WriteLine("Error, Input cannot be empty");
                }
            }
            return coordToShoot;
        }

        public void CheckIfHit(Ship[] shipType, Gameboard board, int row, int col)
        { 
            // Check if the coordinate (x, y) falls between the bow and stern of the ship and spot has not already been shot
            // 0 = open 1 = ship there 2 = shot and miss 3 = shot and hit
           if (board.gameboard[row,col] == 1)
           {
                // update board to hit and 
                // then check if which ship was shot
                Console.WriteLine("Hit!");
                board.gameboard[row, col] = 3;
                foreach (Ship ship in shipType)
                {
                    // find which ship was hit
                    if (((row >= ship.bowX && row <= ship.sternX) && (col >= ship.bowY && col <= ship.sternY)) ||
                    ((row <= ship.bowX && row >= ship.sternX) && (col <= ship.bowY && col >= ship.sternY))) {
                        ship.hitCount++; 
                        CheckIfSunk(ship);
                        break;
                    }

                }
           } else if (board.gameboard[row,col] == 2 || board.gameboard[row, col] == 3)
           {
                Console.WriteLine("You have already shot there!");
           } else if (board.gameboard[row,col] == 0)
           {
                Console.WriteLine("Miss!");
                board.gameboard[row,col] = 2; // set shot at but missed
           }
            

        } // end CheckIfHit

        public void CheckIfSunk(Ship shipType)
        {
            // if the ship has been hit more times than it is long,
            // it has been sunk
            // if not, do nothing
            if (shipType.hitCount >= shipType.shipLength)
            {
                Console.WriteLine($"You sunk my {shipType.name}!");
                shipType.setIsSunk();

            } 
        }

        // need to only return true if ALLL ships are sunk!
        public bool AllShipsSunk(Ship[] myShips)
        {
            bool allSunk = true;
            foreach (Ship ship in myShips) 
            {
                if (!ship.isSunk)
                {
                    allSunk = false;
                }
            }
            return allSunk;
        }

    } // end class
} // end namespace