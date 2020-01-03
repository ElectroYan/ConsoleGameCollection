using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
	class Snake
	{
		private static bool UpPressed = false;
		private static bool LeftPressed = false;
		private static bool DownPressed = false;
		private static bool RightPressed = false;
		private static bool PausePressed = false;
		private static bool IsRunning = true;
		private static int FieldHeight = 15;
		private static int FieldWidth = 15;
		private static int RefreshTime = 150;
		private static FieldType[,] Field = new FieldType[FieldWidth, FieldHeight];
		private static Pos FruitPos = new Pos();
		private static Pos SnakeHead = new Pos(Field.GetLength(0) / 2, Field.GetLength(1) / 2);
		private static Pos PrevSnakeHead = new Pos(Field.GetLength(0) / 2, Field.GetLength(1) / 2);
		private static List<Pos> SnakeTails = new List<Pos>();
		private static int CurrentTailLength = 3;
		private static Random rng = new Random();
		private static Direction CurrentDirection = Direction.Stop;
		public static void Start()
		{
			Initialize();
			GameLoop();
			Console.ReadKey();
			Console.ReadKey();
			Console.ReadKey();
		}

		private static void GameLoop()
		{
			while (IsRunning)
			{
				if (!PausePressed)
				{
					SetDirection();
					RemoveTailEnd();
					SetHead();
					CheckFruit();
					DrawTail();
				}

				Thread.Sleep(RefreshTime);
			}
			DrawAt(0, FieldHeight + 1, "Game over, Score = " + CurrentTailLength + ", maximum is " + (FieldHeight-2) * (FieldWidth-2));
		}

		private static void RemoveTailEnd()
		{
			if (SnakeTails.Count() == CurrentTailLength)
			{
				DrawAt(SnakeTails[0], "  ");
				Field[SnakeTails[0].X, SnakeTails[0].Y] = FieldType.Empty;
				SnakeTails.RemoveAt(0);
			}
		}

		private static void CheckFruit()
		{
			if (Pos.Equal(FruitPos, SnakeHead))
			{
				CurrentTailLength++;
				SetNewFruit();
			}
			DrawAt(FruitPos, color: ConsoleColor.Red);
		}

		private static void SetNewFruit()
		{
			bool PosFine = false;
			while (!PosFine)
			{
				int posX = rng.Next() % FieldWidth;
				int posY = rng.Next() % FieldHeight;
				if (Field[posX, posY] == FieldType.Empty)
				{
					PosFine = true;
					FruitPos = new Pos(posX, posY);
				}
			}
		}

		private static void DrawTail()
		{
			SnakeTails.Add(PrevSnakeHead);
			Field[PrevSnakeHead.X, PrevSnakeHead.Y] = FieldType.Tail;
			DrawAt(PrevSnakeHead, color: ConsoleColor.Blue);
		}

		private static void SetHead()
		{
			PrevSnakeHead = new Pos(SnakeHead.X, SnakeHead.Y);
			DrawAt(PrevSnakeHead, "  ");
			Field[PrevSnakeHead.X, PrevSnakeHead.Y] = FieldType.Empty;
			if (CurrentDirection == Direction.Up)
				SnakeHead.Y--;
			if (CurrentDirection == Direction.Left)
				SnakeHead.X--;
			if (CurrentDirection == Direction.Down)
				SnakeHead.Y++;
			if (CurrentDirection == Direction.Right)
				SnakeHead.X++;
			if (Field[SnakeHead.X, SnakeHead.Y] == FieldType.Empty || Field[SnakeHead.X, SnakeHead.Y] == FieldType.Fruit)
				Field[SnakeHead.X, SnakeHead.Y] = FieldType.Head;
			else
				IsRunning = false;
			DrawAt(SnakeHead, color: ConsoleColor.Green);
		}

		private static void SetDirection()
		{
			if (UpPressed)
			{
				CurrentDirection = Direction.Up;
				UpPressed = false;
			}
			if (LeftPressed)
			{
				CurrentDirection = Direction.Left;
				LeftPressed = false;
			}
			if (DownPressed)
			{
				CurrentDirection = Direction.Down;
				DownPressed = false;
			}
			if (RightPressed)
			{
				CurrentDirection = Direction.Right;
				RightPressed = false;
			}
		}

		private static void Initialize()
		{
			new Thread(()=>GetInputs()).Start();

			for (int i = 0; i < FieldHeight; i++)
				for (int j = 0; j < FieldWidth; j++)
					Field[j, i] = FieldType.Empty;

			DrawBaseStuff();
		}


		private static void DrawBaseStuff()
		{
			SetHead();
			for (int i = 0; i < FieldWidth; i++)
			{
				Field[i, 0] = FieldType.Wall;
				Field[i, FieldHeight-1] = FieldType.Wall;
				DrawAt(i * 2, 0);
				DrawAt(i * 2, FieldHeight - 1);
			}
			for (int i = 0; i < FieldHeight; i++)
			{
				Field[0, i] = FieldType.Wall;
				Field[FieldWidth-1, i] = FieldType.Wall;
				DrawAt(0, i);
				DrawAt(FieldWidth * 2 - 2, i);
			}
			SetNewFruit();
		}

		public static void DrawAt(Pos pos, string ch = "██", ConsoleColor color = ConsoleColor.White)
		{
			DrawAt(pos.X * 2, pos.Y, ch, color);
		}

		public static void DrawAt(int x, int y, string ch = "██", ConsoleColor color = ConsoleColor.White)
		{
			Console.SetCursorPosition(x, y);
			DrawWithColor(color, ch);
		}

		static void DrawWithColor(ConsoleColor color, string text)
		{
			Console.ForegroundColor = color;
			Console.Write(text);
			Console.ForegroundColor = ConsoleColor.Black;
			Console.SetCursorPosition(0, 0);
		}

		private static void GetInputs()
		{
			while (IsRunning)
			{
				ConsoleKey key = Console.ReadKey().Key;
				if ((key == ConsoleKey.UpArrow || key == ConsoleKey.W) && CurrentDirection != Direction.Up && CurrentDirection != Direction.Down)
					UpPressed = true;
				if ((key == ConsoleKey.LeftArrow || key == ConsoleKey.A) && CurrentDirection != Direction.Left && CurrentDirection != Direction.Right)
					LeftPressed = true;
				if ((key == ConsoleKey.DownArrow || key == ConsoleKey.S) && CurrentDirection != Direction.Up && CurrentDirection != Direction.Down)
					DownPressed = true;
				if ((key == ConsoleKey.RightArrow || key == ConsoleKey.D) && CurrentDirection != Direction.Left && CurrentDirection != Direction.Right)
					RightPressed = true;
				if (key == ConsoleKey.P || key == ConsoleKey.Escape)
					PausePressed = !PausePressed;
				SetDirection();
			}
		}
	}

	class Pos
	{
		public int X;
		public int Y;
		public Pos()
		{

		}

		public Pos(int x, int y)
		{
			X = x;
			Y = y;
		}

		public static bool Equal (Pos a, Pos b)
		{
			if (a.X == b.X && a.Y == b.Y)
				return true;
			return false;
		}
	}

	enum FieldType
	{
		Wall, Empty, Tail, Head, Fruit
	}

	enum Direction
	{
		Up, Left, Down, Right, Stop
	}
}
