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
            color = Color.PURPLE;
            winValues = new int[]{10, 50, 250};
        }


        public override bool CreatingWin(Symbol ofWinningType)
        {
            return true;
        } 

    }
}