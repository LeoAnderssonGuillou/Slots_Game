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
        float speed = 1500;                                       //A columns speed of movement
        bool tryingToStop = false;
        bool belowControl = false;
        int timesPassedControl = 0;
        float speedChange = 8000;

        //Prepares a queue of 4 slots waiting to spawn
        //Also resets YMovement
        public void Reload()
        {
            for (int i = 0; i < 4; i++)
            {
                Slot slot = new Slot();
                waitingSlots.Enqueue(slot);
            }
        }

        public void Reset()
        {
            YMovement = 100 - (1 * 240);
            speed = 1500;
            tryingToStop = false;
            belowControl = false;
            timesPassedControl = 0;
            speedChange = 8000;
        }

        public void Move(float delta)
        {
            //If the column's origin of movement (YMovement) is above its "goal"(controlPos), move it down
            if (YMovement < controlPos)
            {
                if (tryingToStop)
                {
                    speed += speedChange * delta;
                }
                if (belowControl)
                {
                    belowControl = false;
                    timesPassedControl++;
                }
                YMovement += speed * delta;
            }
            else if (YMovement > controlPos)
            {
                tryingToStop = true;
                if (tryingToStop)
                {
                    speed -= speedChange * delta;
                }
                if (belowControl == false)
                {
                    belowControl = true;
                    timesPassedControl++;
                    if (timesPassedControl >= 1000)
                    {
                        YMovement = controlPos;
                    }

                }
                YMovement += speed * delta;
            }
            if (tryingToStop)
            {
                if (speed > 0)
                {
                    speed -= 5000 * delta;
                }
                else if (speed < 0)
                {
                    speed += 5000 * delta;
                }
                if (timesPassedControl > 6)
                {
                    YMovement = controlPos;
                    speed = 0;
                }


            }
        }

    }
}