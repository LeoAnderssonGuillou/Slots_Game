using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Slots_Game
{
    public class Column
    {
        Queue<Slot> slots = new Queue<Slot>();

        public Column()
        {
            for (int i = 0; i < 4; i++)
            {
                Slot slot = new Slot(){Index = i};
                slots.Enqueue(slot);
            }
        }
    }
}