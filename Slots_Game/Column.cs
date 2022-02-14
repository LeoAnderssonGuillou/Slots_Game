using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Slots_Game
{
    public class Column
    {
        public Queue<Slot> WaitingSlots = new Queue<Slot>();    //Queue of slots waiting to spawn
        public float YMovement {get; set;}                      //YMovement acts like a columns origin of movement
        public int Index {get; set;}                            //Which column (0-4) this is

        int controlPos =  (int)(100 + (3 * 240));               //Where YMovement wants to go. When a column is here, it means its slots are graphically where they should be based on their positions in the grid array.
        float speed = 0;                                        //Columns speed of movement
        float maxSpeed = 4000;
        bool tryingToStop = false;
        float gravity = 18000;
        float friction = 10000;
        float stopCounter = 0;
        bool stoppedCompletely = false;
        //maxSpeed 2000, gravity 10000, friction 5000

        //Prepares a queue of 4 slots waiting to spawn
        //Also resets YMovement
        public void Reload()
        {
            for (int i = 0; i < 4; i++)
            {
                Slot slot = new Slot();
                WaitingSlots.Enqueue(slot);
            }
        }

        public void Reset()
        {
            YMovement = 100 - (1 * 240);
            tryingToStop = false;
            stopCounter = 0;
            stoppedCompletely = false;
        }

        public void Move(float delta)
        {
            //If the column's origin of movement (YMovement) is above its "goal"(controlPos), move it down by accelerating
            if (YMovement < controlPos)
            {
                speed += gravity * delta;
                if (speed > maxSpeed)
                {
                    speed = maxSpeed;
                }
                YMovement += speed * delta;
            }
            //If origin is below controlPos, it must mean it should stop - meaning it should accelerate up
            //When the column senses it is below controlPos, that means controlPos has not been moved down and the column should aim to stop
            //This causes it to try to accelerate up
            else if (YMovement > controlPos)
            {
                //When this first happens, the column enters tryingToStop-mode
                tryingToStop = true;
                speed -= gravity * delta;
                YMovement += speed * delta;
            }

            //In tryingToStop-mode, a friction is added making the column eventually stop at controlPos
            if (tryingToStop)
            {
                if (speed > 0)
                {
                    speed -= friction * delta;
                }
                else if (speed < 0)
                {
                    speed += friction * delta;
                }
                //If column has not stopped correctly, it will still snap to controlPos after 0.9 seconds in tryingToStop-mode
                stopCounter += delta; 
                if (stopCounter > 0.9f)
                {
                    YMovement = controlPos;
                    speed = 0;
                    stoppedCompletely = true;
                    //Console.WriteLine($"Column {Index} has stopped");
                }
            }
        }

        //Returns whether the column has graphically stopped moving
        public bool HasStopped()
        {
            return stoppedCompletely;
        }

    }
}