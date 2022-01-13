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
        Column[] waitingColumns = new Column[5];
        bool couldProvokeSpin = false;
        bool spinning = false;
        int timesSpun = 0;

        Queue<Slot> waitingSlots = new Queue<Slot>();


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

        //Returns the y position that corresponds to a row in grid
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
                        couldProvokeSpin = false;
                    }
                }
            }

            //Runs when a full board of slots has passed the screen
            if (grid[0, 7].Pos.Y > Goal(7))
            {
                couldProvokeSpin = true;
            }
        }

        //Determines whether to spin or not
        public void HandleSpinning()
        {
            if (Raylib.IsKeyReleased(KeyboardKey.KEY_ENTER) && !spinning)
            {
                spinning = true;
                couldProvokeSpin = true;
            }

            if (couldProvokeSpin && spinning)
            {
                ProvokeSpin();
            }

            if (timesSpun == 5)
            {
                spinning = false;
                couldProvokeSpin = false;
                timesSpun = 0;
            }
        }

        //Before every "Spin", create 5 columns of slots waiting to spawn
        public void PrepareNewWaitingColumns()
        {
            for (int i = 0; i < 5; i++)
            {
                waitingColumns[i] = new Column();
            } 
        }


        //Create new slots at top
        public void CreateSlotsAtTop()
        {
            float startY = grid[0, 0].Pos.Y - slotSize.Y;
            for (int x = 0; x < gX; x++)
            {
                Slot slot = waitingColumns[x].slots.Dequeue();
                slot.Pos = new Vector2(startPos.X + (x * slotSize.X), startY);
                grid[x, 0] = slot;
            }
        }

        //Provoking a spin moves all slots down 4 times and creates new ones at top
        //This only refers to the slots being moved in the "grid" 2D-array - graphically, they remain in place
        //Slots graphically being above where they "should" be according to the "grid" array will cause them to move in "MoveSlots"
        public void ProvokeSpin()
        {
            PrepareNewWaitingColumns();

            //Moves all slots down and creates new ones 4 times
            for (int i = 0; i < 4; i++)
            {
                //Move all slots down - Overrides last row
                for (int x = gX - 1; x >= 0; x--)
                {
                    for (int y = gY - 2; y >= 0; y--)
                    {
                        Slot slot = grid[x, y];
                        grid[x, y + 1] = slot;
                    }
                }
                CreateSlotsAtTop();
            }
            timesSpun++;
        }

    }
}