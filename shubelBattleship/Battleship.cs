using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shubelBattleship
{
    public class Battleship : Ship
    {
        public override string name { get; set; } = "Battleship";
        new public char ID = 'B';
        public override int shipLength { get; set; } = 4;
        public override void SetCoords()
        {
            base.SetCoords();
        }
    }
}