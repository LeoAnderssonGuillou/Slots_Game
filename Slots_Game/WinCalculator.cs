using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


namespace Slots_Game
{
    public class WinCalculator
    {
        Slot[,] grid;
        List<Payline> paylines = new List<Payline>();
        

        public WinCalculator(Slot[,]grid_)
        {
            grid = grid_;

            //Creation of every payline, defining color and offset position
            paylines.Add(new Payline("f7fa00", 0, 0));
            paylines.Add(new Payline("57ffaf", 0, 1));
            paylines.Add(new Payline("cd34db", 0, 2));
            paylines.Add(new Payline("ef090a", 0, 3));

            paylines.Add(new Payline("96d300", 0, 4));
            paylines.Add(new Payline("fa706e", 1, 0));
            paylines.Add(new Payline("c7ff00", 1, 1));
            paylines.Add(new Payline("028d04", 1, 2));

            paylines.Add(new Payline("c5fdb2", 1, 3));
            paylines.Add(new Payline("0ffa8b", 1, 4));
            paylines.Add(new Payline("09cc06", 2, 0));
            paylines.Add(new Payline("fb91f4", 2, 1));
            paylines.Add(new Payline("ffaabb", 2, 2));
            paylines.Add(new Payline("f33105", 2, 3));

            paylines.Add(new Payline("7efc6b", 2, 4));
            paylines.Add(new Payline("f9af11", 3, 0));
            paylines.Add(new Payline("01d2a1", 3, 1));
            paylines.Add(new Payline("08faf9", 3, 2));
            paylines.Add(new Payline("fb34d1", 3, 3));
            paylines.Add(new Payline("ff64d9", 3, 4));

            paylines.Add(new Payline("deff6e", 4, 0));
            paylines.Add(new Payline("008a89", 4, 1));
            paylines.Add(new Payline("5dd2fd", 4, 2));
            paylines.Add(new Payline("099afe", 4, 3));
            paylines.Add(new Payline("fa6901", 4, 4));
            paylines.Add(new Payline("9ffb00", 5, 0));

            paylines.Add(new Payline("da9300", 5, 1));
            paylines.Add(new Payline("8bf5f1", 5, 2));
            paylines.Add(new Payline("ff2d6d", 5, 3));
            paylines.Add(new Payline("6430f2", 5, 4));

            paylines.Add(new Payline("e9b645", 6, 0));
            paylines.Add(new Payline("d8006e", 6, 1));
            paylines.Add(new Payline("096b85", 6, 2));
            paylines.Add(new Payline("c146fb", 6, 3));

            paylines.Add(new Payline("043be7", 6, 4));
            paylines.Add(new Payline("65c20e", 7, 0));

            paylines.Add(new Payline("a8cceb", 7, 1));
            paylines.Add(new Payline("fdd30e", 7, 2));
            paylines.Add(new Payline("036a4b", 7, 3));
            paylines.Add(new Payline("050f9b", 7, 4));

            paylines.Add(new Payline("a0932b", 8, 0));
            paylines.Add(new Payline("ae5a60", 8, 1));
            paylines.Add(new Payline("6fb29c", 8, 2));
            paylines.Add(new Payline("cae86b", 8, 3));
            paylines.Add(new Payline("096ec3", 8, 4));
            paylines.Add(new Payline("fd7ba7", 9, 0));
            paylines.Add(new Payline("670f0a", 9, 1));
            paylines.Add(new Payline("9cada5", 9, 2));

            paylines.Add(new Payline("026a2c", 9, 3));
            paylines.Add(new Payline("6649b2", 9, 4));

        }

        //Defines paylines
        //Runs every time board stops, ensuring paylines contain the slots that are currently there
        public void RefreshPaylineContents()
        {
            //"Straight lines"
            Slot[] horizontal1 = {grid[0,5], grid[1,5], grid[2,5], grid[3,5], grid[4,5]};
            paylines[0].Line = horizontal1;
            Slot[] horizontal2 = {grid[0,6], grid[1,6], grid[2,6], grid[3,6], grid[4,6]};
            paylines[1].Line = horizontal2;
            Slot[] horizontal3 = {grid[0,4], grid[1,4], grid[2,4], grid[3,4], grid[4,4]};
            paylines[2].Line = horizontal3;
            Slot[] horizontal4 = {grid[0,7], grid[1,7], grid[2,7], grid[3,7], grid[4,7]};
            paylines[3].Line = horizontal4;

            //"Triangles"
            Slot[] Triangle1 = {grid[0,4], grid[1,5], grid[2,6], grid[3,5], grid[4,4]};
            paylines[4].Line = Triangle1;
            Slot[] Triangle2 = {grid[0,7], grid[1,6], grid[2,5], grid[3,6], grid[4,7]};
            paylines[5].Line = Triangle2;
            Slot[] Triangle3 = {grid[0,5], grid[1,6], grid[2,7], grid[3,6], grid[4,5]};
            paylines[6].Line = Triangle3;
            Slot[] Triangle4 = {grid[0,6], grid[1,5], grid[2,4], grid[3,5], grid[4,6]};
            paylines[7].Line = Triangle4;

            //"W's"
            Slot[] W1 = {grid[0,4], grid[1,5], grid[2,4], grid[3,5], grid[4,4]};
            paylines[8].Line = W1;
            Slot[] W2 = {grid[0,7], grid[1,6], grid[2,7], grid[3,6], grid[4,7]};
            paylines[9].Line = W2;
            Slot[] W3 = {grid[0,5], grid[1,4], grid[2,5], grid[3,4], grid[4,5]};
            paylines[10].Line = W3;
            Slot[] W4 = {grid[0,6], grid[1,7], grid[2,6], grid[3,7], grid[4,6]};
            paylines[11].Line = W4;
            Slot[] W5 = {grid[0,5], grid[1,6], grid[2,5], grid[3,6], grid[4,5]};
            paylines[12].Line = W5;
            Slot[] W6 = {grid[0,6], grid[1,5], grid[2,6], grid[3,5], grid[4,6]};
            paylines[13].Line = W6;

            //"Birds"
            Slot[] Bird1 = {grid[0,4], grid[1,4], grid[2,5], grid[3,4], grid[4,4]};
            paylines[14].Line = Bird1;
            Slot[] Bird2 = {grid[0,7], grid[1,7], grid[2,6], grid[3,7], grid[4,7]};
            paylines[15].Line = Bird2;
            Slot[] Bird3 = {grid[0,5], grid[1,5], grid[2,6], grid[3,5], grid[4,5]};
            paylines[16].Line = Bird3;
            Slot[] Bird4 = {grid[0,6], grid[1,6], grid[2,5], grid[3,6], grid[4,6]};
            paylines[17].Line = Bird4;
            Slot[] Bird5 = {grid[0,6], grid[1,6], grid[2,7], grid[3,6], grid[4,6]};
            paylines[18].Line = Bird5;
            Slot[] Bird6 = {grid[0,5], grid[1,5], grid[2,4], grid[3,5], grid[4,5]};
            paylines[19].Line = Bird6;

            //"Mouths"
            Slot[] Mouth1 = {grid[0,5], grid[1,6], grid[2,6], grid[3,6], grid[4,5]};
            paylines[20].Line = Mouth1;
            Slot[] Mouth2 = {grid[0,6], grid[1,5], grid[2,5], grid[3,5], grid[4,6]};
            paylines[21].Line = Mouth2;
            Slot[] Mouth3 = {grid[0,5], grid[1,4], grid[2,4], grid[3,4], grid[4,5]};
            paylines[22].Line = Mouth3;
            Slot[] Mouth4 = {grid[0,6], grid[1,7], grid[2,7], grid[3,7], grid[4,6]};
            paylines[23].Line = Mouth4;
            Slot[] Mouth5 = {grid[0,7], grid[1,6], grid[2,6], grid[3,6], grid[4,7]};
            paylines[24].Line = Mouth5;
            Slot[] Mouth6 = {grid[0,4], grid[1,5], grid[2,5], grid[3,5], grid[4,4]};
            paylines[25].Line = Mouth6;

            //"Faces"
            Slot[] Face1 = {grid[0,6], grid[1,4], grid[2,4], grid[3,4], grid[4,6]};
            paylines[26].Line = Face1;
            Slot[] Face2 = {grid[0,4], grid[1,7], grid[2,7], grid[3,7], grid[4,5]};
            paylines[27].Line = Face2;
            Slot[] Face3 = {grid[0,7], grid[1,5], grid[2,5], grid[3,5], grid[4,7]};
            paylines[28].Line = Face3;
            Slot[] Face4 = {grid[0,4], grid[1,6], grid[2,6], grid[3,6], grid[4,4]};
            paylines[29].Line = Face4;

            //"Swans"
            Slot[] Swan1 = {grid[0,6], grid[1,6], grid[2,4], grid[3,6], grid[4,6]};
            paylines[30].Line = Swan1;
            Slot[] Swan2 = {grid[0,5], grid[1,5], grid[2,7], grid[3,5], grid[4,5]};
            paylines[31].Line = Swan2;
            Slot[] Swan3 = {grid[0,4], grid[1,4], grid[2,6], grid[3,4], grid[4,4]};
            paylines[32].Line = Swan3;
            Slot[] Swan4 = {grid[0,7], grid[1,7], grid[2,5], grid[3,7], grid[4,7]};
            paylines[33].Line = Swan4;

            //"Statues"
            Slot[] Statue1 = {grid[0,7], grid[1,7], grid[2,4], grid[3,7], grid[4,7]};
            paylines[34].Line = Statue1;
            Slot[] Statue2 = {grid[0,4], grid[1,4], grid[2,7], grid[3,4], grid[4,4]};
            paylines[35].Line = Statue2;

            //"Sticks"
            Slot[] Stick1 = {grid[0,7], grid[1,6], grid[2,5], grid[3,4], grid[4,4]};
            paylines[36].Line = Stick1;
            Slot[] Stick2 = {grid[0,4], grid[1,5], grid[2,6], grid[3,7], grid[4,7]};
            paylines[37].Line = Stick2;
            Slot[] Stick3 = {grid[0,4], grid[1,4], grid[2,5], grid[3,6], grid[4,7]};
            paylines[38].Line = Stick3;
            Slot[] Stick4 = {grid[0,7], grid[1,7], grid[2,6], grid[3,5], grid[4,4]};
            paylines[39].Line = Stick4;

            //"Boomerangs"
            Slot[] Boomerang1 = {grid[0,6], grid[1,5], grid[2,4], grid[3,4], grid[4,4]};
            paylines[40].Line = Boomerang1;
            Slot[] Boomerang2 = {grid[0,5], grid[1,6], grid[2,7], grid[3,7], grid[4,7]};
            paylines[41].Line = Boomerang2;
            Slot[] Boomerang3 = {grid[0,4], grid[1,4], grid[2,4], grid[3,5], grid[4,6]};
            paylines[42].Line = Boomerang3;
            Slot[] Boomerang4 = {grid[0,7], grid[1,7], grid[2,7], grid[3,6], grid[4,5]};
            paylines[43].Line = Boomerang4;
            Slot[] Boomerang5 = {grid[0,7], grid[1,6], grid[2,5], grid[3,5], grid[4,5]};
            paylines[44].Line = Boomerang5;
            Slot[] Boomerang6 = {grid[0,4], grid[1,5], grid[2,6], grid[3,6], grid[4,6]};
            paylines[45].Line = Boomerang6;
            Slot[] Boomerang7 = {grid[0,5], grid[1,5], grid[2,5], grid[3,6], grid[4,7]};
            paylines[46].Line = Boomerang7;
            Slot[] Boomerang8 = {grid[0,6], grid[1,6], grid[2,6], grid[3,5], grid[4,4]};
            paylines[47].Line = Boomerang8;

            //"Stars"
            Slot[] Star1 = {grid[0,5], grid[1,7], grid[2,4], grid[3,7], grid[4,5]};
            paylines[48].Line = Star1;
            Slot[] Star2 = {grid[0,6], grid[1,4], grid[2,7], grid[3,4], grid[4,6]};
            paylines[49].Line = Star2;

        }
    
        //Returns total winnings of current board
        public int CalculateWinsBoard(int bet)
        {
            RefreshPaylineContents();
            int win = 0;
            foreach (Payline payline in paylines)
            {
                payline.Won = false;
                int paylineWin = CalculateWinsPayline(payline);
                if (paylineWin > 0)
                {
                    payline.Won = true;
                }
                win += paylineWin;
            }
            return CalculateRealWin(win, bet);
        }

        //Calculates winnings of a single payline
        public int CalculateWinsPayline(Payline payline)
        {
            bool looking = true;
            int i = 1;
            int win = 1;
            payline.WinLenght = 0;
            while (looking)
            {
                looking = ExamineSlot(i, payline);
                if (looking)
                {
                    win += 1;
                    payline.WinLenght++;
                    i++;
                }
                if (i > 4)
                {
                    looking = false;
                }
            }
            if (win < 3)
            {
                win = 0;
            }
            return win;
        }

        //Examines if a slot is the same as the previous slot on the payline
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

        //Draws paylines containing winning streak
        public void DrawWinningLines()
        {
            foreach (Payline payline in paylines)
            {
                if (payline.Won)
                {
                    payline.Draw(0);
                    payline.Draw(1);
                    payline.Draw(2);
                }
            }
        }

        //Takes the standardised units retured from CalculateWinsPayline, and calculates the actual win based on what was bet 
        public int CalculateRealWin(int winIndex, int bet)
        {
            float win = (bet * 0.05f) * winIndex;
            return (int)win;
        }




    }
    
}