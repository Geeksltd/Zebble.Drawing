namespace Zebble.Plugin
{
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using Zebble;

    public partial class Drawing : Canvas, IRenderedBy<Renderer.DrawingRenderer>
    {
        public readonly ConcurrentList<Line> Lines = new ConcurrentList<Line>();

        public readonly ConcurrentList<Polygon> Polygons = new ConcurrentList<Polygon>();

        public readonly AsyncEvent<Line> LineAdded = new AsyncEvent<Line>();

        public readonly AsyncEvent<Polygon> PolygonAdded = new AsyncEvent<Polygon>();

        public readonly AsyncEvent Cleared = new AsyncEvent();

        public Task Add(Line line)
        {
            Lines.Add(line);
            return LineAdded.Raise(line);
        }

        public Task Add(Polygon line)
        {
            Polygons.Add(line);
            return PolygonAdded.Raise(line);
        }

        public Task Clear()
        {
            Lines.Clear();
            return Cleared.Raise();
        }

        public override void Dispose()
        {
            Cleared?.Dispose();
            LineAdded?.Dispose();
            PolygonAdded?.Dispose();

            base.Dispose();
        }
    }
}