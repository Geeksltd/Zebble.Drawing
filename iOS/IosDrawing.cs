namespace Zebble.Plugin
{
    using System;
    using Zebble;
    using System.ComponentModel;
    using System.Linq;
    using CoreGraphics;
    using UIKit;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public class IosDrawing : UIView
    {
        Drawing View;

        public IosDrawing(Drawing view)
        {
            View = view;

            view.LineAdded.HandleOn(Device.UIThread, () => SetNeedsDisplay());
            view.PolygonAdded.HandleOn(Device.UIThread, () => SetNeedsDisplay());
            view.Cleared.HandleOn(Device.UIThread, () => SetNeedsDisplay());
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            using (var graph = UIGraphics.GetCurrentContext())
            {
                DrawLines(graph);
                View.Polygons.Do(p => DrawPolygon(p, graph));
            }
        }

        void DrawLines(CGContext graph)
        {
            var path = new CGPath();

            foreach (var line in View.Lines)
            {
                //set up drawing attributes
                graph.SetLineWidth(line.Thickness);
                line.Color.Render().SetStroke();

                var start = new CGPoint(line.Start.X, line.Start.Y);
                var end = new CGPoint(line.End.X, line.End.Y);

                path.AddLines(new[] { start, end });

                graph.AddPath(path);
                graph.DrawPath(CGPathDrawingMode.Stroke);
            }
        }

        void DrawPolygon(Drawing.Polygon polygon, CGContext graph)
        {
            polygon.Changed.HandleOn(Device.UIThread, () => SetNeedsDisplay());

            //set up drawing attributes
            graph.SetLineWidth(polygon.LineThickness);

            polygon.FillColor.Render().SetFill();
            polygon.LineColor.Render().SetStroke();

            //create geometry
            var path = new CGPath();
            path.AddLines(polygon.Points.Select(p => p.Render()).ToArray());
            path.CloseSubpath();

            //add geometry to graphics context and draw it
            graph.AddPath(path);
            graph.DrawPath(CGPathDrawingMode.FillStroke);
        }

        protected override void Dispose(bool disposing)
        {
            View = null;
            base.Dispose(disposing);
        }
    }
}