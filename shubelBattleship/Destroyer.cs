using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shubelBattleship
{
    public class Destroyer : Ship
    {
        public override string name { get; set; } = "Destroyer";
        new public char ID = 'D';
        public override int shipLength { get; set; } = 2;
        public override void SetCoords()
        {
            base.SetCoords();
        }
    }
}