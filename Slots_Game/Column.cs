using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Slots_Game
{
    public class Column
    {
        public Queue<Slot> waitingSlots = new Queue<Slot>();
        public float YMovement {get; set;}
        public int Index {get; set;}

        int controlPos =  (int)(100 + (3 * 240));
        int speed = 1500;


        //Prepares a queue of 4 slots waiting to spawn
        //Also resets YMovement
        public void Reload()
        {
            for (int i = 0; i < 4; i++)
            {
                Slot slot = new Slot();
                //Slot slot = new Slot(i);
                waitingSlots.Enqueue(slot);
            }
            YMovement = 100 - (1 * 240);
        }

        public void Move(float delta)
        {
            //If slot is above where it "should" be based on its position in the grid array, move it down
            if (YMovement < controlPos)
            {
                YMovement += speed * delta;
            }
        }

    }
}