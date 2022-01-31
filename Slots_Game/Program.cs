using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace Slots_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Vector2 winSize = new Vector2(1920, 1200);
            Raylib.InitWindow((int)winSize.X, (int)winSize.Y, "Gymnasiearbete");
            Raylib.SetTargetFPS(165);

            Grid grid = new Grid();
            grid.Fill();


             while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);
                float delta = Raylib.GetFrameTime();


                //Board
                Raylib.DrawRectangle(260, 100, 1400, 960, Color.BLACK);
                //Slot
                //Raylib.DrawRectangle(260, 100, 280, 240, Color.YELLOW);
                Raylib.DrawRectangle(0, 610, 240, 96, Color.GRAY);
                Raylib.DrawRectangle(0, 514, 240, 96, Color.LIGHTGRAY);
                Raylib.DrawRectangle(0, 706, 240, 96, Color.LIGHTGRAY);
                Raylib.DrawRectangle(0, 418, 240, 96, Color.BEIGE);

                //Grid
                grid.DrawSlots();
                grid.MoveSlots(delta);

                 //Top
                Raylib.DrawRectangle(0, 0, (int)winSize.X, 100, Color.PURPLE);
                //Bottom
                Raylib.DrawRectangle(0, 1060, (int)winSize.X, 140, Color.PURPLE);


                grid.HandleSpinning();





                //FPS
                int fps = Raylib.GetFPS();
                Raylib.DrawText($"{fps}", 50, 50, 50, Color.GRAY);

                Raylib.EndDrawing();
            }
        }
    }
}
