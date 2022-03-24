using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


namespace Slots_Game
{
    public class Payline
    {
        public Slot[] Line {get; set;}
        public int WinLenght {get; set;}
        public bool Won {get; set;}

        Color trueColor;
        Color darkColor;
        Color midColor;
        Color color;
        int thickness;
        int xOffset;
        int yOffset;
        
        //Color thickness of lines (for 3D-effect)
        int trueThickness = 16;
        int midThickness = 12;
        int centerThickness = 6;

        public Payline(string col, int x, int y)
        {
            trueColor = GetCol(col, 0);
            midColor = GetCol(col, 22);
            darkColor = GetCol(col, 60);
            xOffset = x * 5;
            yOffset = y * 18;
        }

        //Draws a payline. In reality, this is done three times for every paylien with varying thicness and color to create a 3D-effect
        public void Draw(int brightness)
        {
            switch (brightness)
            {
                case 0:
                    thickness = trueThickness;
                    color = darkColor;
                    break;
                case 1:
                    thickness = midThickness;
                    color = midColor;
                    break;
                case 2:
                    thickness = centerThickness;
                    color = trueColor;
                    break;
            }
            //Draws the first part of the payline
            Vector2 startFirst = new Vector2(Line[0].Pos.X, Line[0].Pos.Y + 120 - (trueThickness / 2) + yOffset);
            Vector2 endFirst = new Vector2(Line[0].Pos.X + 140 + xOffset, Line[0].Pos.Y + 120 - (trueThickness / 2) + yOffset);
            Raylib.DrawLineEx(startFirst, endFirst, thickness, color);

            //Draws all parts of the payline, except start and finish
            for (int i = 0; i < 4; i++)
            {
                Vector2 start = new Vector2(Line[i].Pos.X + 140 + xOffset, Line[i].Pos.Y + 120 - (trueThickness / 2) + yOffset);
                Vector2 end = new Vector2(Line[i + 1].Pos.X + 140 + xOffset, Line[i + 1].Pos.Y + 120 - (trueThickness / 2) + yOffset);
                Raylib.DrawLineEx(start, end, thickness, color);
                Raylib.DrawLineEx(start, end, thickness, color);
                Raylib.DrawCircleV(start, thickness / 2, color);
            }

            //Draws the final part of the payline
            Vector2 startFinal = new Vector2(Line[4].Pos.X + 140 + xOffset, Line[4].Pos.Y + 120 - (trueThickness / 2) + yOffset);
            Vector2 endFinal = new Vector2(Line[4].Pos.X + 280, Line[4].Pos.Y + 120 - (trueThickness / 2) + yOffset);
            Raylib.DrawLineEx(startFinal, endFinal, thickness, color);
            Raylib.DrawCircleV(startFinal, thickness / 2, color);
        }

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