using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


//REMAINING STUFF:
//Blinking paylines
//Graphics
//Win counting up

//Klassdiagram
//Instructions
//Comments
//Every method does 1 thing
//Another generic class


namespace Slots_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup
            Vector2 winSize = new Vector2(1920, 1200);
            Raylib.InitWindow((int)winSize.X, (int)winSize.Y, "SLOTS GAME");
            Raylib.SetTargetFPS(165);
            Game game = new Game();
            Grid grid = new Grid();

            //UI Colors
            Color marginCol = GameObject.GetCol("8f00db", 0);
            Color bgCol = GameObject.GetCol("08003b", 0);

            
             while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(bgCol);
                float delta = Raylib.GetFrameTime();

                switch (game.State)
                {
                    //Starting screen
                    case 0:
                        game.StartingScreen(winSize);
                        break;

                    //Gameplay
                    case 1:
                        //Reels background
                        Raylib.DrawRectangle(260, 100, 1400, 960, Color.BLACK);

                        //Symbols
                        grid.DrawSymbols();
                        grid.MoveSymbols(delta);

                        //Margins
                        Game.DrawMargins(winSize, marginCol);

                        //Text
                        game.DrawMoney();
                        game.DrawWin(delta);
                        game.DrawBet();

                        //Player interaction
                        game.HandleButton();
                        game.UpdateBet();

                        //Logic
                        grid.HandleSpinning(game);
                        grid.HandleWinning(game);
                        break;

                    //Bankrupt screen
                    case 2:
                        Raylib.WindowShouldClose();
                        break;
                }
                
                Raylib.EndDrawing();
            }
        }
    }
}
