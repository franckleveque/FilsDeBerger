namespace FilsDeBerger.SDL.StaticObjects
{
    /// <summary>
    /// Represent a Grass tile
    /// </summary>
    public class Grass : SdlDotNet.Graphics.Surface
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Grass class.
        /// </summary>
        public Grass()
            : base(new System.Drawing.Size(32, 32))
        {
            TileManager.FromTilemap(this, "023-FarmVillage01.png", 0, 0);
        }

        #endregion Constructors
    }
}