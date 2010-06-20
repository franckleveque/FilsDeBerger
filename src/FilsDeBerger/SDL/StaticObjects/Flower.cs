namespace FilsDeBerger.SDL.StaticObjects
{
    using SdlDotNet.Graphics;

    /// <summary>
    /// Represents a flower tile
    /// </summary>
    public class Flower : Surface
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Flower class.
        /// </summary>
        public Flower()
            : base(new System.Drawing.Size(32, 32))
        {
            TileManager.FromTilemap(this, "023-FarmVillage01.png", 0, 3 * 32);
            this.Transparent = true;
        }

        #endregion Constructors
    }
}