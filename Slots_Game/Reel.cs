using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


namespace Slots_Game
{
    public class Reel
    {
        //CLASS - REEL: One of the five columns of the grid. Handles the creation and movement of symbols. (Symbols move in union with the other symbols on the reel.) Exists in 5 instances only.
        public Queue<Symbol> WaitingSymbols = new Queue<Symbol>();  //Queue of symbols waiting to spawn
        public float YMovement {get; set;}                          //YMovement acts like a reel's origin of movement
        public int Index {get; set;}                                //Which reel (0-4) this is

        int controlPos =  (int)(100 + (3 * 240));                   //Where YMovement wants to go. When a reel is here, it means its symbols are graphically where they should be based on their positions in the grid array.
        float speed = 0;
        float maxSpeed = 5000;
        bool tryingToStop = false;
        float gravity = 39000;
        float friction = 20000;
        float stopCounter = 0;
        bool stoppedCompletely = false;
        Random generator = new Random();

        //Prepares a queue of 4 symbols waiting to spawn
        //Also resets YMovement
        public void Reload()
        {
            for (int i = 0; i < 4; i++)
            {
                Symbol symbol;
                if (generator.Next(0, 8) < 7)
                {
                    symbol = new StandardSymbol();
                }
                else
                {
                    symbol = new Wild();
                }
                WaitingSymbols.Enqueue(symbol);
            }
        }

        //Resets movement related variables of reel when it stops
        public void Reset()
        {
            YMovement = 100 - (1 * 240);
            tryingToStop = false;
            stopCounter = 0;
            stoppedCompletely = false;
        }

        //Movement of reel
        public void Move(float delta)
        {
            //If the reel's origin of movement (YMovement) is above its "goal"(controlPos), move it down by accelerating
            if (YMovement < controlPos)
            {
                speed += gravity * delta;
                if (speed > maxSpeed)
                {
                    speed = maxSpeed;
                }
                YMovement += speed * delta;
            }
            //When the reel senses it is below controlPos, that means controlPos has not been moved down (spin has not been provoked) and the reel should aim to stop
            //This causes it to try to accelerate up
            else if (YMovement > controlPos)
            {
                //When this first happens, the reel enters tryingToStop-mode
                tryingToStop = true;
                speed -= gravity * delta;
                YMovement += speed * delta;
            }

            //In tryingToStop-mode, a friction is added making the reel eventually stop at controlPos
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
                //If reel has not stopped correctly, it will still snap to controlPos after 0.9 seconds in tryingToStop-mode
                stopCounter += delta; 
                if (stopCounter > 0.6f)
                {
                    YMovement = controlPos;
                    speed = 0;
                    stoppedCompletely = true;
                }
            }
        }

        //Returns whether the reel has graphically stopped moving
        public bool HasStopped()
        {
            return stoppedCompletely;
        }
    }
}