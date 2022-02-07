using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Slots_Game
{
    public class Column
    {
        public Queue<Slot> waitingSlots = new Queue<Slot>();    //Queue of slots waiting to spawn
        public float YMovement {get; set;}                      //YMovement acts like a columns origin of movement
        public int Index {get; set;}                            //Which column (0-4) this is

        int controlPos =  (int)(100 + (3 * 240));               //Where YMovement wants to go. When a column is here, it means its slots are graphically where they should be based on their positions in the grid array.
        int speed = 1500;                                       //A columns speed of movement


        //Prepares a queue of 4 slots waiting to spawn
        //Also resets YMovement
        public void Reload()
        {
            for (int i = 0; i < 4; i++)
            {
                Slot slot = new Slot();
                waitingSlots.Enqueue(slot);
            }
            YMovement = 100 - (1 * 240);
        }

        public void Move(float delta)
        {
            //If the column's origin of movement is above its "goal"(controlPos), move it down
            if (YMovement < controlPos)
            {
                YMovement += speed * delta;
            }
        }

    }
}