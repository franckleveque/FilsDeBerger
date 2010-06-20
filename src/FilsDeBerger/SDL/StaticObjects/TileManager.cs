namespace FilsDeBerger.SDL.StaticObjects
{
    using System.Collections.Generic;
    using System.Drawing;

    using SdlDotNet.Graphics;

    /// <summary>
    /// Manage all tiles for the product
    /// </summary>
    public static class TileManager
    {
        #region Fields
        /// <summary>
        /// Thread locker to prevent multiple instance to access at the same time to the object
        /// </summary>
        private static object locker = new object();

        /// <summary>
        /// Tiles surface dictionnary
        /// </summary>
        private static Dictionary<string, Surface> tilemap = new Dictionary<string, Surface>();

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of tile objects
        /// </summary>
        private static string Path
        {
            get
            {
                return @"Graphics\Background";
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Retrieve a Tile from it's tilemap, given its coordinates and its size
        /// </summary>
        /// <param name="toFill">Surface which will reprensent the tile</param>
        /// <param name="fileName">Filename of the tilemap</param>
        /// <param name="offsetX">Horizontal offset of the tile in the tile map</param>
        /// <param name="offsetY">Vertical offset of the tile in the tile map</param>
        public static void FromTilemap(SdlDotNet.Graphics.Surface toFill, string fileName, int offsetX, int offsetY)
        {
            if (!tilemap.ContainsKey(fileName))
            {
                tilemap.Add(fileName, new Surface(System.IO.Path.Combine(Path, fileName)));
            }

            toFill.Blit(tilemap[fileName], new Point(0, 0), new Rectangle(offsetX, offsetY, toFill.Width, toFill.Height));
        }

        #endregion Methods
    }
}