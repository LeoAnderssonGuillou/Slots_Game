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


             while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);
                float delta = Raylib.GetFrameTime();

                

                //Top
                Raylib.DrawRectangle(0, 0, (int)winSize.X, 100, Color.PURPLE);
                //Bottom
                Raylib.DrawRectangle(0, 1060, (int)winSize.X, 140, Color.PURPLE);
                //Board
                Raylib.DrawRectangle(260, 100, 1400, 960, Color.BLACK);
                //Slot
                Raylib.DrawRectangle(260, 100, 280, 240, Color.YELLOW);






                //FPS
                int fps = Raylib.GetFPS();
                Raylib.DrawText($"{fps}", 50, 50, 50, Color.GRAY);

                Raylib.EndDrawing();
            }
        }
    }
}
