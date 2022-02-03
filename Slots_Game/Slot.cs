using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Slots_Game
{
    public class Slot
    {
        public int Index {get; set;}
        public Vector2 Pos {get; set;}
        Vector2 size = new Vector2(280, 240);
        int speed = 500;
        Color color = Color.YELLOW;
        Random gen = new Random();

        
        public Slot(int i)
        {
            List<Color> colors = new List<Color>()
            {
                Color.PINK,
                Color.ORANGE,
                Color.YELLOW,
                Color.GREEN,
                Color.SKYBLUE,
                Color.PURPLE
            };
            Index = gen.Next(0, 5);
            Index = i;
            color = colors[Index];
        }

        public Slot()
        {
            List<Color> colors = new List<Color>()
            {
                Color.PINK,
                Color.ORANGE,
                Color.YELLOW,
                Color.GREEN,
                Color.SKYBLUE,
                Color.PURPLE
            };
            Index = gen.Next(0, 5);
            color = colors[Index];
        }

        public void Draw(int y, float controllerPos)
        {
            int distanceToController = (y - 7) * (int)size.Y;
            Raylib.DrawRectangle((int)Pos.X, (int)controllerPos + distanceToController, (int)size.X, (int)size.Y, color);
            Raylib.DrawRectangle((int)(Pos.X / 10), (int)((controllerPos + distanceToController) / 10) + 600, (int)(size.X / 10), (int)(size.Y / 10), color);
            
            //Red controllers
            if (distanceToController == 0)
            {
                Color redCon = new Color(255, 0, 0, 210);
                Raylib.DrawRectangle((int)Pos.X, (int)controllerPos + distanceToController, (int)size.X, (int)size.Y, redCon);
                Raylib.DrawRectangle((int)(Pos.X / 10), (int)((controllerPos + distanceToController) / 10) + 600, (int)(size.X / 10), (int)(size.Y / 10), redCon);
            }
        }

        public void Move(float delta)
        {
            Pos = new Vector2(Pos.X, Pos.Y + (speed * delta));
        }
    }
}





//MAKE ONE FLOAT THE Y POS OF EVERYTHING
//HANDLE SPINNING BASED ON THAT
//DRAW SLOTS BASED ON THAT
//CREATE SLOTS BASED ON THAT?