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
        public VictoryArea(Rectangle area)
            : base(area)
        {
            this.UpperLeft = new Point(area.Left, area.Top);
            this.Area = area;
            this.Fill(Color.Green);
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