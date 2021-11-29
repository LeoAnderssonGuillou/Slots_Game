using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Slots_Game
{
    public class Grid
    {   
        int gX = 5;
        int gY = 12;
        Slot[,] grid;
        Vector2 slotSize = new Vector2(280, 240);
        Vector2 startPos;
        Column[] waitingColumns = new Column[4];
        public bool spinAgain = false;


        public Grid()
        {
            startPos = new Vector2(260, 100 - (slotSize.Y * 4));
            grid = new Slot[gX,gY];
        }


        public void Fill()
        {
            for (int x = 0; x < gX; x++)
            {
                for (int y = 0; y < gY; y++)
                {
                    grid[x, y] = new Slot();
                    Slot slot = grid[x, y];
                    slot.Pos = new Vector2(startPos.X + (x * slotSize.X), startPos.Y + (y * slotSize.Y));
                }
            }
        }

        public void DrawSlots()
        {
            for (int x = 0; x < gX; x++)
            {
                for (int y = 0; y < gY; y++)
                {
                    Slot slot = grid[x, y];
                    slot.Draw();
                }
            }
        }

        public float Goal(int y)
        {
            return startPos.Y + (y * slotSize.Y);
        }

        public void MoveSlots(float delta)
        {
            for (int x = 0; x < gX; x++)
            {
                for (int y = 0; y < gY; y++)
                {
                    Slot slot = grid[x, y];
                    //If slot is above where it "should" be based on its position in the grid array, move it down
                    if (slot.Pos.Y < Goal(y))
                    {
                        slot.Move(delta);
                        spinAgain = false;
                    }
                }
            }

            if (grid[0, 7].Pos.Y > Goal(7))
            {
                Console.WriteLine("yo?");
                spinAgain = true;
                //headstart = grid[0, 7].Pos.Y - Goal(7);
            }
        }

        public void Spin(int length)
        {
            for (int i = 0; i < length; i++)
            {
                //Move slots down - Overrides last row
                for (int x = gX - 1; x >= 0; x--)
                {
                    for (int y = gY - 2; y >= 0; y--)
                    {
                        Slot slot = grid[x, y];
                        grid[x, y + 1] = slot;
                    }
                }

                //Create new slots at top
                float startY = grid[0, 0].Pos.Y - slotSize.Y;
                for (int x = 0; x < gX; x++)
                {
                    Slot slot = new Slot();
                    slot.Pos = new Vector2(startPos.X + (x * slotSize.X), startY);
                    grid[x, 0] = slot;
                }
            } 
        }
    }
}