using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Point
    {
        public int Row;
        public int Col;
    }

    class Chess
    {
        static readonly int FieldSize = 7;
        static readonly int FieldColumns = 8;
        static readonly int FieldRows = 8;
        static Figure[,] Playfield = new Figure[FieldRows, FieldColumns];
        public static void Start()
        {
            InitializeDefaultPlayfield();
            GameLoop();
            Console.ReadKey();
        }


        private static void GameLoop()
        {
            int winner = 0;
            while (winner == 0)
            {
                DrawPlayfield();
                MoveFigure(Console.ReadLine().ToLower());
                Console.Clear();
            }

        }

        private static void MoveFigure(string move)
        {
            try
            {
                Point source = new Point()
                {
                    Col = move[0] - 97,
                    Row = 8 - int.Parse(move[1].ToString())
                };

                Point destination = new Point()
                {
                    Col = move[3] - 97,
                    Row = 8 - int.Parse(move[4].ToString())
                };

                if (Playfield[source.Row, source.Col].ID != 0
                    && (Playfield[destination.Row, destination.Col].Color != Playfield[source.Row, source.Col].Color
                        || Playfield[destination.Row, destination.Col].ID == 0))
                {
                    Playfield[destination.Row, destination.Col] = Playfield[source.Row, source.Col];
                    Playfield[source.Row, source.Col] = new Figure(0);
                }
            }
            catch
            {
            }
        }

        static void DrawPlayfield()
        {
            Console.WindowHeight = FieldSize * FieldRows + 4;
            Console.WindowWidth = FieldSize * 2 * FieldColumns + 15;
            for (int j = 0; j < FieldRows; j++)
            {
                for (int i = 0; i < FieldColumns; i++)
                {
                    for (int k = 0; k < FieldSize; k++)
                        for (int l = 0; l < FieldSize * 2; l++)
                        {
                            Console.SetCursorPosition(i * FieldSize * 2 + l, j * FieldSize + k);
                            Console.BackgroundColor = (i + j) % 2 == 0 ? ConsoleColor.White : ConsoleColor.Black;
                            if (k > 0
                                && k < FieldSize - 1
                                && l > 0
                                && l < FieldSize * 2 - 1)
                            {
                                Console.ForegroundColor = Playfield[j, i].Color == true ? ConsoleColor.Green : ConsoleColor.Red;
                                Console.Write(Playfield[j, i].Icon[k - 1, l - 1] == true ? '█' : ' ');
                            }
                            else
                            {

                                Console.Write(" ");
                            }
                        }
                    Console.WriteLine();
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void InitializeDefaultPlayfield()
        {
            Playfield = new Figure[,]
            {
                {
                    new Figure((FigID)3, false),
                    new Figure((FigID)4, false),
                    new Figure((FigID)5, false),
                    new Figure((FigID)2, false),
                    new Figure((FigID)1, false),
                    new Figure((FigID)5, false),
                    new Figure((FigID)4, false),
                    new Figure((FigID)3, false)
                },
                {
                    new Figure((FigID)6, false),
                    new Figure((FigID)6, false),
                    new Figure((FigID)6, false),
                    new Figure((FigID)6, false),
                    new Figure((FigID)6, false),
                    new Figure((FigID)6, false),
                    new Figure((FigID)6, false),
                    new Figure((FigID)6, false)
                },
                {
                    new Figure(0),
                    new Figure(0),
                    new Figure(0),
                    new Figure(0),
                    new Figure(0),
                    new Figure(0),
                    new Figure(0),
                    new Figure(0)
                },
                {
                    new Figure(0),
                    new Figure(0),
                    new Figure(0),
                    new Figure(0),
                    new Figure(0),
                    new Figure(0),
                    new Figure(0),
                    new Figure(0)
                },
                {
                    new Figure(0),
                    new Figure(0),
                    new Figure(0),
                    new Figure(0),
                    new Figure(0),
                    new Figure(0),
                    new Figure(0),
                    new Figure(0)
                },
                {
                    new Figure(0),
                    new Figure(0),
                    new Figure(0),
                    new Figure(0),
                    new Figure(0),
                    new Figure(0),
                    new Figure(0),
                    new Figure(0)
                },
                {
                    new Figure((FigID)6, true),
                    new Figure((FigID)6, true),
                    new Figure((FigID)6, true),
                    new Figure((FigID)6, true),
                    new Figure((FigID)6, true),
                    new Figure((FigID)6, true),
                    new Figure((FigID)6, true),
                    new Figure((FigID)6, true)
                
                },
                {
                    new Figure((FigID)3, true),
                    new Figure((FigID)4, true),
                    new Figure((FigID)5, true),
                    new Figure((FigID)2, true),
                    new Figure((FigID)1, true),
                    new Figure((FigID)5, true),
                    new Figure((FigID)4, true),
                    new Figure((FigID)3, true)
                }
                               
            };
        }
    }
}
