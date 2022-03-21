using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Slots_Game
{
    public class Game
    {
        public int Bet {get; set;} = 1000;
        public int Win {get; set;} = 0;
        int money = 100000;
        


        public void DrawMoney()
        {
            CenteredText($"${money}", 1920, 50, 30, 0);
        }

        public void DrawWin()
        {
            CenteredText($"{Win}", 1920, 75, 1080, 0);
            CenteredText("WIN", 1920, 30, 1155, 0);
        }

        public void CenteredText(string text, int fullWidth, int fontSize, int yPos, int xStart)
        {
            Raylib.DrawText(text, xStart + (fullWidth - Raylib.MeasureText(text, fontSize)) / 2, yPos, fontSize, Color.WHITE);
        }
    }
}