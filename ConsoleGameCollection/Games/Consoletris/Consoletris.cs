using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Consoletris.Entities;
using Console = Colorful.Console;
namespace Consoletris
{
    class Consoletris
    {
        public static readonly Vector PFP = new Vector(15, 5, 10, 20); //PlayfieldParameters
        public static readonly char BorderChar = '█';
        public static readonly char PieceChar = '█';
        public static readonly char SpaceChar = ' ';
        public static readonly Color BorderColor = Color.FromArgb(255, 255, 255);
        public static float PieceMoveTime = 60f / 69f;
        public static Block[,] PField = new Block[PFP.Width + 2, PFP.Height + 2];
        public static List<Piece> Pieces = new List<Piece>();
        public static Vector CurrentBlockPos = new Vector();
        public static Piece CurrentBlock = null;
        public static List<Piece> Queue = new List<Piece>();
        public static bool IsRunning = true;

        public static bool LeftPressed = false;
        public static bool RightPressed = false;
        public static bool HoldPressed = false;
        public static bool HDPressed = false;
        public static bool DownPressed = false;
        public static bool CRotPressed = false;
        public static bool CWRotPressed = false;


        public static void Start()
        {
            Initialize();
            GameLoop();
        }

        private static void Initialize()
        {
            new Thread(() => GetInputs()).Start();
            Blocks.InitializeBlocks();
            InitializePlayfield();
        }

        private static void GameLoop()
        {
            int a = 0;
            CurrentBlockPos = new Vector(PFP.X + PFP.Width, PFP.Y);
            Stopwatch DefaultPieceMove = new Stopwatch();
            DefaultPieceMove.Start();
            while (IsRunning)
            {
                CheckQueue();

                for (int i = 0; i < PField.GetLength(0); i++)
                {
                    for (int j = 0; j < PField.GetLength(1); j++)
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write(PField[i, j].Exists ? "X" : " ");
                    }
                }

                if ((int)(PieceMoveTime * 1000) < DefaultPieceMove.ElapsedMilliseconds)
                {
                    DrawBlock(CurrentBlock, CurrentBlockPos, true);
                    if (!PField[CurrentBlockPos.X, CurrentBlockPos.Y + 1].Exists)
                        CurrentBlockPos.Y += 1;
                    else

                        DefaultPieceMove.Restart();
                }
                else
                {
                    DrawBlock(Pieces[0], CurrentBlockPos);
                }
                if (a < PFP.Height - 1)
                    a++;
                Thread.Sleep(1);
            }
        }

        private static void CheckQueue()
        {
            if (Queue.Count() < 7)
            {
                
                Queue.AddRange(Shuffle(Pieces));
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
            for (int i = 0; i < PFP.Height; i++)
            {
                Console.SetCursorPosition(PFP.X, PFP.Y + i);
                Console.Write(new string(BorderChar, 2) + new string(SpaceChar, PFP.Width * 2) + new string(BorderChar, 2));
                PField[0, i + 1] = new Block(true, BorderColor);
                PField[PFP.Width + 1, i + 1] = new Block(true, BorderColor);
            }
            Console.SetCursorPosition(PFP.X, PFP.Y + PFP.Height);
            Console.Write(new string(BorderChar, 4 + PFP.Width * 2));
            for (int i = 0; i < PField.GetLength(0); i++)
                PField[i, PFP.Height + 1] = new Block(true, BorderColor);

            Console.SetCursorPosition(0, PFP.Y + PFP.Height + 3);
        }

        private static void DrawBlock(Piece piece, Vector pos, bool space = false)
        {
            Console.ForegroundColor = piece.Color;
            for (int i = 0; i < piece.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < piece.Matrix.GetLength(1); j++)
                {
                    if (piece.Matrix[i, j])
                    {
                        Console.SetCursorPosition(pos.X + j * 2 - 1, pos.Y + i - 1);
                        Console.Write(space ? new string(SpaceChar, 2) : new string(PieceChar, 2));
                        PField[j + (pos.X - PFP.X) / 2 - 1, i + pos.Y - PFP.Y] = new Block(!space, piece.Color);
                    }
                }
            }
        }

        private static void GetInputs()
        {
            while (IsRunning)
            {
                var input = Console.ReadKey();
                ConsoleKey key = input.Key;
                ConsoleModifiers specialKey = input.Modifiers;
                if ((key == ConsoleKey.LeftArrow || key == ConsoleKey.A))
                    LeftPressed = true;
                if ((key == ConsoleKey.DownArrow || key == ConsoleKey.S))
                    DownPressed = true;
                if ((key == ConsoleKey.RightArrow || key == ConsoleKey.D))
                    RightPressed = true;
                if (key == ConsoleKey.Spacebar)
                    HDPressed = true;
                if (key == ConsoleKey.J)
                    CWRotPressed = true;
                if (key == ConsoleKey.K)
                    CRotPressed = true;
                if ((int)specialKey % 2 == 0 && (int)specialKey % 4 != 0)
                    HandleControl();
            }
        }

        private static void HandleControl()
        {

            Console.SetCursorPosition(0, PFP.Y + PFP.Height + 3);
        }

        private static Random rng = new Random();

        public static List<Piece> Shuffle(List<Piece> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Piece value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

    }

}

