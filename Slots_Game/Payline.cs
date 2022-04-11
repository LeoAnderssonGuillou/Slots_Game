using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


namespace Slots_Game
{
    //CLASS - PAYLINE: A line indicating the way which a streak of slots create a win. There are 50 paylines in total, each allowing for different ways that slots can create wins.
    public class Payline : GameObject
    {
        public Symbol[] Line {get; set;}
        public bool Won {get; set;}

        Color trueColor;
        Color darkColor;
        Color midColor;
        int thickness;
        int xOffset;
        int yOffset;
        
        //Color thickness of lines (for 3D-effect)
        int trueThickness = 16;
        int midThickness = 12;
        int centerThickness = 6;

        public Payline(string col, int x, int y)
        {
            trueColor = GetCol(col, 0);
            midColor = GetCol(col, 22);
            darkColor = GetCol(col, 60);
            xOffset = x * 5;
            yOffset = y * 18;
        }

        //Draws a payline. Technically draws three lines with varying thickness and color to create a 3D-effect
        public void DrawLine()
        {
            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        thickness = trueThickness;
                        color = darkColor;
                        break;
                    case 1:
                        thickness = midThickness;
                        color = midColor;
                        break;
                    case 2:
                        thickness = centerThickness;
                        color = trueColor;
                        break;
                }
                //Draws the first part of the payline
                Vector2 startFirst = new Vector2(Line[0].Pos.X, Line[0].Pos.Y + 120 - (trueThickness / 2) + yOffset);
                Vector2 endFirst = new Vector2(Line[0].Pos.X + 140 + xOffset, Line[0].Pos.Y + 120 - (trueThickness / 2) + yOffset);
                Raylib.DrawLineEx(startFirst, endFirst, thickness, color);

                //Draws all parts of the payline, except start and finish
                for (int l = 0; l < 4; l++)
                {
                    Vector2 start = new Vector2(Line[l].Pos.X + 140 + xOffset, Line[l].Pos.Y + 120 - (trueThickness / 2) + yOffset);
                    Vector2 end = new Vector2(Line[l + 1].Pos.X + 140 + xOffset, Line[l + 1].Pos.Y + 120 - (trueThickness / 2) + yOffset);
                    Raylib.DrawLineEx(start, end, thickness, color);
                    Raylib.DrawLineEx(start, end, thickness, color);
                    Raylib.DrawCircleV(start, thickness / 2, color);
                }

                //Draws the final part of the payline
                Vector2 startFinal = new Vector2(Line[4].Pos.X + 140 + xOffset, Line[4].Pos.Y + 120 - (trueThickness / 2) + yOffset);
                Vector2 endFinal = new Vector2(Line[4].Pos.X + 280, Line[4].Pos.Y + 120 - (trueThickness / 2) + yOffset);
                Raylib.DrawLineEx(startFinal, endFinal, thickness, color);
                Raylib.DrawCircleV(startFinal, thickness / 2, color);
            }
            
        }

        //To create a win, a payline has to contain a streak of the same type of slot. Some types of slots are also better than others, creating larger wins. Therefore, the type of slot that is winning on a payline is determined.
        //This slot is always the first slot on the line, with the excpetion of this slot being a WILD. The following slot then becomes the slot of winning type. If that is also a WILD, the third slot determines the winning type, and so on.
        //Only if all slots on the line are WILDs does WILD become the winning type.
        public Symbol GetWinningType()
        {
            Symbol symbolOfWinningType = Line[0];
            bool foundType = false;
            int i = 0;
            while (!foundType)
            {
                if (i > 3)
                {
                    symbolOfWinningType = Line[0];
                    foundType = true;
                }
                else if (Line[i].Index == -1)
                {
                    i++;
                }
                else
                {
                    foundType = true;
                    symbolOfWinningType = Line[i];
                }
            }
            
            return symbolOfWinningType;
        }

    }

}