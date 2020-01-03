using Consoletris.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consoletris
{
    class Blocks
    {
        public static void InitializeBlocks()
        {
            // T-piece
            Consoletris.Pieces.Add(new Piece() { Matrix = new bool[,] { 
                { false, true, false }, 
                { true, true, true }, 
                { false, false, false }
            }, Color = Color.FromArgb(204, 0, 255) });
            //L-Piece
            Consoletris.Pieces.Add(new Piece() { Matrix = new bool[,] { 
                { false, false, true }, 
                { true, true, true }, 
                { false, false, false }
            }, Color = Color.FromArgb(255, 153, 0) });
            //J-Piece
            Consoletris.Pieces.Add(new Piece() { Matrix = new bool[,] { 
                { true, false, false }, 
                { true, true, true }, 
                { false, false, false }
            }, Color = Color.FromArgb(0, 0, 255) });
            //S-Piece
            Consoletris.Pieces.Add(new Piece() { Matrix = new bool[,] { 
                { false, true, true }, 
                { true, true, false }, 
                { false, false, false }
            }, Color = Color.FromArgb(0, 255, 0) });
            //Z-Piece
            Consoletris.Pieces.Add(new Piece() { Matrix = new bool[,] { 
                { true, true, false }, 
                { false, true, true }, 
                { false, false, false }
            }, Color = Color.FromArgb(255, 0, 0) });
            //I-Piece
            Consoletris.Pieces.Add(new Piece() { Matrix = new bool[,] { 
                { false, false, false, false }, 
                { true, true, true, true }, 
                { false, false, false, false }, 
                { false, false, false, false }
            }, Color = Color.FromArgb(0, 204, 255) });
            //O-Piece
            Consoletris.Pieces.Add(new Piece() { Matrix = new bool[,] { 
                { true, true,  }, 
                { true, true,  } 
            }, Color = Color.FromArgb(255, 255, 0) });
        }

    }
}
