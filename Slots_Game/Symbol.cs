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
        protected Random gen = new Random();
        protected int[] winValues;
        protected Vector2 size = new Vector2(280, 240);


        public abstract void Draw(int y, Reel reel);
        public abstract bool CreatingWin(Symbol ofWinningType);

        public int GetBaseWinFromStreak(int streak)
        {
            return winValues[streak - 3];
        }

    }
}



