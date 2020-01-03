using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Colorful;
using Consoletris.Entities;
using Console = Colorful.Console;

namespace Consoletris
{
	class Tetris
	{
		public static readonly Vector PFP = new Vector(15, 5, 10 ,20); //PlayfieldParameters
		public static readonly char BorderChar = '█';
		public static readonly char PieceChar = '█';
		public static readonly char SpaceChar = ' ';
		public static readonly Color BorderColor = Color.FromArgb(255, 255, 255);
		public static float PieceMoveTime = 60f / 69f;
		public static Block[,] PField = new Block[PFP.Width + 2, PFP.Height + 2]; 
		public static List<Piece> blocks = new List<Piece>();
		public static Vector CurrentMainBlockPos = new Vector();
		public static bool IsRunning = true;

		public static bool LeftPressed = false;
		public static bool RightPressed = false;
		public static bool HoldPressed = false;
		public static bool HDPressed = false;
		public static bool DownPressed = false;
		public static bool CRotPressed = false;
		public static bool CWRotPressed = false;

		public static void Entrance()
		{
			InitializePlayfield();
			InitializeBlocks();
			GameLoop();
		}

		private static void InitializeBlocks()
		{
			blocks.Add(new Piece() { Matrix = new bool[,] { { false, true, false }, { true, true, true }, { false, false, false } }, Color = Color.FromArgb(255, 0, 255) });
		}


		private static void GameLoop()
		{
			int a = 0;
			CurrentMainBlockPos = new Vector(PFP.X + PFP.Width, PFP.Y);
			Stopwatch DefaultPieceMove = new Stopwatch();
			DefaultPieceMove.Start();
			while (IsRunning)
			{
				for (int i = 0; i < PField.GetLength(0); i++)
				{
					for (int j = 0; j < PField.GetLength(1); j++)
					{
						Console.SetCursorPosition(i, j);
						Console.Write(PField[i,j].Exists ? "X" : " ");
					}
				}

				if ((int)(PieceMoveTime * 1000)<DefaultPieceMove.ElapsedMilliseconds)
				{
					DrawBlock(blocks[0], CurrentMainBlockPos, true);
					CurrentMainBlockPos.Y += 1;
					DefaultPieceMove.Restart();
				}
				else
				{
					DrawBlock(blocks[0], CurrentMainBlockPos);
				}
				if (a < PFP.Height - 1)
					a++;
				Thread.Sleep(1);
			}
		}

		private static void HandleGameInput()
		{
			Stopwatch keyDownTime = new Stopwatch();
			while (IsRunning)
			{
				if (LeftPressed)
				{
					DrawBlock(blocks[0], CurrentMainBlockPos);
					CurrentMainBlockPos.X -= 1;
					DrawBlock(blocks[0], CurrentMainBlockPos, true);
				}
				if (RightPressed)
				{
					DrawBlock(blocks[0], CurrentMainBlockPos);
					CurrentMainBlockPos.X += 1;
					DrawBlock(blocks[0], CurrentMainBlockPos, true);

				}




				Thread.Sleep(2);
			}
		}

		private static void DrawBlock(Piece piece, Vector pos, bool space = false)
		{
			Console.ForegroundColor = piece.Color;
			for (int i = 0; i < piece.Matrix.GetLength(0); i++)
			{
				for (int j = 0; j < piece.Matrix.GetLength(1); j++)
				{
					if (piece.Matrix[i,j])
					{
						Console.SetCursorPosition(pos.X  + j * 2 - 1, pos.Y + i - 1);
						Console.Write(space ? new string(SpaceChar, 2) : new string(PieceChar, 2));
						PField[j + (pos.X - PFP.X)/2 -1, i + pos.Y - PFP.Y] = new Block(!space, piece.Color);
					}
				}
			}
		}
		private static void InitializePlayfield()
		{
			for (int i = 0; i < PField.GetLength(0); i++)
				for (int j = 0; j < PField.GetLength(1); j++)
					PField[i, j] = new Block(false, Color.Black);
			Console.ForegroundColor = BorderColor;
			PField[0, 0] = new Block(true, BorderColor);
			PField[PFP.Width + 1, 0] = new Block(true, BorderColor);
			for (int i = 0; i < PFP.Height ; i++)
			{
				Console.SetCursorPosition(PFP.X, PFP.Y + i);
				Console.Write(new string(BorderChar,2) + new string(SpaceChar, PFP.Width * 2) + new string(BorderChar, 2));
				PField[0, i + 1] = new Block(true, BorderColor);
				PField[PFP.Width + 1, i + 1] = new Block(true, BorderColor);
			}
			Console.SetCursorPosition(PFP.X, PFP.Y + PFP.Height);
			Console.Write(new string(BorderChar, 4 + PFP.Width * 2));
			for (int i = 0; i < PField.GetLength(0); i++)
				PField[i, PFP.Height + 1] = new Block(true, BorderColor);
		}
	}
}
