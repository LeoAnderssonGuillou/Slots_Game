using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


namespace Slots_Game
{
    //CLASS - GAME: Handles basic game functions and interface. Exists in one instance only.
    public class Game
    {
        public int State {get; set;} = 0;
        public long Bet {get; set;} = 1000;
        public long Win {get; set;} = 0;
        public bool PressingSpin {get; set;}

        long controlBet = 1000;
        long money = 10000;
        double graphicalWin = 0;
        

        //Draws current player money
        public void DrawMoney()
        {
            CenteredText($"${money.ToString("N0")}", 1920, 50, 30, 0);
        }

        //Draws what the player have won
        public void DrawWin(float delta)
        {
            int fSize = 76;
            if (Win > 1000000000000)
            {
                fSize = 50;
            }
            UpdateGraphicalWin(delta);
            CenteredText($"{graphicalWin.ToString("N0")}", 1920, fSize, 1080, 0);
            CenteredText("WIN", 1920, 30, 1155, 0);
        }

        //When a win is displayed, it quickly counts up from 0 until it reaches the actie win 
        public void UpdateGraphicalWin(float delta)
        {
            if (graphicalWin < Win)
            {
                graphicalWin += Win * delta * 3;
            }
            if (graphicalWin > Win)
            {
                graphicalWin = Win;
            }
        }

        //Encapsulates graphicalWin
        public void ResetGraphicalWin()
        {
            graphicalWin = 0;
        }

        //Draws the active bet
        public void DrawBet()
        {
            CenteredText("TOTAL BET", 960, 30, 1100, 0);
            CenteredText($"{Bet.ToString("N0")}", 960, 46, 1130, 0);
        }

        //Draws the SPIN! button and checks if it is being pressed (with left-click or with enter)
        public void HandleButton()
        {
            DrawButton("SPIN!", 960, 80, 1094, 960);
            Rectangle hitbox = new Rectangle(1286, 1079, 308, 100);
            bool hovering = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), hitbox);
            bool clicking = Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON);
            PressingSpin = false;
            if (hovering && clicking)
            {
                PressingSpin = true;
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                PressingSpin = true;
            }
        }

        //Checks if player want to change the active bet with input
        public void UpdateBet()
        {
            while (Bet > money && Bet >= 200)
            {
                controlBet -= controlBet / 2;
                RoundBet();
            }

            long oldBet = Bet;
            long oldControlBet = controlBet;
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT))
            {
                controlBet += controlBet;
                RoundBet();
                if (Bet > money)
                {
                    Bet = oldBet;
                    controlBet = oldControlBet;
                }
                
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT))
            {
                controlBet -= controlBet / 2;
                RoundBet();
            }
            
        }
        
        //In UpdateBet, the bet is changed 50%. Here, it is then rounded int he following way:
        //109 will become 100, 4412 will become 4000, 18934 will become 20000, etc.)
        public void RoundBet()
        {
            if (controlBet < 125)
            {
                controlBet = 125;
            }
            int betDecimals = controlBet.ToString().Length - 1;
            double roundedBet = controlBet / Math.Pow(10, betDecimals);
            roundedBet = Math.Round(roundedBet);
            roundedBet = roundedBet * Math.Pow(10, betDecimals);
            Bet = (long)roundedBet;
        }


        //Way of easily drawing Raylib text, centered within a given range on the x-axis
        public static void CenteredText(string text, int fullWidth, int fontSize, int yPos, int xStart)
        {
            Raylib.DrawText(text, xStart + (fullWidth - Raylib.MeasureText(text, fontSize)) / 2, yPos, fontSize, Color.WHITE);
        }
        public static void CenteredText(string text, int fullWidth, int fontSize, int yPos, int xStart, Color color)
        {
            Raylib.DrawText(text, xStart + (fullWidth - Raylib.MeasureText(text, fontSize)) / 2, yPos, fontSize, color);
        }

        //Draws a button with a "capsule" look
        public void DrawButton(string text, int fullWidth, int fontSize, int yPos, int xStart)
        {
            int margin = 20;
            int buttonHeight = fontSize + margin;
            int textLenght = Raylib.MeasureText(text, fontSize);
            int x = xStart + (fullWidth - textLenght) / 2;

            Raylib.DrawRectangle(x, yPos - 5 - (margin / 2), textLenght, buttonHeight, Color.GREEN);
            Raylib.DrawCircle(x, yPos + (fontSize / 2) - 5, buttonHeight / 2, Color.GREEN);
            Raylib.DrawCircle(x + textLenght, yPos + (fontSize / 2) - 5, buttonHeight / 2, Color.GREEN);
            Raylib.DrawText(text, x, yPos, fontSize, Color.WHITE);
        }

        //Change total player money
        public void ChangeMoney(long change)
        {
            money += change;
        }
        
        //Draw top and bottom screen margins
        public static void DrawMargins(Vector2 winSize, Color marginCol)
        {
            Raylib.DrawRectangle(0, 0, (int)winSize.X, 100, marginCol);
             Raylib.DrawRectangle(0, 1060, (int)winSize.X, 140, marginCol);
        }

        //Draw starting screen, giving player instructions
        public void StartingScreen(Vector2 size)
        {
            Raylib.ClearBackground(Color.GOLD);
            Raylib.DrawRectangle(50, 50, (int)size.X - 100, (int)size.Y - 100, Color.MAROON);

            CenteredText("WELCOME", (int)size.X, 200, 150, 0, Color.GOLD);
            CenteredText("SPIN: ENTER", (int)size.X, 75, 600, 0, Color.GOLD);
            CenteredText("CHANGE BET: ARROW KEYS", (int)size.X, 75, 700, 0, Color.GOLD);
            CenteredText("[PRESS ENTER TO START]", (int)size.X, 35, 1000, 0, Color.WHITE);
            
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                
                State = 1;
            }
        }

        //Checks if player has money below 0
        public void CheckIfBankrupt()
        {
            if (money <= 0)
            {
                State = 2;
            }
        }

        //Draws the gameover-screen. This screen is an ending - nothing can be done from here.
        public void GameOverScreeen(Vector2 size)
        {
            Raylib.ClearBackground(Color.BLACK);
            Raylib.DrawRectangle(250, 125, (int)size.X - 500, (int)size.Y - 500, Color.WHITE);
            Raylib.DrawRectangle(275, 150, (int)size.X - 550, (int)size.Y - 550, Color.BLACK);

            CenteredText("BANKRUPT ENDING", (int)size.X, 130, 880, 0, Color.WHITE);
            CenteredText("You bet more than you could afford", (int)size.X, 50, 1050, 0, Color.WHITE);
        }


        //dIcktionary of describtions of the 2 types of symbols. very important (VERY)
        //Alos conviniently use a (skolverket approved) 3rd generic class how cool
        public static void ObligatoryUseOfAThirdGenericClass()
        {
            Dictionary<Symbol, string> epicDictionaryOfSymbols = new Dictionary<Symbol, string>();

            Symbol thisStandardSymbolInParticular = new StandardSymbol();
            Symbol thisWildSymbolInParticular = new Wild();

            epicDictionaryOfSymbols.Add(thisStandardSymbolInParticular, "So this one is just like a normal symbol, right? Comes in a few different colors, makes different amounts of cash based on that. Exists in the grid and stuff. What is the grid exactly? Don't worry about it. Anyway so when a standard symbol is created its color is randomized, aint that crazy?");
            epicDictionaryOfSymbols.Add(thisWildSymbolInParticular, "This one is pretty crazy, yo. It can act like any other color of slot, so it's very good to get on your spins. Says WILD on it too, unbelievable. Anyway so coming to think of it this way of using a dictionary doesn't really make sense. It would be like if I wanted to google cat I would have to input an actual cat. And not like any cat, but a specific individual one at that. Using an array with the Index's of the different types of slots would have made more sense. But then again, then I wouldn't have used three different generic classes would I?");

            Console.WriteLine(epicDictionaryOfSymbols[thisStandardSymbolInParticular]);
            Console.WriteLine(epicDictionaryOfSymbols[thisWildSymbolInParticular]);
        }

    }
}