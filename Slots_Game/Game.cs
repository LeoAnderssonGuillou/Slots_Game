using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


namespace Slots_Game
{
    public class Game
    {
        public int Bet {get; set;} = 1000;
        public int Win {get; set;} = 0;
        int money = 1000;
        

        public void DrawMoney()
        {
            CenteredText($"${money}", 1920, 50, 30, 0);
        }

        public void DrawWin()
        {
            CenteredText($"{Win}", 1920, 76, 1080, 0);
            CenteredText("WIN", 1920, 30, 1155, 0);
        }

        public void DrawBet()
        {
            CenteredText("TOTAL BET", 960, 30, 1100, 0);
            CenteredText($"{Bet}", 960, 46, 1130, 0);
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
            int step = 200;
            if (Raylib.IsKeyReleased(KeyboardKey.KEY_RIGHT))
            {
                Bet += step;
            }
            if (Raylib.IsKeyReleased(KeyboardKey.KEY_LEFT))
            {
                Bet -= step;
            }
            Bet = Math.Clamp(Bet, 0, money);
        }

        //Way of easily drawing Raylib text, centered on the x-axis
        public void CenteredText(string text, int fullWidth, int fontSize, int yPos, int xStart)
        {
            Raylib.DrawText(text, xStart + (fullWidth - Raylib.MeasureText(text, fontSize)) / 2, yPos, fontSize, Color.WHITE);
        }

        //Draws a button with a "capsule" look
        public void DrawButton(string text, int fullWidth, int fontSize, int yPos, int xStart)
        {
            int margin = 0;//20
            int buttonHeight = fontSize + margin;
            int textLenght = Raylib.MeasureText(text, fontSize);
            int x = xStart + (fullWidth - textLenght) / 2;

            Raylib.DrawRectangle(x, yPos - 5 - (margin / 2), textLenght, buttonHeight, Color.GREEN);
            Raylib.DrawCircle(x, yPos + (fontSize / 2) - 5, buttonHeight / 2, Color.GREEN);
            Raylib.DrawCircle(x + textLenght, yPos + (fontSize / 2) - 5, buttonHeight / 2, Color.GREEN);
            Raylib.DrawText(text, x, yPos, fontSize, Color.WHITE);
        }

        public void ChangeMoney(int change)
        {
            money += change;
        }
    }
}