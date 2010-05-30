using System.Drawing;

using SdlDotNet.Graphics;


namespace FilsDeBerger.SDL.StaticObjects
{
    public class VictoryArea : Surface
    {
        public Point UpperLeft
        {
            get;
            private set;
        }

        public Rectangle Area
        {
            get;
            private set;
        }

        public VictoryArea(Rectangle area)
            :base(area)
        {
            UpperLeft = new Point(area.Left, area.Top);
            Area = area;
            this.Fill(Color.Green);
        }

    }
}
