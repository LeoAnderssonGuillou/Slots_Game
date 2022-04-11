using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


namespace Slots_Game
{  
    //CLASS - SYMBOL: Abstract base class for symbols. Exists in favour of WILD, BONUS, and other potential special symbols.
    public abstract class Symbol : GameObject    
    {
        public int Index {get; set;}
        public Vector2 Pos {get; set;}
        protected Random gen = new Random();
        protected int[] winValues;
        protected Vector2 size = new Vector2(280, 240);

        //Draw the symbol
        public abstract void Draw(int y, Reel reel);

        //Check if the symbol is crea
        public abstract bool CreatingWin(Symbol ofWinningType);

        //Examines if a symbol is the same as the previous symbol on the payline (or if it is a WILD)
        public int GetBaseWinFromStreak(int streak)
        {
            return winValues[streak - 3];
        }

    }
}



