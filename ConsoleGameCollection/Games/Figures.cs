using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess {
    public enum FigID {
        Nothing,
        FKing,
        FQueen,
        FRook,
        FKnight,
        FBishop,
        FPawn,
    }

    class Figure {
        public int ID;
        public char Symbol;
        public int Value;
        public bool[,] Icon;
        public bool Color; //true = white;

        public Figure(FigID id, bool color = true) {
            Color = color;
            switch (id)
            {
                case FigID.Nothing:
                    ID = 0;
                    Symbol = ' ';
                    Value = 0;
                    Icon = new bool[5, 12];
                    break;

                case FigID.FKing:
                    ID = 1;
                    Symbol = 'K';
                    Value = 0;
                    Icon = new bool[,]
                    {
                        {false,false,false,false,false,true ,true ,false,false,false,false,false,false },
                        {false,true ,true ,false,true ,true ,true ,true ,false,true ,true ,false,false },
                        {false,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,false,false },
                        {false,false,true ,true ,false,false,false,false,true ,true ,false,false,false },
                        {false,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,false,false },
                    };
                    break;

                case FigID.FQueen:
                    ID = 2;
                    Symbol = 'Q';
                    Value = 9;
                    Icon = new bool[,]
                    {
                        {false,true ,false,false,true ,false,false,true ,false,false,true ,false,false },
                        {false,false,true ,false,true ,false,false,true ,false,true ,false,false,false },
                        {false,false,true ,true ,true ,true ,true ,true ,true ,true ,false,false,false },
                        {false,false,false,true ,false,false,false,false,true ,false,false,false,false },
                        {false,false,true ,true ,true ,true ,true ,true ,true ,true ,false,false,false },
                    };
                    break;

                case FigID.FRook:
                    ID = 3;
                    Symbol = 'R';
                    Value = 5;
                    Icon = new bool[,]
                    {
                        {false,false,true ,false,true ,false,false,true ,false,true ,false,false},
                        {false,false,true ,true ,true ,true ,true ,true ,true ,true ,false,false},
                        {false,false,false,true ,true ,true ,true ,true ,true ,false,false,false},
                        {false,false,false,true ,true ,true ,true ,true ,true ,false,false,false},
                        {false,false,true ,true ,true ,true ,true ,true ,true ,true ,false,false},
                    };
                    break;

                case FigID.FKnight:
                    ID = 4;
                    Symbol = 'N';
                    Value = 3;
                    Icon = new bool[,]
                    {
                        {false,false,false,false,true ,true ,true ,true ,true ,false,false,false},
                        {false,false,false,true ,true ,true ,false,true ,true ,true ,false,false},
                        {false,false,true ,true ,false,false,true ,true ,true ,true ,false,false},
                        {false,false,false,false,false,true ,true ,true ,true ,true ,false,false},
                        {false,false,false,true ,true ,true ,true ,true ,true ,true ,true ,false},
                    };
                    break;

                case FigID.FBishop:
                    ID = 5;
                    Symbol = 'B';
                    Value = 3;
                    Icon = new bool[,]
                    {
                        {false,false,false,false,false,true ,true ,false,false,false,false,false},
                        {false,false,false,false,true ,true ,true ,true ,false,false,false,false},
                        {false,false,false,true ,true ,false,false,true ,true ,false,false,false},
                        {false,false,false,false,true ,true ,true ,true ,false,false,false,false},
                        {false,false,true ,true ,true ,true ,true ,true ,true ,true ,false,false},
                    };
                    break;

                case FigID.FPawn:
                    ID = 6;
                    Symbol = 'P';
                    Value = 1;
                    Icon = new bool[,]
                    {
                        {false,false,false,false,false,false,false,false,false,false,false,false},
                        {false,false,false,false,false,true ,true ,false,false,false,false,false},
                        {false,false,false,false,true ,true ,true ,true ,false,false,false,false},
                        {false,false,false,false,false,true ,true ,false,false,false,false,false},
                        {false,false,false,true ,true ,true ,true ,true ,true ,false,false,false},
                    };
                    break;

                default:
                    break;
            }
        }
        public void FKing() {

        }
        public void FQueen() {

        }
        public void FRook() {

        }
        public void FKnight() {

        }
        public void FBishop() {

        }
        public void FPawn() {

        }

    }
}
