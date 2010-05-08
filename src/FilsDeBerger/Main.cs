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
            // Start up the window
            Video.WindowIcon();
            Video.WindowCaption = "Fils de Berger";
            Video.SetVideoMode(800, 600);

            Character.Path = @"Graphics\Charset";
            this.characters = new Character[2];
            this.characters[0] = new Character("shepherd.png");
            this.characters[0].Speed = 2;
            this.characters[1] = new Character("sheep.png");
            this.characters[1].Speed = 1;
            foreach (Character hero in this.characters)
            {
                hero.Animate = false;
            }

            // Put him in the center of the screen
            this.characters[0].Center = new Point(
                Video.Screen.Width / 2,
                Video.Screen.Height / 2);

            this.characters[1].Center = new Point(
                (Video.Screen.Width / 2) + 50,
                Video.Screen.Height / 2);
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
            switch (e.Key)
            {
                case Key.LeftArrow:
                    foreach (Character hero in this.characters)
                    {
                        hero.CurrentAnimation = "WalkLeft";
                        hero.Animate = true;
                    }

                    break;
                case Key.RightArrow:
                    foreach (Character hero in this.characters)
                    {
                        hero.CurrentAnimation = "WalkRight";
                        hero.Animate = true;
                    }

                    break;
                case Key.DownArrow:
                    foreach (Character hero in this.characters)
                    {
                        hero.CurrentAnimation = "WalkDown";
                        hero.Animate = true;
                    }

                    break;
                case Key.UpArrow:
                    foreach (Character hero in this.characters)
                    {
                        hero.CurrentAnimation = "WalkUp";
                        hero.Animate = true;
                    }

                    break;
                case Key.Escape:
                case Key.Q:
                    Events.QuitApplication();
                    break;
            }
        }

        /// <summary>
        /// Event handler for the KeyboardUp event
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Parameters of the event</param>
        private void Events_KeyboardUp(object sender, KeyboardEventArgs e)
        {
            // Check which key was brought up and stop the hero if needed
            if (e.Key == Key.LeftArrow && this.characters[0].CurrentAnimation == "WalkLeft")
            {
                foreach (Character hero in this.characters)
                {
                    hero.Animate = false;
                }
            }
            else if (e.Key == Key.UpArrow && this.characters[0].CurrentAnimation == "WalkUp")
            {
                foreach (Character hero in this.characters)
                {
                    hero.Animate = false;
                }
            }
            else if (e.Key == Key.DownArrow && this.characters[0].CurrentAnimation == "WalkDown")
            {
                foreach (Character hero in this.characters)
                {
                    hero.Animate = false;
                }
            }
            else if (e.Key == Key.RightArrow && this.characters[0].CurrentAnimation == "WalkRight")
            {
                foreach (Character hero in this.characters)
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