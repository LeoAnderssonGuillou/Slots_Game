using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


namespace Slots_Game
{
    public class StandardSymbol : Symbol
    {
        

        public StandardSymbol()
        {
            List<Color> colors = new List<Color>()
            {
                Color.RED,
                Color.YELLOW,
                Color.GREEN,
                Color.ORANGE,
                Color.SKYBLUE,
                Color.PURPLE
            };
            
            Index = gen.Next(0, 4);
            switch (Index)
            {
                case 0:
                    //J
                    winValues = new int[]{1, 3, 8};
                    break;
                case 1:
                    //Coins
                    winValues = new int[]{3, 7, 15};
                    break;
                case 2:
                    //Turtle
                    winValues = new int[]{5, 10, 20};
                    break;
                case 3:
                    //Dragon
                    winValues = new int[]{10, 20, 50};
                    break;
            }
            color = colors[Index];
        }


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

    }
}