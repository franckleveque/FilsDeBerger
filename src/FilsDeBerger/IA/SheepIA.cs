namespace FilsDeBerger.IA
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Class responsible for managing the artificial inteligence of sheeps
    /// </summary>
    public static class SheepIA
    {
        #region Fields

        /// <summary>
        /// Randomizer of number for Sheep thinking
        /// </summary>
        private static Random t = new Random();

        #endregion Fields

        /// <summary>
        /// Gets or sets screen size used by sheep to be kept in screen
        /// </summary>
        public static System.Drawing.Size ScreenSize
        {
            get;
            set;
        }

        #region Methods

        /// <summary>
        /// Method responsible of sheep movement
        /// </summary>
        /// <param name="curChar">Current sheep to move</param>
        /// <param name="allChar">Table of all characters of the game</param>
        /// <returns>Direction in which the sheep will move</returns>
        public static SDL.MoveDirection Think(SDL.Character curChar, SDL.Character[] allChar)
        {
            if (!curChar.Disposed)
            {
                // Is sheep safe or not, if safe, run out of the screen
                SDL.Sheep curSheep = curChar as SDL.Sheep;
                if (curSheep.Safe)
                {
                    if (curSheep.X < ScreenSize.Width)
                    {
                        return global::FilsDeBerger.SDL.MoveDirection.Right;
                    }
                    else
                    {
                        return global::FilsDeBerger.SDL.MoveDirection.Stop;
                    }
                }
                else
                {
                    // First check we are in screen
                    if (curChar.X <= 1)
                    {
                        return global::FilsDeBerger.SDL.MoveDirection.Right;
                    }
                    else if (curChar.X >= ScreenSize.Width - curChar.Width)
                    {
                        return global::FilsDeBerger.SDL.MoveDirection.Left;
                    }
                    else if (curChar.Y <= 1)
                    {
                        return global::FilsDeBerger.SDL.MoveDirection.Down;
                    }
                    else if (curChar.Y >= ScreenSize.Height - curChar.Height)
                    {
                        return global::FilsDeBerger.SDL.MoveDirection.Up;
                    }
                    else
                    {
                        // Is sheep near one of the player controlled objects
                        // 1. Find the player controlled characters
                        SDL.Character[] charactersToFlee = Array.FindAll(
                            allChar,
                            delegate(SDL.Character toCheck)
                            {
                                return toCheck.Control == global::FilsDeBerger.SDL.Controller.Player || toCheck.Control == global::FilsDeBerger.SDL.Controller.AltPlayer || toCheck.Eatable == false;
                            });
                        SDL.MoveDirection result = CommonIABehaviors.FleeCharacters(curChar, 100, charactersToFlee);
                        if (result == global::FilsDeBerger.SDL.MoveDirection.Stop)
                        {
                            // We are not in the danger zone, let's just do something stupid
                            if (t.NextDouble() > .9)
                            {
                                // Choose a new direction
                                result = (SDL.MoveDirection)t.Next(5);
                            }
                            else
                            {
                                if (t.NextDouble() > .7)
                                {
                                    // Just stop the sheep
                                    result = global::FilsDeBerger.SDL.MoveDirection.Stop;
                                }
                                else
                                {
                                    // We just continue in the same direction
                                    switch (curChar.CurrentAnimation)
                                    {
                                        case "WalkUp":
                                            result = global::FilsDeBerger.SDL.MoveDirection.Up;
                                            break;
                                        case "WalkDown":
                                            result = global::FilsDeBerger.SDL.MoveDirection.Down;
                                            break;
                                        case "WalkLeft":
                                            result = global::FilsDeBerger.SDL.MoveDirection.Left;
                                            break;
                                        case "WalkRight":
                                            result = global::FilsDeBerger.SDL.MoveDirection.Right;
                                            break;
                                        default:
                                            result = global::FilsDeBerger.SDL.MoveDirection.Stop;
                                            break;
                                    }
                                }
                            }
                        }

                        return result;
                    }
                }
            }
            else
            {
                return global::FilsDeBerger.SDL.MoveDirection.Stop;
            }
        }

        #endregion Methods
    }
}