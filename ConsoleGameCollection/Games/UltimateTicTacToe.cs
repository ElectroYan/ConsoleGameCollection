using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe
{
	class Point<T>
	{
		public T Row;
		public T Col;
	}

	class UltimateTicTacToe
	{
		static int[,] Playfield = new int[9, 9];
		static int[,] MainField = new int[3, 3];
		static Point<int> Pos = new Point<int>() { Row = 0, Col = 0 };
		//CellHeight Range 3-6
		static readonly int CellHeight = 5;
		static readonly int CellWidth = CellHeight * 2;
		static readonly ConsoleColor FieldGridColor = ConsoleColor.Magenta;
		static bool[,] PatternX = new bool[CellHeight, CellWidth];
		static bool[,] PatternXL = new bool[CellHeight, CellWidth];
		static bool[,] PatternO = new bool[CellHeight, CellWidth];
		static bool[,] PatternOL = new bool[CellHeight, CellWidth];
		static bool LastPlayer = true;

		public static void Start()
		{
			if (CellWidth * 9 + 12 > Console.WindowWidth)
			{
				Console.BufferWidth = CellWidth * 9 + 13;
			}
			else
			{
				Console.WindowHeight = CellHeight * 9 + 12;
				Console.WindowWidth = CellWidth * 9 + 12;
			}
			PatternX = GeneratePattern(false, false);
			PatternXL = GeneratePattern(true, false);
			PatternO = GeneratePattern(false, true);
			PatternOL = GeneratePattern(true, true);
			GameLoop();
			Console.ReadKey();
		}

		private static bool[,] GeneratePattern(bool large, bool circle)
		{
			int height = large ? CellHeight * 3 + 2 : CellHeight;
			int width = large ? CellWidth * 3 + 4 : CellWidth;
			int it1 = 0;
			int it2 = width-1;
			bool[,] vs = new bool[height, width];

			if (circle)
			{
				for (int i = 2; i < width - 2; i++)
				{
					vs[0, i] = true;
					vs[CellHeight - 1, i] = true;
				}
				for (int i = 1; i < height - 1; i++)
				{
					vs[i, 0] = true;
					vs[i, 1] = true;
					vs[i, CellWidth - 1] = true;
					vs[i, CellWidth - 2] = true;
				}
			}
			else
			{
				for (int i = 0; i < height; i++)
				{
					vs[i, it1] = true;
					vs[i, it1 + 1] = true;
					vs[i, it2] = true;
					vs[i, it2 - 1] = true;
					it1 += 2;
					it2 -= 2;
				}
			}

			return vs;
		}

		private static void GameLoop()
		{
			int winner = 0;
			DrawField();
			while (winner == 0)
			{
				GetInput();
				for (int x = 0; x < 3; x++)
					for (int y = 0; y < 3; y++)
						MainField[x,y] = CheckWinner(x, y, false);
				winner = CheckWinner();
			}
			Console.SetCursorPosition(0, CellHeight * 9 + 10);
			Console.WriteLine(winner == 3 ? "Draw" : $"\nWinner is {(winner == 1 ? "O" : "X")}");
		}

		private static void DrawField()
		{
			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					DrawCell(x,y);
				}
				if ((x+1)%3==0 && x<8)
					Console.Write("\n" + new string('═', CellWidth*3+2) + '╬' + new string('═', CellWidth*3+2) + '╬' + new string('═', CellWidth*3+2));
				else if (x<8)
				{
					Console.Write("\n");
					for (int i = 0; i < 3; i++)
					{
						Console.Write( new string('─', CellWidth) + '┼' + new string('─', CellWidth) + '┼' + new string('─', CellWidth));
						if (i < 2)
							Console.Write("║");
					}
				}
			}
		}

		private static void DrawCell(int x, int y, bool invert = false)
		{
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					Console.SetCursorPosition(y * CellWidth + 2 * y, x * CellHeight + x);
					for (int k = 0; k < CellHeight; k++)
					{
						Console.SetCursorPosition(y * CellWidth + y, Console.CursorTop);
						for (int l = 0; l < CellWidth; l++)
						{

							Console.BackgroundColor = invert ? ConsoleColor.DarkBlue : MainField[x / 3, y / 3] == 0 ? ConsoleColor.Black : MainField[x / 3, y / 3] == 1 ? ConsoleColor.DarkGreen : ConsoleColor.DarkRed;
							Console.ForegroundColor = Playfield[x, y] == 0 ? FieldGridColor : Playfield[x, y] == 1 ? ConsoleColor.Green : ConsoleColor.Red;
							Console.Write(Playfield[x, y] == 0 ? " " : Playfield[x, y] == 1 ? (PatternO[k, l] == true ? "█" : " ") : PatternX[k, l] == true ? "█" : " ");
							Console.ForegroundColor = FieldGridColor;
							Console.BackgroundColor = ConsoleColor.Black;
						}

						if (y%3 < 2)
						{
							if (invert)
							{
								NormalColor();
								Console.Write("│");
								InvertColor();
							}
							else
								Console.Write("│");
						}
						else if (y < 8)
						{
							if (invert)
							{
								NormalColor();
								Console.Write("║");
								InvertColor();
							}
							else
								Console.Write("║");
						}

						if (k < CellHeight - 1)
							Console.Write("\n");
					}
				}
			}
		}

		private static int CheckWinner(int x=0, int y=0, bool v=true)
		{
			int[,] CheckField = v ? MainField : Playfield;

			for (int i = x * 3; i < x * 3+3; i++)
				if (CheckField[i, y * 3] != 0
                    && CheckField[i, y * 3] == CheckField[i, y*3+1]
                    && CheckField[i, y * 3+1] == CheckField[i, y * 3+2])
                {
                    return CheckField[i, y * 3];
                }

			for (int i = y * 3; i < y*3+3; i++)
				if (CheckField[x * 3, i] != 0
                    && CheckField[x * 3, i] == CheckField[x * 3+1, i]
                    && CheckField[x * 3, i] == CheckField[x * 3+2, i])
                { 
                    return CheckField[x * 3, i]; 
                }

			if (CheckField[x * 3, y * 3] != 0
                && CheckField[x * 3, x * 3] == CheckField[x * 3+1, y * 3 +1]
                && CheckField[x * 3, y * 3] == CheckField[x * 3+2, y * 3+2])
            {
                return CheckField[x * 3, y * 3];
            }

			if (CheckField[x * 3, y * 3 + 2] != 0
                && CheckField[x * 3, y * 3+2] == CheckField[x * 3+1, y * 3+1]
                && CheckField[x * 3, y * 3+2] == CheckField[x * 3+2, y * 3])
            {
                return CheckField[x * 3, y * 3 + 2];
            }

			for (int i = x*3; i < x*3+3; i++)
				for (int j = y*3; j < y*3+3; j++)
					if (CheckField[i,j] == 0)
						return 0;

			return 3;

		}

		private static void GetInput()
		{
			Point<int> prev = new Point<int> { Row = Pos.Row, Col = Pos.Col };
			InvertColor();
			DrawCell(prev.Row, prev.Col, true);
			NormalColor();
			Console.SetCursorPosition(0, CellHeight * 9 + 10);
			ConsoleKeyInfo consoleKey = Console.ReadKey();

			if (consoleKey.Key == ConsoleKey.A && Pos.Col > 0)
				Pos.Col--;

			if (consoleKey.Key == ConsoleKey.D && Pos.Col < 8)
				Pos.Col++;

			if (consoleKey.Key == ConsoleKey.W && Pos.Row > 0)
				Pos.Row--;

			if (consoleKey.Key == ConsoleKey.S && Pos.Row < 8)
				Pos.Row++;

			if (consoleKey.Key == ConsoleKey.O
                && Playfield[Pos.Row, Pos.Col] == 0
                && MainField[Pos.Row / 3, Pos.Col / 3] == 0)
            {
                Playfield[Pos.Row, Pos.Col] = 1;
            }

			if (consoleKey.Key == ConsoleKey.I
                && Playfield[Pos.Row, Pos.Col] == 0
                && MainField[Pos.Row / 3, Pos.Col / 3] == 0)
            {
                Playfield[Pos.Row, Pos.Col] = 2;
            }

			if (consoleKey.Key == ConsoleKey.R)
				Playfield[Pos.Row, Pos.Col] = 0;

			if (consoleKey.Key == ConsoleKey.Enter
                && Playfield[Pos.Row, Pos.Col] == 0
                && MainField[Pos.Row / 3, Pos.Col / 3] == 0)
			{
				Playfield[Pos.Row, Pos.Col] = LastPlayer == true ? 2 : 1;
				LastPlayer = !LastPlayer;
			}

			DrawCell(prev.Row, prev.Col);
			Console.SetCursorPosition(0, CellHeight * 9 + 10);
		}

		//┼, │, ─
		//
		//   │ O │
		//───┼───┼───
		// O │ O │
		//───┼───┼───
		// X │ X │ X
		

		private static void NormalColor()
		{
			Console.BackgroundColor = ConsoleColor.Black;
		}

		private static void InvertColor()
		{
			Console.BackgroundColor = ConsoleColor.DarkBlue;
		}

	}
}
