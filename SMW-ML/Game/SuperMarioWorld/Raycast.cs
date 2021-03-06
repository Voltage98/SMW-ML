using SMW_ML.Utils;
using System;
using System.Collections.Generic;

namespace SMW_ML.Game.SuperMarioWorld
{
    internal static class Raycast
    {
        public const int OUTPUT_HEIGHT = 4;
        public static readonly int[] POSSIBLE_RAY_COUNT = new int[] { 4, 8, 16, 32, 64 };

        private static readonly Dictionary<int, (double dirX, double dirY)[]> precomputedRays;

        static Raycast()
        {
            precomputedRays = new();

            foreach (int rayCount in POSSIBLE_RAY_COUNT)
            {
                var rays = new (double x, double y)[rayCount];

                double mulRad = Math.Tau * 1.0 / rayCount;
                for (int i = 0; i < rayCount; i++)
                {
                    (double sin, double cos) = Math.SinCos(mulRad * i);
                    // We invert the cos, since a negative value actually is up.
                    rays[i] = (Math.Round(sin, 4), Math.Round(-cos, 4));
                }

                precomputedRays[rayCount] = rays;
            }
        }

        public static double[,] GetRayDistances(bool[,] tiles, int rayRadius, int rayCount)
        {
            double[,] distances = new double[OUTPUT_HEIGHT, rayCount / OUTPUT_HEIGHT];
            int raysPerRow = rayCount / OUTPUT_HEIGHT;
            for (int i = 0; i < OUTPUT_HEIGHT; i++)
            {
                for (int j = 0; j < raysPerRow; j++)
                {
                    distances[i, j] = CastRay(tiles, rayRadius, precomputedRays[rayCount][i * raysPerRow + j]);
                }
            }

            return distances;
        }

        private static double CastRay(bool[,] tiles, int rayRadius, (double dirX, double dirY) dirr)
        {
            int incX = dirr.dirX > 0 ? 1 : -1;
            int incY = dirr.dirY > 0 ? 1 : -1;

            double distX = Math.Round(dirr.dirX * rayRadius, 2);
            int distXInt = 0;
            if (distX > 0) distXInt = (int)Math.Ceiling(distX);
            if (distX < 0) distXInt = (int)Math.Floor(distX);
            double distY = Math.Round(dirr.dirY * rayRadius, 2);
            int distYInt = 0;
            if (distY > 0) distYInt = (int)Math.Ceiling(distY);
            if (distY < 0) distYInt = (int)Math.Floor(distY);

            int deltaX = Math.Abs(distXInt);
            int deltaY = Math.Abs(distYInt);

            int currX = rayRadius;
            int currY = rayRadius;

            int difference = deltaX - deltaY;

            for (int i = deltaX + deltaY; i > 0; i--)
            {
                if (difference >= 0)
                {
                    currX += incX;
                    difference -= deltaY * 2;
                }
                else
                {
                    currY += incY;
                    difference += deltaX * 2;
                }

                //Minus 1 before the square root, since the minimum squared distance is 1, we want to center on 0.
                if (tiles[currY, currX]) return 1 - (((currX - rayRadius).Squared() + (currY - rayRadius).Squared() - 1).ApproximateSquareRoot() / rayRadius);
            }

            return 0;
        }
    }
}
