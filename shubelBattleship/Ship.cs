using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shubelBattleship
{
    public class Ship
    {
        public virtual string name { get; set; }  // to be overwritten in derived classes
        public char ID;
        Random rndGen = new Random();

        // Store coords for front of ship (row)
        public int bowX;
        // store coords for front of ship (col)
        public int bowY;
        // store coords for stern
        // will be updated when ship is built.
        public int sternX;
        public int sternY;
   

        /// <summary>
        /// If ship is hit as many times as it is long (assuming it doesnt get hit in the same spot) will set == true
        /// </summary>
        public bool isSunk = false;



        /// <summary>
        /// store how long the ship is (duh) will change based on type of ship
        /// </summary>
        public virtual int shipLength { get; set; } = 0;

        // store how many times the ship is hit
        // will compare to shiplength to determine if sunk
        public int hitCount;


        public void setIsSunk()
        {
           isSunk = true;
        }

        public virtual void SetCoords()
        {
            // set bow randomly, and randomize for direction
            bowX = rndGen.Next(0, 9);
            bowY = rndGen.Next(0, 9);


        }
    } // end class
} // end namespace