using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Slots_Game
{
    public class Slot
    {
        Vector2 size = new Vector2(280, 240);
        int index;
        int pay;
        Color color = Color.YELLOW;
        Random gen = new Random();

        
        public Slot()
        {
            List<Color> colors = new List<Color>()
            {
                Color.RED,
                Color.ORANGE,
                Color.YELLOW,
                Color.GREEN,
                Color.BLUE,
                Color.PURPLE
            };
            index = gen.Next(0, 5);
            color = colors[index];
        }

        public void Draw(int x, int y)
        {
            Raylib.DrawRectangle(x, y, (int)size.X, (int)size.Y, color);
        }
    }
}