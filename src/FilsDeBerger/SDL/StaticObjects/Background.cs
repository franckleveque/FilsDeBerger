namespace FilsDeBerger.SDL.StaticObjects
{
    using System;

    using SdlDotNet.Graphics;

    /// <summary>
    /// Represent the background image of the app.
    /// </summary>
    public class Background : SdlDotNet.Graphics.Surface
    {
        #region Fields

        /// <summary>
        /// Random number generator
        /// </summary>
        private static Random posAlea = new Random();

        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Background class.
        /// </summary>
        /// <param name="width">Width of the background</param>
        /// <param name="height">Height of the background</param>
        public Background(int width, int height)
            : base(width, height)
        {
            // First fill background of grass
            Surface toFill = new Grass();

            int x = 0;
            int y = 0;
            while (x < width)
            {
                while (y < height)
                {
                    this.Blit(toFill, new System.Drawing.Point(x, y));
                    y += toFill.Height;
                }

                y = 0;
                x += toFill.Width;
            }

            // Add some gravels
            toFill = new Gravel();
            this.AddRandomElement(5, toFill);

            // Add some herbs
            toFill = new Herbs();
            this.AddRandomElement(5, toFill);

            // Add some flowers to BackGround
            toFill = new Flower();
            this.AddRandomElement(10, toFill);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Add random elements on the background
        /// </summary>
        /// <param name="maxNumberOfElements">Maximum number of this element to display</param>
        /// <param name="toFill">Element to display</param>
        private void AddRandomElement(int maxNumberOfElements, Surface toFill)
        {
            int numberOfElement = posAlea.Next(maxNumberOfElements);
            for (int i = 0; i < numberOfElement; i++)
            {
                // Let's blit element as tile
                this.Blit(
                    toFill,
                    new System.Drawing.Point(
                        System.Convert.ToInt32(Math.Floor(posAlea.Next(this.Width) / 32.0) * 32),
                        System.Convert.ToInt32(Math.Floor(posAlea.Next(this.Height) / 32.0) * 32)));
            }
        }

        #endregion Methods
    }
}