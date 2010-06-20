using FilsDeBerger.SDL;

namespace FilsDeBerger
{
    using System;
    using System.Drawing;
    using System.IO;

    using SdlDotNet;
    using SdlDotNet.Core;
    using SdlDotNet.Graphics;
    using SdlDotNet.Graphics.Sprites;
    using SdlDotNet.Input;

    /// <summary>
    /// Main class of the game
    /// </summary>
    public class FilsDeBerger : IDisposable
    {
        #region Fields

        /// <summary>
        /// List of characters to move in the world
        /// </summary>
        private Character[] characters;

        /// <summary>
        /// Background surface of screen
        /// </summary>
        private SDL.StaticObjects.Background backGround;

        /// <summary>
        /// Victory area of the game
        /// </summary>
        private SDL.StaticObjects.VictoryArea victoryArea;

        /// <summary>
        /// Thread safety locker
        /// </summary>
        private object threadLocker = new object();

        /// <summary>
        /// Indicates whether object have been disposed or not
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Timer for playing time
        /// </summary>
        private System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FilsDeBerger class.
        /// </summary>
        public FilsDeBerger()
        {
            Random posAlea = new Random();

            // Start up the window
            Video.WindowIcon();
            Video.WindowCaption = "Fils de Berger";
            Video.SetVideoMode(800, 600);

            IA.SheepIA.ScreenSize = new Size(Video.Screen.Width, Video.Screen.Height);
            this.backGround = new global::FilsDeBerger.SDL.StaticObjects.Background(800, 600);
            this.characters = new Character[10];
            this.characters[0] = new Shepherd();
            this.characters[1] = new Dog();
            this.characters[0].Center = new Point(
                Video.Screen.Width / 2,
                Video.Screen.Height / 2);

            this.characters[1].Center = new Point(
                (Video.Screen.Width / 2) + 50,
                Video.Screen.Height / 2);

            for (int i = 2; i < 10; i++)
            {
                this.characters[i] = new Sheep();

                // Put him in the center of the screen
                this.characters[i].Center = new Point(
                    (Video.Screen.Width / 4) + posAlea.Next(Video.Screen.Width / 2),
                    (Video.Screen.Height / 4) + posAlea.Next(Video.Screen.Height / 2));
            }

            // Initialize victoryArea
            this.victoryArea = new global::FilsDeBerger.SDL.StaticObjects.VictoryArea(
                new Rectangle(
                    new Point(
                        Convert.ToInt32(Video.Screen.Width * .95),
                        Convert.ToInt32(Video.Screen.Height * .25)),
                    new Size(
                        Convert.ToInt32(Video.Screen.Width * .7),
                        Convert.ToInt32(Video.Screen.Height * .5))));

            // Initialize the thinking of IA
            System.Threading.ThreadPool.QueueUserWorkItem(delegate(object notUsed)
            {
                while (!this.disposed)
                {
                    if (!this.disposed)
                    {
                        // First get only IA controlled 
                        Character[] iaControlled = Array.FindAll(
                            this.characters,
                            delegate(Character toCheck)
                            {
                                return toCheck.Control == Controller.IA && toCheck.Think != null;
                            });

                        foreach (Character curIA in iaControlled)
                        {
                            switch (curIA.Think(curIA, this.characters))
                            {
                                case MoveDirection.Down:
                                    if (curIA.CurrentAnimation != "WalkDown")
                                    {
                                        curIA.CurrentAnimation = "WalkDown";
                                    }

                                    if (!curIA.Animate)
                                    {
                                        curIA.Animate = true;
                                    }

                                    break;
                                case MoveDirection.Left:
                                    if (curIA.CurrentAnimation != "WalkLeft")
                                    {
                                        curIA.CurrentAnimation = "WalkLeft";
                                    }

                                    if (!curIA.Animate)
                                    {
                                        curIA.Animate = true;
                                    }

                                    break;
                                case MoveDirection.Right:
                                    if (curIA.CurrentAnimation != "WalkRight")
                                    {
                                        curIA.CurrentAnimation = "WalkRight";
                                    }

                                    if (!curIA.Animate)
                                    {
                                        curIA.Animate = true;
                                    }

                                    break;
                                case MoveDirection.Stop:
                                    curIA.Animate = false;
                                    break;
                                case MoveDirection.Up:
                                    if (curIA.CurrentAnimation != "WalkUp")
                                    {
                                        curIA.CurrentAnimation = "WalkUp";
                                    }

                                    if (!curIA.Animate)
                                    {
                                        curIA.Animate = true;
                                    }

                                    break;
                            }
                        }
                    }

                    // Sleeping a little to let other threads do their jobs
                    System.Threading.Thread.Sleep(10);
                }
            });
        }

        /// <summary>
        /// Finalizes an instance of the FilsDeBerger class.
        /// </summary>
        ~FilsDeBerger()
        {
            this.Dispose(false);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Main sub of software
        /// </summary>
        [STAThread]
        public static void Main()
        {
            // Start the application
            FilsDeBerger app = new FilsDeBerger();
            app.Go();
        }

        /// <summary>
        /// End class life
        /// </summary>
        public void Close()
        {
            this.Dispose();
        }

        /// <summary>
        /// Execute task defined by the software linked to freeing or redefining of unmanaged ressources
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Launch the SDL Events loop
        /// </summary>
        public void Go()
        {
            // Setup the SDL.NET events
            Events.Fps = 50;
            Events.Tick += new EventHandler<TickEventArgs>(this.Events_Tick);
            Events.Quit += new EventHandler<QuitEventArgs>(this.Events_Quit);
            Events.KeyboardDown += new EventHandler<KeyboardEventArgs>(this.Events_KeyboardDown);
            Events.KeyboardUp += new EventHandler<KeyboardEventArgs>(this.Events_KeyboardUp);
            this.timer.Start();
            Events.Run();
        }

        /// <summary>
        /// Execute task defined by the software linked to freeing or redefining of unmanaged ressources
        /// </summary>
        /// <param name="disposing">Define if dispose of linked objects should occurs</param>
        protected virtual void Dispose(bool disposing)
        {
            lock (this.threadLocker)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        for (int i = 0; i < this.characters.GetLength(0); i++)
                        {
                            if (this.characters[i] != null)
                            {
                                this.characters[i].Dispose();
                                this.characters[i] = null;
                            }
                        }
                    }

                    this.disposed = true;
                }
            }
        }

        /// <summary>
        /// Event handler for the KeyboardDown event
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Parameters of the event</param>
        private void Events_KeyboardDown(object sender, KeyboardEventArgs e)
        {
            // Check which key was pressed and change the animation accordingly
            Character hero = Array.Find(
                this.characters,
                delegate(Character toCheck)
                {
                    return toCheck.Control == Controller.Player;
                });
            if (hero != null)
            {
                switch (e.Key)
                {
                    case Key.LeftArrow:
                        hero.CurrentAnimation = "WalkLeft";
                        hero.Animate = true;
                        break;
                    case Key.RightArrow:
                        hero.CurrentAnimation = "WalkRight";
                        hero.Animate = true;
                        break;
                    case Key.DownArrow:
                        hero.CurrentAnimation = "WalkDown";
                        hero.Animate = true;
                        break;
                    case Key.UpArrow:
                        hero.CurrentAnimation = "WalkUp";
                        hero.Animate = true;
                        break;
                    case Key.Tab:
                        Character notConrolledHero = Array.Find(
                            this.characters,
                            delegate(Character toCheck)
                            {
                                return toCheck.Control == Controller.AltPlayer;
                            });

                        if (notConrolledHero != null)
                        {
                            notConrolledHero.Control = Controller.Player;
                            hero.Control = Controller.AltPlayer;
                        }

                        break;
                    case Key.Escape:
                    case Key.Q:
                        Events.QuitApplication();
                        break;
                }
            }
        }

        /// <summary>
        /// Event handler for the KeyboardUp event
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Parameters of the event</param>
        private void Events_KeyboardUp(object sender, KeyboardEventArgs e)
        {
            Character hero = Array.Find(
                this.characters,
                delegate(Character toCheck)
                {
                    return toCheck.Control == Controller.Player;
                });

            if (hero != null)
            {
                // Check which key was brought up and stop the hero if needed
                if (e.Key == Key.LeftArrow && hero.CurrentAnimation == "WalkLeft")
                {
                    hero.Animate = false;
                }
                else if (e.Key == Key.UpArrow && hero.CurrentAnimation == "WalkUp")
                {
                    hero.Animate = false;
                }
                else if (e.Key == Key.DownArrow && hero.CurrentAnimation == "WalkDown")
                {
                    hero.Animate = false;
                }
                else if (e.Key == Key.RightArrow && hero.CurrentAnimation == "WalkRight")
                {
                    hero.Animate = false;
                }
            }
        }

        /// <summary>
        /// Event handler for the Quit event
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Parameters of the event</param>
        private void Events_Quit(object sender, QuitEventArgs e)
        {
            Events.QuitApplication();
        }

        /// <summary>
        /// Event handler for tick events
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Parameters of the event</param>
        private void Events_Tick(object sender, TickEventArgs e)
        {
            // Clear the screen, draw the hero and output to the window
            Video.Screen.Fill(Color.DarkGreen);
            Video.Screen.Blit(this.backGround);

            // Add a victory area, should be tall as 50% of screen and large as 5% of screen
            Video.Screen.Blit(
                this.victoryArea,
                this.victoryArea.UpperLeft);

            try
            {
                foreach (Character hero in this.characters)
                {
                    Video.Screen.Blit(hero);
                }
            }
            catch (System.ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }

            // Check winning Condition
            SDL.Sheep[] sheeps = Array.ConvertAll(
                Array.FindAll(
                    this.characters,
                    delegate(Character toCheck)
                    {
                        return toCheck.GetType().Equals(typeof(SDL.Sheep));
                    }),
                delegate(Character target)
                {
                    return target as Sheep;
                });

            TextSprite timerText = new TextSprite(string.Format("{0:00}'{1:00}''", Math.Floor(this.timer.Elapsed.TotalMinutes), this.timer.Elapsed.Seconds), new SdlDotNet.Graphics.Font(System.IO.Path.Combine(@"Graphics\Ttf", "comicbd.ttf"), 14), Color.GhostWhite);
            Video.Screen.Blit(
                    timerText,
                    new Point(1, 1));

            if (Array.TrueForAll(
                sheeps,
                delegate(Sheep toCheck)
                {
                    return toCheck.Safe;
                }))
            {
                this.timer.Stop();
                TextSprite victoryText = new TextSprite("Victory !", new SdlDotNet.Graphics.Font(System.IO.Path.Combine(@"Graphics\Ttf", "comicbd.ttf"), 32), Color.GhostWhite);
                TextSprite resumeOfVictory = new TextSprite(
                    string.Format(
                        "It tooks you {0} minutes and {1} seconds to store your sheep",
                        Math.Floor(this.timer.Elapsed.TotalMinutes),
                        this.timer.Elapsed.Seconds),
                    new SdlDotNet.Graphics.Font(
                        System.IO.Path.Combine(@"Graphics\Ttf", "comicbd.ttf"), 
                        16), 
                    Color.GhostWhite);
                Video.Screen.Blit(
                    victoryText,
                    new Point(
                        (Video.Screen.Width - victoryText.Width) / 2,
                        (Video.Screen.Height - victoryText.Height - resumeOfVictory.Height) / 2));
                Video.Screen.Blit(
                    resumeOfVictory,
                    new Point(
                        (Video.Screen.Width - resumeOfVictory.Width) / 2,
                        ((Video.Screen.Height - victoryText.Height - resumeOfVictory.Height) / 2) + victoryText.Height));
            }

            Video.Screen.Update();

            // If the hero is animated, he is walking, so move him around!
            foreach (Character hero in this.characters)
            {
                if (hero.Animate)
                {
                    switch (hero.CurrentAnimation)
                    {
                        case "WalkLeft":
                            // 2 is the speed of the hero when walking.
                            hero.X -= hero.Speed;
                            break;
                        case "WalkUp":
                            hero.Y -= hero.Speed;
                            break;
                        case "WalkDown":
                            hero.Y += hero.Speed;
                            break;
                        case "WalkRight":
                            hero.X += hero.Speed;
                            break;
                    }
                }

                // Setting winning condition
                if (hero.GetType().Equals(typeof(SDL.Sheep)))
                {
                    // We have a sheep, let's check if it has been saved
                    if (hero.IntersectsWith(this.victoryArea.Area))
                    {
                        SDL.Sheep curSheep = hero as SDL.Sheep;
                        curSheep.Safe = true;
                        curSheep = null;
                    }
                }
            }
        }

        #endregion Methods
    }
}