namespace FilsDeBerger.SDL.StaticObjects
{
    using SdlDotNet.Graphics;

    /// <summary>
    /// Represent a Gravel tile.
    /// </summary>
    public class Gravel : Surface
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Gravel class.
        /// </summary>
        public Gravel()
            : base(new System.Drawing.Size(32, 32))
        {
            TileManager.FromTilemap(this, "023-FarmVillage01.png", 0, 4 * 32);
            this.Transparent = true;
        }

        #endregion Constructors
    }
}