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