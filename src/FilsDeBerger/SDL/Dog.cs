using System;
using System.Collections.Generic;
using System.Text;

namespace FilsDeBerger.SDL
{
    public class Dog : Character
    {
        public Dog()
            : base("dog.png")
        {
            base.Speed = 3;
            base.Control = Controller.AltPlayer;
        }
    }
}
