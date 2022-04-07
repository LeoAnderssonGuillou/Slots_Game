using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


//REMAINING STUFF:
//Change bet
//String manipulation for clarity
//Blinking paylines
//Graphics
//Win counting up

//Klassdiagram
//Instruktioner
//Comments
//Every method does 1 thing
//Another generic class


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

            //Colors
            Color marginCol = GameObject.GetCol("8f00db", 0);
            Color bgCol = GameObject.GetCol("08003b", 0);

            Game game = new Game();
            Grid grid = new Grid();
            grid.Fill();


             while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(bgCol);
                float delta = Raylib.GetFrameTime();


                //Board
                Raylib.DrawRectangle(260, 100, 1400, 960, Color.BLACK);
                
                //Side indicators
                // Raylib.DrawRectangle(0, 610, 240, 96, Color.GRAY);
                // Raylib.DrawRectangle(0, 514, 240, 96, Color.LIGHTGRAY);
                // Raylib.DrawRectangle(0, 706, 240, 96, Color.LIGHTGRAY);
                // Raylib.DrawRectangle(0, 418, 240, 96, Color.BEIGE);

                //Grid
                grid.DrawSymbols();
                grid.MoveSymbols(delta);

                 //Top
                Raylib.DrawRectangle(0, 0, (int)winSize.X, 100, marginCol);
                //Bottom
                Raylib.DrawRectangle(0, 1060, (int)winSize.X, 140, marginCol);

                //Text
                game.DrawMoney();
                game.DrawWin();
                game.DrawBet();

                //Player interaction
                bool clickingButton = game.HandleButton();
                game.UpdateBet();

                //Logic
                grid.HandleSpinning(game, clickingButton);
                grid.HandleWinning(game);


                //FPS
                // int fps = Raylib.GetFPS();
                // Raylib.DrawText($"{fps}", 50, 50, 50, Color.GRAY);

                Raylib.EndDrawing();
            }
        }

    }
}
