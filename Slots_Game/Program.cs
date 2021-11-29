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

                //Grid
                grid.DrawSlots();
                grid.MoveSlots(delta);

                 //Top
                Raylib.DrawRectangle(0, 0, (int)winSize.X, 100, Color.PURPLE);
                //Bottom
                Raylib.DrawRectangle(0, 1060, (int)winSize.X, 140, Color.PURPLE);


                if (Raylib.IsKeyReleased(KeyboardKey.KEY_ENTER))
                {
                    grid.Spin(4);
                }
                if (grid.spinAgain == true)
                {
                    grid.Spin(4);
                    Console.WriteLine("Spun");
                }





                //FPS
                int fps = Raylib.GetFPS();
                Raylib.DrawText($"{fps}", 50, 50, 50, Color.GRAY);

                Raylib.EndDrawing();
            }
        }
    }
}
