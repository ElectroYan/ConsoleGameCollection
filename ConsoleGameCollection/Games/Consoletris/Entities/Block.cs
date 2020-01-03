using System.Drawing;

namespace Consoletris.Entities
{
    class Block
    {
        public Block(bool exists, Color color)
        {
            Exists = exists;
            Color = color;
        }
        public bool Exists;
        public Color Color;
    }
}
