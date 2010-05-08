using System;
using System.IO;
using System.Drawing;

using SdlDotNet;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Sprites;
using SdlDotNet.Core;
using SdlDotNet.Input;

namespace FilsDeBerger.SDL
{
    public class Character : AnimatedSprite
    {
        public static string Path
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }

        public Character(string fileName)
        {
            this.FileName = fileName;
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

        }
    }
}
