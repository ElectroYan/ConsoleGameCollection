using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
	public enum FieldType
	{
		Number, Bomb
	}

	class Field
	{
		public FieldType Type;
		public int Value;
		public bool Flagged;
		public bool Visible;

		public Field(FieldType type)
		{
			Type = type;
			Flagged = false;
			Visible = false;
		}

		public Field(FieldType type, int value)
		{
			Type = type;
			Value = value;
			Flagged = false;
			Visible = false;
		}
	}

	public class Point
	{
		public int X;
		public int Y;

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}
	}

	class Minesweeper
	{
		public static Point cursor = new Point(0, 0);
		static Dictionary<int, ConsoleColor> colorKey = new Dictionary<int, ConsoleColor>
		{
			{-1, ConsoleColor.Red},
			{0, ConsoleColor.Black},
			{1, ConsoleColor.Blue},
			{2, ConsoleColor.DarkBlue},
			{3, ConsoleColor.Green},
			{4, ConsoleColor.DarkGreen},
			{5, ConsoleColor.Yellow},
			{6, ConsoleColor.DarkYellow},
			{7, ConsoleColor.DarkRed},
			{8, ConsoleColor.DarkMagenta}
		};

		public static int FieldWidth;
		public static int FieldHeight;
		public static Field[,] PlayField = new Field[10,20];
		public static void Start()
		{
			Initialize();
			GameLoop();
			Console.ReadKey();
		}

		private static void GameLoop()
		{
			bool running = true;
			while (running)
			{
				bool bomb = CheckAndHandleInput();
				if (bomb)
					running = false;
			}
			DrawFullField(true);
		}

		private static void OpenField(int x, int y)
		{
			PlayField[x, y].Visible = true;
			if (PlayField[x, y].Value != 0)
				return;
			//Check Horizontal/Vertical
			if (x>0 && PlayField[x-1,y].Visible == false)
				OpenField(x - 1, y);
			if (y>0 && PlayField[x,y-1].Visible == false)
				OpenField(x, y-1);
			if (x< FieldWidth-1 && PlayField[x+1,y].Visible == false)
				OpenField(x+1, y);
			if (y<FieldHeight-1 && PlayField[x,y+1].Visible == false)
				OpenField(x, y+1);
			//Check Diagonals
			if (x>0 && y>0 && PlayField[x-1,y-1].Visible == false)
				OpenField(x - 1, y-1);
			if (x>0 && y<FieldHeight-1 && PlayField[x-1,y+1].Visible == false)
				OpenField(x-1, y+1);
			if (x< FieldWidth-1 && y>0 && PlayField[x+1,y-1].Visible == false)
				OpenField(x+1, y-1);
			if (x<FieldWidth-1 && y<FieldHeight-1 && PlayField[x+1,y+1].Visible == false)
				OpenField(x+1, y+1);
		}

		private static bool CheckAndHandleInput()
		{
			ConsoleKey key = Console.ReadKey().Key;
			DrawSingleCell(cursor.X, cursor.Y);
			if (key == ConsoleKey.W && cursor.Y > 0)
				cursor.Y--;
			if (key == ConsoleKey.A && cursor.X > 0)
				cursor.X--;
			if (key == ConsoleKey.S && cursor.Y < FieldHeight-1)
				cursor.Y++;
			if (key == ConsoleKey.D && cursor.X < FieldWidth-1)
				cursor.X++;
			if (key == ConsoleKey.Q)
				PlayField[cursor.X, cursor.Y].Flagged = !PlayField[cursor.X, cursor.Y].Flagged;
			if (key == ConsoleKey.E && !PlayField[cursor.X, cursor.Y].Flagged)
			{
				if (PlayField[cursor.X, cursor.Y].Type == FieldType.Bomb)
					return true;
				else if (PlayField[cursor.X, cursor.Y].Value == 0)
				{
					OpenField(cursor.X, cursor.Y);
					DrawFullField();
				}
				else
					PlayField[cursor.X, cursor.Y].Visible = true;

			}
			DrawSingleCell(cursor.X, cursor.Y, background: ConsoleColor.DarkGray);
			return false;
		}

		private static void Initialize()
		{
			GetFieldSize();
			FieldWidth = PlayField.GetLength(0);
			FieldHeight = PlayField.GetLength(1);
			float bombPercentage = GetBombPercentage();
			int bombCount = (int)(FieldWidth * FieldHeight * bombPercentage / 100);
			GenerateBombs(bombCount);
			GenerateNumbers();
			DrawColorKeys();
			DrawFullField();
		}

		private static void DrawColorKeys()
		{

			for (int i = 0; i <= 8 ; i++)
			{
				Console.SetCursorPosition(FieldWidth * 2 + 3, i);
				Console.ForegroundColor = colorKey[i];
				Console.Write("▒▒");
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write(i > 0 ? ": " + i.ToString() + " bombs nearby" : ": Empty");
			}
			Console.SetCursorPosition(FieldWidth * 2 + 3, 9);
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("▒▒");
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(": Flagged");

		}

		private static void DrawFullField(bool ignoreVisible = false)
		{
			for (int y = 0; y < FieldHeight; y++)
			{
				for (int x = 0; x < FieldWidth; x++)
				{
					DrawSingleCell(x, y, ignoreVisible);
				}
				Console.WriteLine();
			}
		}

		private static void DrawSingleCell(int x,int y, bool ignoreVisible = false, ConsoleColor background = ConsoleColor.Black)
		{
			Console.SetCursorPosition(x*2, y);
			if (PlayField[x, y].Visible || ignoreVisible)
				Console.ForegroundColor = colorKey[PlayField[x, y].Value];
			else
			{
				if (PlayField[x, y].Flagged)
					Console.ForegroundColor = ConsoleColor.Magenta;
				else
					Console.ForegroundColor = (y + x) % 2 == 0 ? ConsoleColor.White : ConsoleColor.Gray;
			}
			Console.BackgroundColor = background;
			Console.Write("▒▒");
			Console.SetCursorPosition(0, FieldHeight+2);
		}

		private static void GenerateNumbers()
		{
			for (int x = 0; x < FieldWidth; x++)
			{
				for (int y = 0; y < FieldHeight; y++)
				{
					if (PlayField[x,y] == null)
					{
						int tmpX = x - 1 >= 0 ? x - 1: x;
						int tmpY = y - 1 >= 0 ? y - 1: y;
						int tmpEndX = x + 1 < FieldWidth ? x + 1 : x;
						int tmpEndY = y + 1 < FieldHeight ? y + 1 : y;
						int count = CountOfBombs(tmpX, tmpY, tmpEndX, tmpEndY);
						PlayField[x, y] = new Field(FieldType.Number, count);
					}
				}
			}
		}

		private static int CountOfBombs(int x, int y, int endX, int endY)
		{
			int count = 0;
			for (int i = x; i <= endX; i++)
			{
				for (int j = y; j <= endY; j++)
				{
					if (PlayField[i, j] != null && PlayField[i,j].Type == FieldType.Bomb)
						count++;
				}
			}
			return count;
		}

		private static void GenerateBombs(int bombCount)
		{
			Random r = new Random();
			int actualBombs = 0;
			while (actualBombs < bombCount)
			{
				int tmpX = r.Next() % FieldWidth;
				int tmpY = r.Next() % FieldHeight;
				if (PlayField[tmpX, tmpY] == null)
				{
					PlayField[tmpX, tmpY] = new Field(FieldType.Bomb, -1);
					actualBombs++;
				}
			}
		}

		private static float GetBombPercentage()
		{
			Console.Write("Bomb percentage [10-90]: ");
			string input = Console.ReadLine();
			Console.Clear();
			if (float.TryParse(input, out float per) && per >= 10 && per <= 90)
				return per;
			else return GetBombPercentage();
		}

		private static void GetFieldSize()
		{
			Console.Write("Field size [x,y]: ");
			string[] inputParts = Console.ReadLine().Split(',');
			
			Console.Clear();

			if (inputParts.Count() == 2 && int.TryParse(inputParts[0], out int x) && int.TryParse(inputParts[1], out int y))
				PlayField = new Field[x, y];
			else GetFieldSize();
		}
	}
}
