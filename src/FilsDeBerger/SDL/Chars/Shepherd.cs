namespace FilsDeBerger.SDL
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Game class of shepherd
    /// </summary>
    public class Shepherd : Character
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Shepherd class.
        /// </summary>
        public Shepherd()
            : base("shepherd.png")
        {
            this.Speed = 2;
            this.Control = Controller.Player;
            this.Eatable = true;
        }

        #endregion Constructors
    }
}