namespace FilsDeBerger
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;

    using global::FilsDeBerger.SDL;

    /// <summary>
    /// Class managing all accessable objects
    /// </summary>
    public class Context
    {
        #region Fields

        /// <summary>
        /// Background surface of screen
        /// </summary>
        private SDL.StaticObjects.Background backGround;

        /// <summary>
        /// List of characters to move in the world
        /// </summary>
        private List<Character> characters = new List<Character>();

        /// <summary>
        /// List of obstacle in the world
        /// </summary>
        private List<Rectangle> obstacles = new List<Rectangle>();

        /// <summary>
        /// Victory area of the game
        /// </summary>
        private SDL.StaticObjects.VictoryArea victoryArea;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets background surface of screen
        /// </summary>
        public SDL.StaticObjects.Background BackGround
        {
            get { return this.backGround; }
            set { this.backGround = value; }
        }

        /// <summary>
        /// Gets list of characters to move in the world
        /// </summary>
        public List<Character> Characters
        {
            get { return this.characters; }
        }

        /// <summary>
        /// Gets list of obstacle in the world
        /// </summary>
        public List<Rectangle> Obstacles
        {
            get { return this.obstacles; }
        }

        /// <summary>
        /// Gets or sets victory area of the game
        /// </summary>
        public SDL.StaticObjects.VictoryArea VictoryArea
        {
            get { return this.victoryArea; }
            set { this.victoryArea = value; }
        }

        #endregion Properties
    }
}