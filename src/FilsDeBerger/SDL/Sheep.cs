﻿namespace FilsDeBerger.SDL
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Game class of sheep
    /// </summary>
    public class Sheep : Character
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Sheep class.
        /// </summary>
        public Sheep()
            : base("sheep.png")
        {
            this.Speed = 1;
            this.Control = Controller.IA;
            this.Think = IA.SheepIA.Think;
        }

        #endregion Constructors
    }
}