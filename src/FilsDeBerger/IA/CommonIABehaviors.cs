using System;
using System.Collections.Generic;
using System.Text;

namespace FilsDeBerger.IA
{
    public static class CommonIABehaviors
    {
        public static global::FilsDeBerger.SDL.MoveDirection FleeCharacters(SDL.Character curChar, int fleeDistance, SDL.Character[] charsToFlee)
        {
            SortTableOnNearestDistance(curChar, charsToFlee);

            if (charsToFlee.GetLength(0) > 0)
            {
                if (curChar.GetDistance(charsToFlee[0]) < fleeDistance)
                {
                    // We will think of which direction to take as we fear player controlled Character
                    if (Math.Abs(curChar.Position.X - charsToFlee[0].Position.X) >
                        Math.Abs(curChar.Position.Y - charsToFlee[0].Position.Y))
                    {
                        // Then it will be right or left
                        if (curChar.Position.X < charsToFlee[0].Position.X)
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
                        if (curChar.Position.Y < charsToFlee[0].Position.Y)
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
                    // We are not in the danger zone, let's just try to eat something
                    return global::FilsDeBerger.SDL.MoveDirection.Stop;
                }
            }
            else
            {
                // There is a problem, stop the IA
                return global::FilsDeBerger.SDL.MoveDirection.Stop;
            }
        }

        public static void SortTableOnNearestDistance(SDL.Character curChar, SDL.Character[] charsToFlee)
        {
            // 2. Find the nearest player
            Array.Sort<SDL.Character>(
                charsToFlee,
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
        }

    }
}
