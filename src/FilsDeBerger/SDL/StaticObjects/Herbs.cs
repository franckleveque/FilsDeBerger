namespace FilsDeBerger.SDL.StaticObjects
{
    using SdlDotNet.Graphics;

    /// <summary>
    /// Represent a herb tile
    /// </summary>
    public class Herbs : Surface
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Herbs class.
        /// </summary>
        public Herbs()
            : base(new System.Drawing.Size(32, 32))
        {
            TileManager.FromTilemap(this, "023-FarmVillage01.png", 0, 2 * 32);
            this.Transparent = true;
        }

        #endregion Constructors
    }
}