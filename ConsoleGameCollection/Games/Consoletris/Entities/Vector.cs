namespace Consoletris.Entities
{
    class Vector
    {
        public Vector()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
        }
        public Vector(int x, int y)
        {
            X = x;
            Y = y;
            Width = 0;
            Height = 0;
        }
        public Vector(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public int Width { get; set; }
        public int Height{ get; set; }
    }
}
