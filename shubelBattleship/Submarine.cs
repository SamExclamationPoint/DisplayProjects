using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shubelBattleship
{
    public class Submarine : Ship
    {
        public override string name { get; set; } = "Submarine";
        new public char ID = 's';
        public override int shipLength { get; set; } = 3;
        public override void SetCoords()
        {
            base.SetCoords();
        }

    }
}