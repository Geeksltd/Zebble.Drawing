namespace Zebble.Plugin
{
    partial class Drawing
    {
        public class Line
        {
            public Point Start, End;
            public float Thickness = 1;
            public Color Color = Colors.Black;

            public Line() { }

            public Line(Point start, Point end) : this(start, end, 1) { }

            public Line(Point start, Point end, float thickness) : this(start, end, thickness, Colors.Transparent) { }

            public Line(Point start, Point end, float thickness, Color color)
            {
                Start = start;
                End = end;
                Thickness = thickness;
                Color = color;
            }
        }
    }
}