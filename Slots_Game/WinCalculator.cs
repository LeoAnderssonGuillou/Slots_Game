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
            Slot[] horizontalOne = {grid[0,4], grid[1,4], grid[2,4], grid[3,4], grid[4,4]};
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
            Raylib.DrawLineEx(start, end, 10, Color.GREEN);
        }


    }
    
}