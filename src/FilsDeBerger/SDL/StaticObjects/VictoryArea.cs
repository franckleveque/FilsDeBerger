namespace FilsDeBerger.SDL.StaticObjects
{
    using System.Drawing;

    using SdlDotNet.Graphics;

    /// <summary>
    /// Class used to determine victory area
    /// </summary>
    public class VictoryArea : Surface
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the VictoryArea class.
        /// </summary>
        /// <param name="area">Area of victory zone</param>
        public VictoryArea(Rectangle area, Context context)
            : base(area)
        {
            this.UpperLeft = new Point(area.Left, area.Top);
            this.Area = area;

            Surface toFill = new Surface(new Size(32, 32));
            int x = 0;
            int y = 0;

            // Get left upper corner
            TileManager.FromTilemap(toFill, "003-G_Ground01.png", 0 * toFill.Width, 1 * toFill.Height);
            this.Blit(toFill);
            
            // Upper separation of victory zone
            TileManager.FromTilemap(toFill, "003-G_Ground01.png", 1 * toFill.Width, 1 * toFill.Height);
            x += toFill.Width;
            while (x < this.Width)
            {
                this.Blit(toFill, new Point(x, 0));
                x += toFill.Width;
            }

            // Left separation of victory zone
            TileManager.FromTilemap(toFill, "003-G_Ground01.png", 0 * toFill.Width, 2 * toFill.Height);
            y += toFill.Height;
            while (y < this.Height)
            {
                this.Blit(toFill, new Point(0, y));
                y += toFill.Height;                
            }

            // Center of the victory zone
            TileManager.FromTilemap(toFill, "003-G_Ground01.png", 2 * toFill.Width, 2 * toFill.Height);
            x = toFill.Width;
            y = toFill.Height;
            while (x < this.Width)
            {
                while (y < this.Height)
                {
                    this.Blit(toFill, new Point(x, y));
                    y += toFill.Height;
                }

                x += toFill.Width;
            }

            // Get left lower corner
            TileManager.FromTilemap(toFill, "003-G_Ground01.png", 0 * toFill.Width, 3 * toFill.Height);
            this.Blit(toFill, new Point(0, this.Height - toFill.Height));

            // Lower separation of victory zone
            TileManager.FromTilemap(toFill, "003-G_Ground01.png", 1 * toFill.Width, 3 * toFill.Height);
            x += toFill.Width;
            while (x < this.Width)
            {
                this.Blit(toFill, new Point(x, this.Height - toFill.Height));
                x += toFill.Width;
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the area of the victory zone
        /// </summary>
        public Rectangle Area
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the upper left corner of the victory zone
        /// </summary>
        public Point UpperLeft
        {
            get;
            private set;
        }

        #endregion Properties
    }
}