using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Slots_Game
{
    public class Slot
    {
        Vector2 size = new Vector2(280, 240);
        public Vector2 Pos {get; set;}
        int index;
        int speed = 800;
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
                Color.SKYBLUE,
                Color.PURPLE
            };
            index = gen.Next(0, 5);
            color = colors[index];
        }

        public void Draw()
        {
            Raylib.DrawRectangle((int)Pos.X, (int)Pos.Y, (int)size.X, (int)size.Y, color);
        }

        public void Move(float delta)
        {
            Pos = new Vector2(Pos.X, Pos.Y + (speed * delta));
        }
    }
}