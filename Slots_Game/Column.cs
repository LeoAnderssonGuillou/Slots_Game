using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Slots_Game
{
    public class Column
    {
        public Queue<Slot> slots;

        public Column()
        {
            slots = new Queue<Slot>();
             
            for (int i = 0; i < 4; i++)
            {
                Slot slot = new Slot(i);
                slots.Enqueue(slot);
            }
        }
    }
}