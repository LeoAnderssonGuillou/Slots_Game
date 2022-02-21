using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


namespace Slots_Game
{
    public class WinCalculator
    {
        Slot[,] grid = new Slot[5,12];

        Slot[] payline1;

        public WinCalculator(Slot[,]grid_)
        {
            Slot[] paylineBro = {grid[0,0], grid[1,0], grid[2,0], grid[3,0], grid[4,0]};
            payline1 = paylineBro;

            grid = grid_;
        }
    
        public int CalculateWinsBoard()
        {
            int win = 0;
            win += CalculateWinsPayline(payline1);
            return win;
        }

        public int CalculateWinsPayline(Slot[] payline)
        {
            bool looking = true;
            int i = 1;
            int win = 0;
            while (looking)
            {
                looking = ExamineSlot(i, payline);
                if (looking)
                {
                    win += 100;
                    i++;
                }
                if (i > 4)
                {
                    looking = false;
                }
            }
            return win;
        }

        public bool ExamineSlot(int i , Slot[] payline)
        {
            Slot currentSlot = payline[i];
            Slot previousSlot = payline[i - 1];

            if (currentSlot.Index == previousSlot.Index)
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