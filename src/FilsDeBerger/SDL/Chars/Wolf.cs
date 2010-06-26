using System;
using System.Collections.Generic;
using System.Text;

namespace FilsDeBerger.SDL
{
    public class Wolf : Character
    {
        public Wolf()
            : base("wolf.png")
        {
            this.Speed = 3;
            this.Control = Controller.IA;
            this.Think = IA.WolfIA.Think;
            this.Eatable = false;
        }
    }
}
