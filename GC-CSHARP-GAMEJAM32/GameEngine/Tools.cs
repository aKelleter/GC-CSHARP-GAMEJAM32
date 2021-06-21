using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AlphaKilo_GameJam32
{
    class Tools
    {
        static Random randomGen = new Random();

        public enum SizeScreenType
        {
            Width,
            Height
        }
        public static int RandomInt(int pMin, int pMax)
        {
            return randomGen.Next(pMin, pMax + 1);
        }

        public static void SetRandomSeed(int pSeed)
        {
            randomGen = new Random(pSeed);
        }

        public static int ScreenSize(Rectangle pScreen, SizeScreenType pType)
        {
            int size = 1;

            switch (pType)
            {
                case SizeScreenType.Width:
                    size = pScreen.Width;
                    break;
                case SizeScreenType.Height:
                    size = pScreen.Height;
                    break;
                default:
                    break;
            }

            return size;
        }

        public static bool CollideByBox(IActor pBox1, IActor pBox2)
        {
            return pBox1.boundingBox.Intersects(pBox2.boundingBox);
        }
    }
}
