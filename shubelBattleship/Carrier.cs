using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shubelBattleship
{
    public class Carrier : Ship
    {
        public override string name { get; set; } = "Carrier";
        new public char ID = 'C';
        public override int shipLength { get; set; } = 5;
        public override void SetCoords()
        {
            base.SetCoords();
        }
    }
}