using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            int mode = SelectMode();
            Console.Clear();
            switch (mode)
            {
                case 0:
                    Chess.Chess.Start();
                    break;
                case 1:
                    DealOrNoDeal.DealOrNoDeal.Start();
                    break;
                case 2:
                    Minesweeper.Minesweeper.Start();
                    break;
                case 3:
                    Snake.Snake.Start();
                    break;
                case 4:
                    TicTacToe.TicTacToe.Start();
                    break;
                case 5:
                    UltimateTicTacToe.UltimateTicTacToe.Start();
                    break;
                default:
                    break;
            }
        }

        private static int SelectMode()
        {
            Console.WriteLine("[0] Chess\n[1] Deal or no deal\n[2] Minesweeper\n[3] Snake\n[4] TicTacToe\n[5] Ultimate TicTacToe\n");
            Console.Write("Mode: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int mode) && mode >= 0 && mode <= 5)
                return mode;
            else return SelectMode();
        }
    }
}
