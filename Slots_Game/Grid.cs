using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Slots_Game
{
    public class Grid
    {
        Slot[,] grid = new Slot[5,8];
        Vector2 slotSize = new Vector2(280, 240);
        Vector2 startPos;
        public int ShouldMove {get; set;}

        public Grid()
        {
            startPos = new Vector2(260, 100 - (slotSize.Y * 4));
        }


        public void Fill()
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    grid[x, y] = new Slot();
                    Slot slot = grid[x, y];
                    slot.Pos = new Vector2(startPos.X + (x * slotSize.X), startPos.Y + (y * slotSize.Y));
                }
            }
        }

        public void DrawSlots()
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Slot slot = grid[x, y];
                    slot.Draw(startPos.X + (x * slotSize.X), startPos.Y + (y * slotSize.Y));
                }
            }
        }

        public void MoveSlots(float delta)
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Slot slot = grid[x, y];
                    //If slot is above where it "should" be based on its position in the grid array, move it down
                    if (slot.Pos.Y < startPos.Y + (y * slotSize.Y))
                    {
                        slot.Move(delta);
                    }
                    //If not, make sure it is not too far down
                    else
                    {
                        slot.Pos = new Vector2(slot.Pos.X, startPos.Y + (y * slotSize.Y));
                        Console.WriteLine("move not");
                    }
                    
                }
            }
        }

        public void Spin()
        {
            for (int x = 5; x < 0; x--)
            {
                for (int y = 7; y < 0; y--)
                {
                    Slot slot = grid[x, y];
                    grid[x, y + 1] = slot;
                }
            }
        }

    }
}