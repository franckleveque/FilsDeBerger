using System;
using System.Collections.Generic;
using System.Text;

namespace FilsDeBerger.IA
{
    public static class WolfIA
    {
        /// <summary>
        /// Method responsible of sheep movement
        /// </summary>
        /// <param name="curChar">Current sheep to move</param>
        /// <param name="allChar">Table of all characters of the game</param>
        /// <returns>Direction in which the sheep will move</returns>
        public static SDL.MoveDirection Think(SDL.Character curChar, SDL.Character[] allChar)
        {
            SDL.MoveDirection result = global::FilsDeBerger.SDL.MoveDirection.Stop;
            if (!curChar.Disposed)
            {
                // First check if we are near characters which are not eatable
                SDL.Character[] unEatableChars = Array.FindAll(
                            allChar,
                            delegate(SDL.Character toCheck)
                            {
                                return toCheck.Eatable == false && toCheck != curChar;
                            });
                result = CommonIABehaviors.FleeCharacters(curChar, 120, unEatableChars);
                if (result == global::FilsDeBerger.SDL.MoveDirection.Stop)
                {
                    // We have no ennemies nearly, let's try to eat something
                    // First check if we are near characters which are not eatable
                    SDL.Character[] eatableChars = Array.FindAll(
                                allChar,
                                delegate(SDL.Character toCheck)
                                {
                                    return toCheck.Eatable;
                                });

                    CommonIABehaviors.SortTableOnNearestDistance(curChar, eatableChars);
                    if (eatableChars.GetLength(0) > 0)
                    {
                        // We will think of which direction to take as we want to eat
                        if (Math.Abs(curChar.Position.X - eatableChars[0].Position.X) > 0)
                            //Math.Abs(curChar.Position.Y - eatableChars[0].Position.Y))
                        {
                            // Then it will be right or left
                            if (curChar.Position.X < eatableChars[0].Position.X)
                            {
                                // We are at the left of the meal lest continue to right
                                return global::FilsDeBerger.SDL.MoveDirection.Right;
                            }
                            else
                            {
                                // We are at the right of the meal lest continue to left
                                return global::FilsDeBerger.SDL.MoveDirection.Left;
                            }
                        }
                        else
                        {
                            // It will be up or down
                            if (curChar.Position.Y < eatableChars[0].Position.Y)
                            {
                                // We are up of the meal lest continue to bottom
                                return global::FilsDeBerger.SDL.MoveDirection.Down;
                            }
                            else
                            {
                                // We are bottom of the meal lest continue to move up
                                return global::FilsDeBerger.SDL.MoveDirection.Up;
                            }
                        }
                    }
                    else
                    {
                        // There's no eatable things, let's stop
                        return global::FilsDeBerger.SDL.MoveDirection.Stop;
                    }

                }

                return result;
            }
            else
            {
                return global::FilsDeBerger.SDL.MoveDirection.Stop;
            }
        }
    }
}
