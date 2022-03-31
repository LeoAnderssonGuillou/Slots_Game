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
        }


        public override bool CreatingWin(int i , Payline payline)
        {
            
            return true;
        } 
    }
}