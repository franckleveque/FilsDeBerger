using System;
using System.Collections.Generic;
using System.Text;

namespace FilsDeBerger.SDL
{
    public class Sheep : Character
    {
        public Sheep() : base("sheep.png")
        {
            base.Speed = 1;
            base.Control = Controller.IA;
            base.Think = IA.SheepIA.Think;
        }
    }
}
