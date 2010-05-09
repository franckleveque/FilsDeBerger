using System;
using System.Collections.Generic;
using System.Text;

namespace FilsDeBerger.SDL
{
    public class Shepherd : Character
    {
        public Shepherd(): base("shepherd.png")
        {
            base.Speed = 2;
            base.Control = Controller.Player;
        }
    }
}
