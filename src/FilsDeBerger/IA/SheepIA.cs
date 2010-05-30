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
                //Is sheep safe or not, if safe, run out of the screen
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
                        SDL.Character[] playerControlled = Array.FindAll(
                            allChar,
                            delegate(SDL.Character toCheck)
                            {
                                return toCheck.Control == global::FilsDeBerger.SDL.Controller.Player || toCheck.Control == global::FilsDeBerger.SDL.Controller.AltPlayer;
                            });

                        // 2. Find the nearest player
                        Array.Sort<SDL.Character>(
                            playerControlled,
                            delegate(SDL.Character a, SDL.Character b)
                            {
                                if (a != b)
                                {
                                    return curChar.GetDistance(a).CompareTo(curChar.GetDistance(b));
                                }
                                else
                                {
                                    return 0;
                                }
                            });
                        if (playerControlled.GetLength(0) > 0)
                        {
                            if (curChar.GetDistance(playerControlled[0]) < 100)
                            {
                                // We will think of which direction to take as we fear player controlled Character
                                if (Math.Abs(curChar.Position.X - playerControlled[0].Position.X) >
                                    Math.Abs(curChar.Position.Y - playerControlled[0].Position.Y))
                                {
                                    // Then it will be right or left
                                    if (curChar.Position.X < playerControlled[0].Position.X)
                                    {
                                        // We are at the left of player controlled character, continue to the left
                                        return global::FilsDeBerger.SDL.MoveDirection.Left;
                                    }
                                    else
                                    {
                                        // We are at the right of player controlled character, continue to the right
                                        return global::FilsDeBerger.SDL.MoveDirection.Right;
                                    }
                                }
                                else
                                {
                                    // It will be up or down
                                    if (curChar.Position.Y < playerControlled[0].Position.Y)
                                    {
                                        // We are upper to player controlled character, continue to move up
                                        return global::FilsDeBerger.SDL.MoveDirection.Up;
                                    }
                                    else
                                    {
                                        // We are bottom to player controlled character, continue to move down
                                        return global::FilsDeBerger.SDL.MoveDirection.Down;
                                    }
                                }
                            }
                            else
                            {
                                // We are not in the danger zone, let's just do something stupid
                                if (t.NextDouble() > .9)
                                {
                                    // Choose a new direction
                                    return (SDL.MoveDirection)t.Next(5);
                                }
                                else
                                {
                                    if (t.NextDouble() > .7)
                                    {
                                        // Just stop the sheep
                                        return global::FilsDeBerger.SDL.MoveDirection.Stop;
                                    }
                                    else
                                    {
                                        // We just continue in the same direction
                                        switch (curChar.CurrentAnimation)
                                        {
                                            case "WalkUp":
                                                return global::FilsDeBerger.SDL.MoveDirection.Up;
                                            case "WalkDown":
                                                return global::FilsDeBerger.SDL.MoveDirection.Down;
                                            case "WalkLeft":
                                                return global::FilsDeBerger.SDL.MoveDirection.Left;
                                            case "WalkRight":
                                                return global::FilsDeBerger.SDL.MoveDirection.Right;
                                            default:
                                                return global::FilsDeBerger.SDL.MoveDirection.Stop;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            // There is a problem, stop the IA
                            return global::FilsDeBerger.SDL.MoveDirection.Stop;
                        }
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