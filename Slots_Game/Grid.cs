using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


namespace Slots_Game
{   
    //CLASS - GRID: Handles most things related to the 4x5 grid of slots seen on the screen. Exists in one instance only.
    public class Grid
    {   
        int gX = 5;
        int gY = 12;
        Symbol[,] grid;
        Vector2 symbolSize = new Vector2(280, 240);
        Vector2 startPos;
        Reel[] reels = new Reel[5];
        bool couldProvokeSpin = false;
        bool spinning = false;
        int timesSpun = 0;
        int stoppedReels = 0;
        bool hasCalculated = false;
        int controlPos =  (int)(100 + (3 * 240));
        long win;

        WinCalculator winCalculator;


        public Grid()
        {
            startPos = new Vector2(260, 100 - (symbolSize.Y * 4));
            grid = new Symbol[gX,gY];
            winCalculator = new WinCalculator(grid);
            
            for (int i = 0; i < 5; i++)
            {
                reels[i] = new Reel() {Index = i};
            } 
            Fill();
        }

        //Fills the entire grid with slots during its initiation
        public void Fill()
        {
            for (int x = 0; x < gX; x++)
            {
                for (int y = 0; y < gY; y++)
                {
                    grid[x, y] = new StandardSymbol();
                }
            }
        }

        //Draws symnbols
        public void DrawSymbols()
        {
            for (int x = 0; x < gX; x++)
            {
                for (int y = 0; y < gY; y++)
                {
                    Symbol symbol = grid[x, y];
                    symbol.Draw(y, reels[x]);
                }
            }
        }

        //Moves symbols (graphically)
        public void MoveSymbols(float delta)
        {
            for (int x = 0; x < 5; x++)
            {
                reels[x].Move(delta);
            }
            couldProvokeSpin = false;

            //When a full set of 4x5 symbols has passed the screen, a new spin can be proved (see ProvokeSpin)
            if (reels[4].YMovement > controlPos)
            {
                couldProvokeSpin = true;
            }
        }


        //Determines whether to spin or not
        public void HandleSpinning(Game game)
        {
            //Lets player iniate a new spin when all reels has stopped
            if (game.PressingSpin && reels[4].HasStopped())
            {
                spinning = true;
                couldProvokeSpin = true;
                hasCalculated = false;
                game.ChangeMoney(-game.Bet);
            }

            //If a spin can be provoked AND the reels should continue to spin - provoke spin
            if (couldProvokeSpin && spinning)
            {
                ProvokeSpin(stoppedReels);
                if (timesSpun >= 3)
                {
                    stoppedReels++;
                }
            }

            //When all 5 reels have stopped spinning - stop completely
            if (stoppedReels == 5)
            {
                spinning = false;
                couldProvokeSpin = false;
                timesSpun = 0;
                stoppedReels = 0;
            }
        }

        //When all reels have stopped - calculate the winnings and draw winning paylines
        public void HandleWinning(Game game)
        {
            if (reels[4].HasStopped())
            {
                if (!hasCalculated)
                {
                    game.ResetGraphicalWin();
                    win = 0;
                    UpdateSymbolPositions();
                    win = winCalculator.CalculateWinsBoard(game.Bet);
                    hasCalculated = true;
                    game.Win = win;
                    game.ChangeMoney(win);
                }
                winCalculator.DrawWinningLines();
            }
        }


        //Assigns every symbol its graphical position (used for drawing paylines)
        public void UpdateSymbolPositions()
        {
            for (int x = 0; x < gX; x++)
            {
                for (int y = 0; y < gY; y++)
                {
                    Symbol symbol = grid[x, y];
                    symbol.Pos = new Vector2(260 + (x * symbolSize.X), -860 + (y * symbolSize.Y));
                }
            }
        }
        

        //Before every "ProvokeSpin", create 5 sets of 4 symbols waiting to spawn
        //Also resets movement variables of reels
        public void ReloadReels(int targets)
        {
            for (int i = targets; i < 5; i++)
            {
                reels[i].Reload();
                reels[i].Reset();
            } 
        }


        //Create new symbols at top
        public void CreateSymbolsAtTop(int targets, int i)
        {
            for (int x = targets; x < gX; x++)
            {
                Symbol symbol = reels[x].WaitingSymbols.Dequeue();
                grid[x, 0] = symbol;
            }
        }

        //Provoking a spin moves all symbols down 4 times in the grid and creates new ones at top
        //This only refers to the symbols being moved in the "grid" 2D-array. Graphically, they remain in place
        //Reels' YMovement being reset causes them to strive to move down graphically
        public void ProvokeSpin(int targets)
        {
            ReloadReels(targets);

            //Moves all symbols down and creates new ones 4 times
            for (int i = 0; i < 4; i++)
            {
                //Move all symbols down - Overrides last row
                for (int x = gX - 1; x >= targets; x--)
                {
                    for (int y = gY - 2; y >= 0; y--)
                    {
                        Symbol symbol = grid[x, y];
                        grid[x, y + 1] = symbol;
                    }
                }
                CreateSymbolsAtTop(targets, i);
            }
            timesSpun++;
        }

    }
}
