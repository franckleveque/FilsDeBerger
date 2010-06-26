namespace FilsDeBerger.SDL
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Game class of dog
    /// </summary>
    public class Dog : Character
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Dog class.
        /// </summary>
        public Dog()
            : base("dog.png")
        {
            this.Speed = 3;
            this.Control = Controller.AltPlayer;
            this.Eatable = false;
        }

        #endregion Constructors
    }
}