using System;
using System.Linq;

namespace TicTacToe
{
	class Point<T>
	{
		public T Row;
		public T Col;
	}

	class TicTacToe
	{
		static int FieldSize = 3;
		static int[,] Playfield = new int[FieldSize, FieldSize];
		static Point<int> Pos = new Point<int>() { Row = 0, Col = 0 };
		//CellHeight Range 3-19
		static readonly int CellHeight = 11;
		static readonly int CellWidth = CellHeight * 2;
		static bool[,] PatternX = new bool[CellHeight, CellWidth];
		static bool[,] PatternO = new bool[CellHeight, CellWidth];
		static bool LastPlayer = true;

		public static void Start()
		{
			Console.WindowHeight = CellHeight * 3 + 5;
			Console.WindowWidth = CellWidth * 3 + 5;
			GeneratePattern();
			GameLoop();
			Console.ReadKey();
		}

		static bool[,] vs = new bool[CellHeight, CellWidth / 2];

		private static void GeneratePattern()
		{
			int it1 = 0;
			int it2 = CellWidth - 1;
			for (int i = 0; i < CellHeight; i++)
			{
				PatternX[i, it1] = true;
				PatternX[i, it1 + 1] = true;
				PatternX[i, it2] = true;
				PatternX[i, it2 - 1] = true;
				it1 += 2;
				it2 -= 2;
			}
			for (int i = 2; i < CellWidth - 2; i++)
			{
				PatternO[0, i] = true;
				PatternO[CellHeight - 1, i] = true;
			}
			for (int i = 1; i < CellHeight - 1; i++)
			{
				PatternO[i, 1] = true;
				PatternO[i, CellWidth - 2] = true;
			}
		}

		private static void GameLoop()
		{
			int winner = 0;
			while (winner == 0)
			{
				DrawField();
				GetInput();
				winner = CheckWinner();
			}
			DrawField();
			Console.SetCursorPosition(0, CellHeight * FieldSize + 3);
			Console.WriteLine(winner == 3 ? "Draw" : $"\nWinner is {(winner == 1 ? "O" : "X")}");
		}

		private static int CheckWinner()
		{
			for (int i = 0; i < 3; i++)
				if (Playfield[i, 0] != 0 && Playfield[i, 0] == Playfield[i, 1] && Playfield[i, 0] == Playfield[i, 2])
					return Playfield[i, 0];
			for (int i = 0; i < 3; i++)
				if (Playfield[0, i] != 0 && Playfield[0, i] == Playfield[1, i] && Playfield[0, i] == Playfield[2, i])
					return Playfield[0, i];
			if (Playfield[0, 0] == Playfield[1, 1] && Playfield[0, 0] == Playfield[2, 2])
				return Playfield[0, 0];
			if (Playfield[0, 2] == Playfield[1, 1] && Playfield[0, 2] == Playfield[2, 0])
				return Playfield[0, 2];
			if (Playfield.Cast<int>().Min() > 0)
				return 3;
			return 0;

		}

		private static void GetInput()
		{
			InvertColor();
			DrawCell(Pos.Row, Pos.Col);
			NormalColor();
			ConsoleKeyInfo consoleKey = Console.ReadKey();
			if (consoleKey.Key == ConsoleKey.LeftArrow && Pos.Col > 0)
				Pos.Col--;
			if (consoleKey.Key == ConsoleKey.RightArrow && Pos.Col < FieldSize-1)
				Pos.Col++;
			if (consoleKey.Key == ConsoleKey.UpArrow && Pos.Row > 0)
				Pos.Row--;
			if (consoleKey.Key == ConsoleKey.DownArrow && Pos.Row < FieldSize-1)
				Pos.Row++;
			if (consoleKey.Key == ConsoleKey.O && Playfield[Pos.Row, Pos.Col] == 0)
				Playfield[Pos.Row, Pos.Col] = 1;
			if (consoleKey.Key == ConsoleKey.X && Playfield[Pos.Row, Pos.Col] == 0)
				Playfield[Pos.Row, Pos.Col] = 2;
			if (consoleKey.Key == ConsoleKey.R)
				Playfield[Pos.Row, Pos.Col] = 0;
			if (consoleKey.Key == ConsoleKey.Enter && Playfield[Pos.Row, Pos.Col] == 0)
			{
				Playfield[Pos.Row, Pos.Col] = LastPlayer == true ? 2 : 1;
				LastPlayer = !LastPlayer;
			}
		}

		//┼, │, ─
		//
		//   │ O │
		//───┼───┼───
		// O │ O │
		//───┼───┼───
		// X │ X │ X
		private static void DrawField()
		{
			for (int x = 0; x < FieldSize; x++)
			{
				for (int y = 0; y < FieldSize; y++)
					DrawCell(x, y);
				if (x < 2)
					Console.WriteLine("\n" + new string('─', CellWidth) + '┼' + new string('─', CellWidth) + '┼' + new string('─', CellWidth));
			}
		}

		private static void DrawCell(int x, int y)
		{
			Console.SetCursorPosition(y * CellWidth + 2 * y, x * CellHeight + x);
			for (int k = 0; k < CellHeight; k++)
			{
				Console.SetCursorPosition(y * CellWidth + y, Console.CursorTop);
				for (int l = 0; l < CellWidth; l++)
					Console.Write(Playfield[x, y] == 0 ? " " : Playfield[x, y] == 1 ? (PatternO[k, l] == true ? "O" : " ") : PatternX[k, l] == true ? "X" : " ");
				if (y < FieldSize-1)
					Console.Write("│");
				if (k < CellHeight - 1)
					Console.Write("\n");
			}
		}

		private static void NormalColor()
		{
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
		}

		private static void InvertColor()
		{
			Console.BackgroundColor = ConsoleColor.DarkBlue;
			Console.ForegroundColor = ConsoleColor.DarkRed;
		}
	}
}
