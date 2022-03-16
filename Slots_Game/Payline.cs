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

        Color color = Color.BLACK;
        int thickness = 12;
        int xOffset;
        int yOffset;


        public Payline(Color col, int x, int y)
        {
            color = col;
            xOffset = x * 5;
            yOffset = y * 18;
        }

        public void Draw()
        {
            Vector2 startFirst = new Vector2(Line[0].Pos.X, Line[0].Pos.Y + 120 - (thickness / 2) + yOffset);
            Vector2 endFirst = new Vector2(Line[0].Pos.X + 140 + xOffset, Line[0].Pos.Y + 120 - (thickness / 2) + yOffset);
            Raylib.DrawLineEx(startFirst, endFirst, thickness, color);

            for (int i = 0; i < 4; i++)
            {
                Vector2 start = new Vector2(Line[i].Pos.X + 140 + xOffset, Line[i].Pos.Y + 120 - (thickness / 2) + yOffset);
                Vector2 end = new Vector2(Line[i + 1].Pos.X + 140 + xOffset, Line[i + 1].Pos.Y + 120 - (thickness / 2) + yOffset);
                Raylib.DrawLineEx(start, end, thickness, color);
                Raylib.DrawCircleV(start, thickness / 2, color);
            }

            Vector2 startFinal = new Vector2(Line[4].Pos.X + 140 + xOffset, Line[4].Pos.Y + 120 - (thickness / 2) + yOffset);
            Vector2 endFinal = new Vector2(Line[4].Pos.X + 280, Line[4].Pos.Y + 120 - (thickness / 2) + yOffset);
            Raylib.DrawLineEx(startFinal, endFinal, thickness, color);
            Raylib.DrawCircleV(startFinal, thickness / 2, color);
        }
    }

}