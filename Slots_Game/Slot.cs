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
        Color color = Color.YELLOW;
        Random gen = new Random();


        public Slot()
        {
            List<Color> colors = new List<Color>()
            {
                //Color.PINK,
                Color.RED,
                Color.ORANGE,
                Color.YELLOW,
                Color.GREEN,
                Color.SKYBLUE,
                Color.PURPLE
            };
            Index = gen.Next(0, 5);
            color = colors[Index];
        }

        public void Draw(int y, Column column)
        {
            int yMovement = (int)column.YMovement;
            int distanceToController = (y - 7) * (int)size.Y;
            int xPos = 260 + (column.Index * (int)size.X);

            Raylib.DrawRectangle(xPos, yMovement+ distanceToController, (int)size.X, (int)size.Y, color);
            //Raylib.DrawRectangle((int)(xPos / 10), ((yMovement + distanceToController) / 10) + 600, (int)(size.X / 10), (int)(size.Y / 10), color);
            
            //Red controllers
            // if (distanceToController == 0)
            // {
            //     Color redCon = new Color(255, 0, 0, 210);
            //     Raylib.DrawRectangle(xPos, yMovement + distanceToController, (int)size.X, (int)size.Y, redCon);
            //     Raylib.DrawRectangle((int)(xPos / 10), ((yMovement + distanceToController) / 10) + 600, (int)(size.X / 10), (int)(size.Y / 10), redCon);
            // }
        }

    }
}



