using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


namespace Slots_Game
{
    public class Game
    {
        public long Bet {get; set;} = 1000;
        public long Win {get; set;} = 0;
        public bool PressingSpin {get; set;}

        long controlBet = 1000;
        long money = 300;
        double graphicalWin = 0;
        

        public void DrawMoney()
        {
            CenteredText($"${money.ToString("N0")}", 1920, 50, 30, 0);
        }

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

        public void ResetGraphicalWin()
        {
            graphicalWin = 0;
        }

        public void DrawBet()
        {
            CenteredText("TOTAL BET", 960, 30, 1100, 0);
            CenteredText($"{Bet.ToString("N0")}", 960, 46, 1130, 0);
        }

        //Draws the SPIN! button and checks if it is being clicked
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
            if (Bet > money)
            {
                PressingSpin = false;
            }
        }

        public void UpdateBet()
        {
            while (Bet > money && Bet >= 200)
            {
                Console.WriteLine("BROO");
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

    }
}