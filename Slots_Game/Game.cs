using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


namespace Slots_Game
{
    public class Game
    {
        long controlBet = 1000;
        public long Bet {get; set;} = 1000;
        public long Win {get; set;} = 0;
        long money = 5000000000000000000;
        

        public void DrawMoney()
        {
            CenteredText($"${money.ToString("N0")}", 1920, 50, 30, 0);
        }

        public void DrawWin()
        {
            int fSize = 76;
            if (Win > 1000000000000)
            {
                fSize = 50;
            }
            CenteredText($"{Win.ToString("N0")}", 1920, fSize, 1080, 0);
            CenteredText("WIN", 1920, 30, 1155, 0);
        }

        public void DrawBet()
        {
            CenteredText("TOTAL BET", 960, 30, 1100, 0);
            CenteredText($"{Bet.ToString("N0")}", 960, 46, 1130, 0);
        }

        //Draws the SPIN! button and checks if it is being clicked
        public bool HandleButton()
        {
            DrawButton("SPIN!", 960, 80, 1094, 960);
            Rectangle hitbox = new Rectangle(1286, 1079, 308, 100);
            bool hovering = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), hitbox);
            bool clicking = Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON);
            if (hovering && clicking)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateBet()
        {
            long oldBet = Bet;
            long oldControlBet = controlBet;
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT))
            {
                controlBet += controlBet;
                RoundBet();
                
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT))
            {
                controlBet -= controlBet / 2;
                RoundBet();
            }
            if (Bet > money)
            {
                Bet = oldBet;
                controlBet = oldControlBet;
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


        //Way of easily drawing Raylib text, centered on the x-axis
        public void CenteredText(string text, int fullWidth, int fontSize, int yPos, int xStart)
        {
            Raylib.DrawText(text, xStart + (fullWidth - Raylib.MeasureText(text, fontSize)) / 2, yPos, fontSize, Color.WHITE);
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

        public void ChangeMoney(long change)
        {
            money += change;
        }
    }
}