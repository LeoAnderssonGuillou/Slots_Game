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
        Column[] columns = new Column[5];
        bool couldProvokeSpin = false;
        bool spinning = false;
        int timesSpun = 0;
        int stoppedColumns = 0;
        int controlPos =  (int)(100 + (3 * 240));

        Queue<Slot> waitingSlots = new Queue<Slot>();
        int[] rows = new int[5];


        public Grid()
        {
            startPos = new Vector2(260, 100 - (slotSize.Y * 4));
            grid = new Slot[gX,gY];

            for (int i = 0; i < 5; i++)
            {
                columns[i] = new Column() {Index = i};
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
                    slot.Draw(y, columns[x]);
                }
            }
        }


        public void MoveSlots(float delta)
        {
            for (int x = 0; x < 5; x++)
            {
                columns[x].Move(delta);
            }
            couldProvokeSpin = false;

            //Runs when a full set of 4 slots has passed the rightmost column.
            //If all/some columns are still spinning, this means one or more columns should "spin" again.
            //If the rigtmost column was the last column spinning, it means the whole board should stop. (HandleSpinning)
            if (columns[4].YMovement > controlPos)
            {
                couldProvokeSpin = true;
            }
        }



        //Determines whether to spin or not
        public void HandleSpinning()
        {
            if (columns[4].HasStopped())
            {
                Console.WriteLine(CalculateWinsBoard());
                DrawWinLines();
            }

            if (Raylib.IsKeyReleased(KeyboardKey.KEY_ENTER) && columns[4].HasStopped())
            {
                spinning = true;
                couldProvokeSpin = true;
                ResetRowsWinIndex();
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

        public int CalculateWinsBoard()
        {
            int win = 0;
            for (int y = 0; y < 4; y++)
            {
                win += CalculateWinsRow(y);
            }
            return win;
        }

        public int CalculateWinsRow(int y)
        {
            bool looking = true;
            int x = 1;
            int win = 0;
            while (looking)
            {
                looking = ExamineSlot(x, y);
                if (looking)
                {
                    win += 100;
                    rows[y]++;
                    x++;
                }
                if (x > 4)
                {
                    looking = false;
                }
            }
            return win;
        }

        public void ResetRowsWinIndex()
        {
            for (int x = 0; x < 4; x++)
            {
                rows[x] = 0;
            }
        }

        public bool ExamineSlot(int targetX , int y)
        {
            Slot currentSlot = grid[targetX, 4 + y];
            Slot previousSlot = grid[targetX - 1, 4 + y];

            if (currentSlot.Index == previousSlot.Index)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DrawWinLines()
        {
            int boxHeight = 10;
            for (int x = 0; x < 4; x++)
            {
                int winSlots = rows[x];
                Raylib.DrawRectangle(280, (220 - (boxHeight / 2)) + (240 * x), (260 * winSlots) - 40, boxHeight, Color.BLACK);
            }
        }
        

        //Before every "ProvokeSpin", create 5 sets of 4 slots waiting to spawn
        //Also resets movement variables of columns
        public void ReloadColumns(int targets)
        {
            for (int i = targets; i < 5; i++)
            {
                columns[i].Reload();
                columns[i].Reset();
            } 
        }


        //Create new slots at top
        public void CreateSlotsAtTop(int targets, int i)
        {
            for (int x = targets; x < gX; x++)
            {
                Slot slot = columns[x].WaitingSlots.Dequeue();
                grid[x, 0] = slot;
            }
        }

        //Provoking a spin moves all slots down 4 times and creates new ones at top
        //This only refers to the slots being moved in the "grid" 2D-array. Graphically, they remain in place
        //Columns' YMovement being reset causes them to move down graphically
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
                CreateSlotsAtTop(targets, i);
            }
            timesSpun++;
        }

    }
}