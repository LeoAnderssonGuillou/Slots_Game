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
        Column[] Columns = new Column[5];
        bool couldProvokeSpin = false;
        bool spinning = false;
        int timesSpun = 0;
        int stoppedColumns = 0;

        

        Queue<Slot> waitingSlots = new Queue<Slot>();


        public Grid()
        {
            startPos = new Vector2(260, 100 - (slotSize.Y * 4));
            grid = new Slot[gX,gY];

            for (int i = 0; i < 5; i++)
            {
                Columns[i] = new Column();
            } 
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
                    Slot controller = grid[x, 7];
                    slot.Draw(y, controller.Pos.Y);     //Make controller.Pos.Y int here?
                }
            }
        }

        //Returns the y position that corresponds to a row in grid
        public float Goal(int y)
        {
            return startPos.Y + (y * slotSize.Y);
        }


        // public void MoveSlots(float delta)
        // {
        //     for (int x = 0; x < gX; x++)
        //     {
        //         for (int y = 0; y < gY; y++)
        //         {
        //             Slot slot = grid[x, y];
        //             //If slot is above where it "should" be based on its position in the grid array, move it down
        //             if (slot.Pos.Y < Goal(y))
        //             {
        //                 slot.Move(delta);
        //                 couldProvokeSpin = false;
        //             }
        //         }
        //     }

        //     //Runs when a full set of 4 slots has passed the rightmost column.
        //     //If all/some columns are still spinning, this means one or more columns should "spin" again.
        //     //If the rigtmost column was the last column spinning, it means the whole board should stop.
        //     if (grid[4, 7].Pos.Y > Goal(7))
        //     {
        //         couldProvokeSpin = true;
        //     }
        // }

        public void MoveSlots(float delta)
        {
            for (int x = 0; x < 5; x++)
            {
                MoveColumn(x, delta);
            }
            couldProvokeSpin = false;

            //Runs when a full set of 4 slots has passed the rightmost column.
            //If all/some columns are still spinning, this means one or more columns should "spin" again.
            //If the rigtmost column was the last column spinning, it means the whole board should stop.
            if (grid[4, 7].Pos.Y >= Goal(7))
            {
                couldProvokeSpin = true;
            }
        }

        public void MoveColumn(int x, float delta)
        {
            Slot controllerSlot = grid[x, 7];

            //If slot is above where it "should" be based on its position in the grid array, move it down
            if (controllerSlot.Pos.Y < Goal(7))
            {
                controllerSlot.Move(delta);
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
                ProvokeSpin(stoppedColumns);
                if (timesSpun >= 3)
                {
                    stoppedColumns++;
                }
            }

            if (stoppedColumns == 5)
            {
                spinning = false;
                couldProvokeSpin = false;
                timesSpun = 0;
                stoppedColumns = 0;
            }
        }

        //Before every "Spin", create 5 sets of 4 slots waiting to spawn
        public void ReloadColumns(int targets)
        {
            for (int i = targets; i < 5; i++)
            {
                Columns[i].Reload();
            } 
        }


        //Create new slots at top
        public void CreateSlotsAtTop(int targets)
        {
            float startY = grid[0, 0].Pos.Y - slotSize.Y;
            for (int x = targets; x < gX; x++)
            {
                Slot slot = Columns[x].waitingSlots.Dequeue();
                slot.Pos = new Vector2(startPos.X + (x * slotSize.X), startY);
                grid[x, 0] = slot;
            }
        }

        //Provoking a spin moves all slots down 4 times and creates new ones at top
        //This only refers to the slots being moved in the "grid" 2D-array. Graphically, they remain in place
        //Slots graphically being above where they "should" be according to the "grid" array will cause them to move in "MoveSlots"
        public void ProvokeSpin(int targets)
        {
            ReloadColumns(targets);

            //Moves all slots down and creates new ones 4 times
            for (int i = 0; i < 4; i++)
            {
                //Move all slots down - Overrides last row
                for (int x = gX - 1; x >= targets; x--)
                {
                    for (int y = gY - 2; y >= 0; y--)
                    {
                        Slot slot = grid[x, y];
                        grid[x, y + 1] = slot;
                    }
                }
                CreateSlotsAtTop(targets);
            }
            timesSpun++;
        }

    }
}