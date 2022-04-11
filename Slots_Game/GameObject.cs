using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Slots_Game
{
    public abstract class GameObject
    {
        protected Color color;


        //Converts a hex code to a Raylib Color, with the option to make it darker with an int
        public static Color GetCol(string hex, int dark)
        {
            int r = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int g = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int b = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            r -= dark;
            g -= dark;
            b -= dark;
            if (r < 0)
            {
                r = 0;
            }
            if (g < 0)
            {
                g = 0;
            }
            if (b < 0)
            {
                b = 0;
            }
            return new Color(r, g, b, 255);
        }
    }
}