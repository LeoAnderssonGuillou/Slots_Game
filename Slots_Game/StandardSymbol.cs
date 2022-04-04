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
            
            Index = gen.Next(0, 3);
            switch (Index)
            {
                case 0:
                    winValues = new int[]{1, 3, 8};
                    break;
                case 1:
                    winValues = new int[]{5, 10, 20};
                    break;
                case 2:
                    winValues = new int[]{10, 20, 50};
                    break;
            }
            color = colors[Index];
        }


        public override bool CreatingWin(int winningType)
        {
            if (Index == winningType)
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