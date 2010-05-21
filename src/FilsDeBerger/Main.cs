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
        /// Indicates whether object have been disposed or not
        /// </summary>
        private bool disposed;

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

            Character.Path = @"Graphics\Charset";
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

            // Initialize the thinking of IA
            System.Threading.ThreadPool.QueueUserWorkItem(delegate(object notUsed)
            {
                while (true)
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
            Events.Run();
        }

        /// <summary>
        /// Execute task defined by the software linked to freeing or redefining of unmanaged ressources
        /// </summary>
        /// <param name="disposing">Define if dispose of linked objects should occurs</param>
        protected virtual void Dispose(bool disposing)
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
            }
        }

        #endregion Methods
    }
}