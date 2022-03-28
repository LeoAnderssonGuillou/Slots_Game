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
            color = colors[Index];
        }
    }
}