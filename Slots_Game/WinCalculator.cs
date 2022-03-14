using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


namespace Slots_Game
{
    public class WinCalculator
    {
        Slot[,] grid;
        Payline payline1 = new Payline();

        public WinCalculator(Slot[,]grid_)
        {
            grid = grid_;
        }

        //Ensures paylines contain the slots that are currently there
        public void RefreshPaylineContents()
        {
            Slot[] horizontalOne = {grid[0,4], grid[1,5], grid[2,4], grid[3,7], grid[4,4]};
            payline1.Line = horizontalOne;
        }
    
        public int CalculateWinsBoard()
        {
            RefreshPaylineContents();
            int win = 0;
            win += CalculateWinsPayline(payline1);
            return win;
        }

        public int CalculateWinsPayline(Payline payline)
        {
            bool looking = true;
            int i = 1;
            int win = 0;
            payline.WinLenght = 0;
            while (looking)
            {
                looking = ExamineSlot(i, payline);
                if (looking)
                {
                    win += 100;
                    payline.WinLenght++;
                    i++;
                }
                if (i > 4)
                {
                    looking = false;
                }
            }
            return win;
        }

        public bool ExamineSlot(int i , Payline payline)
        {
            Slot currentSlot = payline.Line[i];
            Slot previousSlot = payline.Line[i - 1];

            if (currentSlot.Index == previousSlot.Index)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DrawWinningLines()
        {
            int thickness = 10;
            Payline payline = payline1;
            // Vector2 start = new Vector2(payline.Line[0].Pos.X, payline.Line[0].Pos.Y + 120 - (thickness / 2));
            // Vector2 end = new Vector2(payline.Line[4].Pos.X, payline.Line[4].Pos.Y + 120 - (thickness / 2));
            //Raylib.DrawLineEx(start, end, thickness, Color.BLACK);

            Vector2 startFirst = new Vector2(payline.Line[0].Pos.X, payline.Line[0].Pos.Y + 120 - (thickness / 2));
            Vector2 endFirst = new Vector2(payline.Line[0].Pos.X + 140, payline.Line[0].Pos.Y + 120 - (thickness / 2));
            Raylib.DrawLineEx(startFirst, endFirst, thickness, Color.BLACK);

            for (int i = 0; i < 4; i++)
            {
                Vector2 start = new Vector2(payline.Line[i].Pos.X + 140, payline.Line[i].Pos.Y + 120 - (thickness / 2));
                Vector2 end = new Vector2(payline.Line[i + 1].Pos.X + 140, payline.Line[i + 1].Pos.Y + 120 - (thickness / 2));
                Raylib.DrawLineEx(start, end, thickness, Color.BLACK);
                Raylib.DrawCircleV(start, thickness / 2, Color.BLACK);
            }

            Vector2 startFinal = new Vector2(payline.Line[4].Pos.X + 140, payline.Line[4].Pos.Y + 120 - (thickness / 2));
            Vector2 endFinal = new Vector2(payline.Line[4].Pos.X + 280, payline.Line[4].Pos.Y + 120 - (thickness / 2));
            Raylib.DrawLineEx(startFinal, endFinal, thickness, Color.BLACK);
            Raylib.DrawCircleV(startFinal, thickness / 2, Color.BLACK);
        }



    }
    
}