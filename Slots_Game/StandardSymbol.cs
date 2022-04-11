using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


namespace Slots_Game
{
    //CLASS - STANDARDSYMBOL: The "normal", most common type of symbol. When created, it is randomized as one of 5 types.
    public class StandardSymbol : Symbol
    {


        public StandardSymbol()
        {
            //Standard symbols come in 5 types, each with their own color and profitability.
            Index = gen.Next(0, 5);
            switch (Index)
            {
                case 0:
                    //"J"
                    winValues = new int[]{1, 3, 8};
                    color = Color.PURPLE;
                    break;
                case 1:
                    //"Coins"
                    winValues = new int[]{3, 7, 15};
                    color = Color.ORANGE;
                    break;
                case 2:
                    //"Frog"
                    winValues = new int[]{5, 10, 30};
                    color = Color.YELLOW;
                    break;
                case 3:
                    //"Dragon"
                    winValues = new int[]{10, 20, 50};
                    color = Color.GREEN;
                    break;
                case 4:
                    //"A"
                    winValues = new int[]{2, 5, 10};
                    color = Color.RED;
                    break;
            }
        }

        //Returns true if it is of the same type as the given type
        public override bool CreatingWin(Symbol ofWinningType)
        {
            if (Index == ofWinningType.Index)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Draws the symbol
        public override void Draw(int y, Reel reel)
        {
            int yMovement = (int)reel.YMovement;
            int distanceToController = (y - 7) * (int)size.Y;
            int xPos = 260 + (reel.Index * (int)size.X);

            Raylib.DrawRectangle(xPos, yMovement+ distanceToController, (int)size.X, (int)size.Y, color);
            Raylib.DrawRectangleLines(xPos, yMovement+ distanceToController, (int)size.X, (int)size.Y, Color.BLACK);
            //Raylib.DrawRectangle((int)(xPos / 10), ((yMovement + distanceToController) / 10) + 600, (int)(size.X / 10), (int)(size.Y / 10), color);
        }

    }
}