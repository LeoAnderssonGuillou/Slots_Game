using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


namespace Slots_Game
{
    public abstract class Symbol : GameObject    
    {
        public int Index {get; set;}
        public Vector2 Pos {get; set;}
        protected Color color = Color.YELLOW;
        protected Random gen = new Random();
        protected int[] winValues;
        Vector2 size = new Vector2(280, 240);


        public void Draw(int y, Reel reel)
        {
            int yMovement = (int)reel.YMovement;
            int distanceToController = (y - 7) * (int)size.Y;
            int xPos = 260 + (reel.Index * (int)size.X);

            Raylib.DrawRectangle(xPos, yMovement+ distanceToController, (int)size.X, (int)size.Y, color);
            Raylib.DrawRectangleLines(xPos, yMovement+ distanceToController, (int)size.X, (int)size.Y, Color.BLACK);
            //Raylib.DrawRectangle((int)(xPos / 10), ((yMovement + distanceToController) / 10) + 600, (int)(size.X / 10), (int)(size.Y / 10), color);
        }
        

        public abstract bool CreatingWin(int winningType);

    }
}



