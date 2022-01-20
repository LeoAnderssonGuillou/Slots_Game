using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Slots_Game
{
    public class Column
    {
        public Queue<Slot> waitingSlots = new Queue<Slot>();


        //Prepares a queue of 4 slots waiting to spawn
        public void Reload()
        {
            for (int i = 0; i < 4; i++)
            {
                Slot slot = new Slot();
                //Slot slot = new Slot(i);
                waitingSlots.Enqueue(slot);
            }
        }

    }
}