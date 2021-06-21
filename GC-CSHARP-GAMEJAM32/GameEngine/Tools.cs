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
        /**
         * Retourne un nombre entier compris entre les valeurs min et max passées en paramètre
         */
        public static int RandomInt(int pMin, int pMax)
        {
            return randomGen.Next(pMin, pMax + 1);
        }

        /**
         * Initialise la graine() de la fonction Random
         */
        public static void SetRandomSeed(int pSeed)
        {
            randomGen = new Random(pSeed);
        }

        /**
         * Retourne en fonction du paramètre la largeyr ou la longeur de la fenêtre du jeu
         */
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

        /**
         * Détection de collision entre deux box
         */
        public static bool CollideByBox(IActor pBox1, IActor pBox2)
        {
            return pBox1.boundingBox.Intersects(pBox2.boundingBox);
        }

        /**
         * Retourne la distance entre deux points 
         */
        public static double MathDist(double x1, double y1, double x2, double y2)
        {
            return Math.Pow(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2), 0.5);
        }
        /**
         * Retourne l'angle entre deux points
         */
        public static double MathAngle(double x1, double y1, double x2, double y2)
        {
            return Math.Atan2(y2 - y1, x2 - x1);
        }
    }
}

