namespace FilsDeBerger.SDL
{
    using System.Drawing;

    using SdlDotNet.Graphics;
    using SdlDotNet.Graphics.Sprites;

    /// <summary>
    /// Delegate function to make thinking characters
    /// </summary>
    /// <param name="curCharacter">Current character which is thinking</param>
    /// <param name="allCharacters">All characters of the game</param>
    /// <returns>Move direction of character</returns>
    public delegate MoveDirection ThinkerDelegate(Character curCharacter, Character[] allCharacters);

    #region Enumerations

    /// <summary>
    /// Indicates type of controller used for the character
    /// </summary>
    public enum Controller
    {
        /// <summary>
        /// Player controlled sprite
        /// </summary>
        Player,

        /// <summary>
        /// Alternate player sprite
        /// </summary>
        AltPlayer,

        /// <summary>
        /// IA controlled sprite
        /// </summary>
        IA,

        /// <summary>
        /// Not moving sprite
        /// </summary>
        None
    }

    /// <summary>
    /// Available move direction for a character
    /// </summary>
    public enum MoveDirection
    {
        /// <summary>
        /// Character is moving up
        /// </summary>
        Up,

        /// <summary>
        /// Character is moving right
        /// </summary>
        Right,

        /// <summary>
        /// Character is moving down
        /// </summary>
        Down,

        /// <summary>
        /// Character is moving left
        /// </summary>
        Left,

        /// <summary>
        /// Character doesn't move
        /// </summary>
        Stop
    }

    #endregion Enumerations

    /// <summary>
    /// Class used to draw and control characters
    /// </summary>
    public class Character : AnimatedSprite
    {
        #region Fields

        /// <summary>
        /// FileName of the sprite file
        /// </summary>
        private string fileName;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Character class.
        /// </summary>
        /// <param name="fileName">FileName of the sprite to load for the character</param>
        public Character(string fileName)
        {
            this.FileName = fileName;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets path of sprites used for characters
        /// </summary>
        public static string Path
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets FileName of the sprite file
        /// </summary>
        public string FileName
        {
            get
            {
                return this.fileName;
            }

            set
            {
                this.fileName = value;
                string file = System.IO.Path.Combine(Character.Path, this.FileName);
                Surface image = new Surface(file);

                int animHeight = image.Height / 4;
                int animWidth = image.Width / 4;

                // Create the animation frames
                SurfaceCollection walkDown = new SurfaceCollection();
                walkDown.Add(image, new Size(animWidth, animHeight), 0);
                SurfaceCollection walkLeft = new SurfaceCollection();
                walkLeft.Add(image, new Size(animWidth, animHeight), 1);
                SurfaceCollection walkRight = new SurfaceCollection();
                walkRight.Add(image, new Size(animWidth, animHeight), 2);
                SurfaceCollection walkUp = new SurfaceCollection();
                walkUp.Add(image, new Size(animWidth, animHeight), 3);

                // Add the animations to the hero
                AnimationCollection animWalkUp = new AnimationCollection();
                animWalkUp.Add(walkUp, animHeight - 1);
                this.Animations.Add("WalkUp", animWalkUp);
                AnimationCollection animWalkRight = new AnimationCollection();
                animWalkRight.Add(walkRight, animHeight - 1);
                this.Animations.Add("WalkRight", animWalkRight);
                AnimationCollection animWalkDown = new AnimationCollection();
                animWalkDown.Add(walkDown, animHeight - 1);
                this.Animations.Add("WalkDown", animWalkDown);
                AnimationCollection animWalkLeft = new AnimationCollection();
                animWalkLeft.Add(walkLeft, animHeight - 1);
                this.Animations.Add("WalkLeft", animWalkLeft);

                // Change the transparent color of the sprite
                Color animTrans = image.GetPixel(new Point(1, 1));
                this.TransparentColor = animTrans;
                this.Transparent = true;

                // Setup the startup animation and make him not walk
                this.CurrentAnimation = "WalkDown";
                this.Animate = false;
            }
        }

        /// <summary>
        /// Gets or sets moving speed of the character
        /// </summary>
        public int Speed
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of controller for the character
        /// </summary>
        public Controller Control
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets IA function for IA controlled characters
        /// </summary>
        public ThinkerDelegate Think
        {
            get;
            set;
        }

        public bool Disposed
        {
            get;
            private set;
        }

        #endregion Properties

        /// <summary>
        /// Calculate the distance of a character
        /// </summary>
        /// <param name="toCheck">Character from which to check the distance</param>
        /// <returns>The euclidian distance of the character</returns>
        public int GetDistance(Character toCheck)
        {
            return System.Math.Abs(this.Position.X - toCheck.Position.X) +
                   System.Math.Abs(this.Position.Y - toCheck.Position.Y);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Disposed = true;
            }

            base.Dispose(disposing);
        }
    }
}