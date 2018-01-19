namespace Zebble.Plugin.Renderer
{
    using System;
    using foundation = Windows.Foundation;
    using xaml = Windows.UI.Xaml;
    using Zebble.UWP;

    class UWPDrawing : UWPCanvas
    {
        Zebble.Renderer Renderer;
        Drawing View;

        public UWPDrawing(Zebble.Renderer renderer) : base(renderer)
        {
            Renderer = renderer;
            View = (Drawing)renderer.View;

            View.LineAdded.HandleOn(Thread.UI, l => Draw(l));
            View.PolygonAdded.HandleOn(Thread.UI, p => Draw(p));
            View.Cleared.HandleOn(Thread.UI, () => Children.Clear());

            View.Lines.Do(Draw);
            View.Polygons.Do(Draw);
        }

        void Draw(Drawing.Line line)
        {
            Children.Add(new xaml.Shapes.Line
            {
                Stroke = line.Color.RenderBrush(),
                X1 = line.Start.X,
                X2 = line.End.X,
                Y1 = line.Start.Y,
                Y2 = line.End.Y,
                HorizontalAlignment = xaml.HorizontalAlignment.Left,
                VerticalAlignment = xaml.VerticalAlignment.Center,
                StrokeThickness = line.Thickness
            });
        }

        void Draw(Drawing.Polygon polygon)
        {
            var result = new xaml.Shapes.Polygon
            {
                Stroke = polygon.LineColor.RenderBrush(),
                StrokeThickness = polygon.LineThickness
            };

            foreach (var point in polygon.Points)
                result.Points.Add(new foundation.Point(point.X, point.Y));

            if (polygon.FillColor != null && !polygon.FillColor.IsTransparent())
                result.Fill = polygon.FillColor.RenderBrush();

            Children.Add(result);
        }
    }
}