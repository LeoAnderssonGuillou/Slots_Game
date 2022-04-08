using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


namespace Slots_Game
{
    public class Wild : Symbol
    {
        

        public Wild()
        {
            Index = -1;
            color = Color.RED;
            winValues = new int[]{10, 50, 250};
        }


        public override bool CreatingWin(Symbol ofWinningType)
        {
            return true;
        }

        public override void Draw(int y, Reel reel)
        {
            int yMovement = (int)reel.YMovement;
            int distanceToController = (y - 7) * (int)size.Y;
            int xPos = 260 + (reel.Index * (int)size.X);

            Raylib.DrawRectangle(xPos, yMovement+ distanceToController, (int)size.X, (int)size.Y, Color.GOLD);
            Raylib.DrawRectangle(xPos + 10, yMovement+ distanceToController + 10, (int)size.X - 20, (int)size.Y - 20, Color.MAROON);
            Raylib.DrawRectangleLines(xPos, yMovement+ distanceToController, (int)size.X, (int)size.Y, Color.BLACK);
            Game.CenteredText("WILD", (int)size.X, 100, yMovement + distanceToController + 70, xPos, Color.GOLD);
            //Raylib.DrawRectangle((int)(xPos / 10), ((yMovement + distanceToController) / 10) + 600, (int)(size.X / 10), (int)(size.Y / 10), color);
        }

    }
}