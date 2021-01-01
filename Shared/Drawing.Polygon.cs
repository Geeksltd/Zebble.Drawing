namespace Zebble.Plugin
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Olive;

    partial class Drawing
    {
        [EscapeGCop("X and Y are actually good names here.")]
        public class Polygon
        {
            Color lineColor = Colors.Black;
            Color fillColor = Colors.Transparent;
            float lineThickness = 1;
            public string Id;
            public readonly AsyncEvent Changed = new AsyncEvent();

            public ConcurrentList<Point> Points { get; set; }

            public Color LineColor { get => lineColor; set { lineColor = value; Changed.Raise(); } }

            public Color FillColor { get => fillColor; set { fillColor = value; Changed.Raise(); } }

            public float LineThickness { get => lineThickness; set { lineThickness = value; Changed.Raise(); } }

            public Polygon(params Point[] points) { Points = new ConcurrentList<Point>(points); }

            public async Task<Point> Add(Point point)
            {
                Points.Add(point);
                await Changed.Raise();
                return point;
            }

            public Task<Point> Add(float x, float y) => Add(new Point(x, y));

            /// <summary>E.g. (10,20) -> (40,0) -> (70,50%)</summary>
            public string PointsData
            {
                get => Points.ToString(" -> ");
                set
                {
                    var items = value.Split(new[] { "->" }, StringSplitOptions.RemoveEmptyEntries)
                        .Trim().Select(Point.Parse);

                    Points = new ConcurrentList<Point>(items);
                    Changed.Raise();
                }
            }
        }
    }
}