#region LICENSE
/*
 * Copyright (C) 2005 Rob Loach (http://www.robloach.net)
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */
#endregion LICENSE

using System;
using System.IO;
using System.Drawing;

using SdlDotNet;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Sprites;
using SdlDotNet.Core;
using SdlDotNet.Input;
using FilsDeBerger.SDL;

namespace SdlDotNetExamples.SmallDemos
{
    public class FilsDeBerger : IDisposable
    {
        // List of characters to move in the world
        private Character[] characters; 

        [STAThread]
        public static void Main()
        {
            // Start the application
            FilsDeBerger app = new FilsDeBerger();
            app.Go();
        }

        public void Go()
        {
            // Setup the SDL.NET events
            Events.Fps = 50;
            Events.Tick += new EventHandler<TickEventArgs>(Events_Tick);
            Events.Quit += new EventHandler<QuitEventArgs>(Events_Quit);
            Events.KeyboardDown += new EventHandler<KeyboardEventArgs>(Events_KeyboardDown);
            Events.KeyboardUp += new EventHandler<KeyboardEventArgs>(Events_KeyboardUp);
            Events.Run();
        }

        public FilsDeBerger()
        {
            // Start up the window
            Video.WindowIcon();
            Video.WindowCaption = "Fils de Berger";
            Video.SetVideoMode(800, 600);

            Character.Path = @"Graphics\Charset";
            characters = new Character[2];
            characters[0] = new Character("shepherd.png");
            characters[0].Speed = 2;
            characters[1] = new Character("sheep.png");
            characters[1].Speed = 1;
            foreach (Character hero in characters)
            {
                hero.Animate = false;
            }

            // Put him in the center of the screen
            characters[0].Center = new Point(
                Video.Screen.Width / 2,
                Video.Screen.Height / 2);

            characters[1].Center = new Point(
                Video.Screen.Width / 2 + 50,
                Video.Screen.Height / 2);
        }

        private void Events_Tick(object sender, TickEventArgs e)
        {

            // Clear the screen, draw the hero and output to the window
            Video.Screen.Fill(Color.DarkGreen);
            try
            {
                foreach (Character hero in characters)
                {
                    Video.Screen.Blit(hero);
                }
                //hero.Render(Video.Screen);
            }
            catch (System.ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
            Video.Screen.Update();


            // If the hero is animated, he is walking, so move him around!
            foreach (Character hero in characters)
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

        private void Events_KeyboardDown(object sender, KeyboardEventArgs e)
        {
            // Check which key was pressed and change the animation accordingly
            switch (e.Key)
            {
                case Key.LeftArrow:
                    foreach (Character hero in characters)
                    {
                        hero.CurrentAnimation = "WalkLeft";
                        hero.Animate = true;
                    }

                    break;
                case Key.RightArrow:
                    foreach (Character hero in characters)
                    {
                        hero.CurrentAnimation = "WalkRight";
                        hero.Animate = true;
                    }

                    break;
                case Key.DownArrow:
                    foreach (Character hero in characters)
                    {
                        hero.CurrentAnimation = "WalkDown";
                        hero.Animate = true;
                    }

                    break;
                case Key.UpArrow:
                    foreach (Character hero in characters)
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

        private void Events_KeyboardUp(object sender, KeyboardEventArgs e)
        {
            // Check which key was brought up and stop the hero if needed
            if (e.Key == Key.LeftArrow && characters[0].CurrentAnimation == "WalkLeft")
            {
                foreach (Character hero in characters)
                {
                    hero.Animate = false;
                }
            }
            else if (e.Key == Key.UpArrow && characters[0].CurrentAnimation == "WalkUp")
            {
                foreach (Character hero in characters)
                {
                    hero.Animate = false;
                }
            }
            else if (e.Key == Key.DownArrow && characters[0].CurrentAnimation == "WalkDown")
            {
                foreach (Character hero in characters)
                {
                    hero.Animate = false;
                }
            }
            else if (e.Key == Key.RightArrow && characters[0].CurrentAnimation == "WalkRight")
            {
                foreach (Character hero in characters)
                {
                    hero.Animate = false;
                }
            }
        }
        private void Events_Quit(object sender, QuitEventArgs e)
        {
            Events.QuitApplication();
        }

        /// <summary>
        /// Lesson Title
        /// </summary>
        public static string Title
        {
            get
            {
                return "HeroExample: Simple animation";
            }
        }

        #region IDisposable Members

        private bool disposed;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    for(int i= 0;i<characters.GetLength(0);i++)
                    {
                        if (characters[i] != null)
                        {
                            characters[i].Dispose();
                            characters[i] = null;
                        }
                    }
                }
                this.disposed = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        ~FilsDeBerger()
        {
            Dispose(false);
        }

        #endregion
    }
}