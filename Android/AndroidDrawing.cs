namespace Zebble.Plugin.Renderer
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using Android.Graphics;
    using Zebble.Device;
    using Olive;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public class AndroidDrawing : Android.Views.View
    {
        Drawing View;
        Canvas Result;

        public AndroidDrawing(Drawing view) : base(UIRuntime.CurrentActivity)
        {
            View = view;

            view.LineAdded.HandleOn(Thread.UI, Invalidate);
            view.PolygonAdded.HandleOn(Thread.UI, Invalidate);
            view.Cleared.HandleOn(Thread.UI, Invalidate);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            Result = canvas;

            View.Lines.Do(Draw);
            View.Polygons.Do(Draw);
        }

        void Draw(Drawing.Line line)
        {
            var paint = new Paint();
            var path = new Path();

            paint.SetStyle(Paint.Style.Fill);
            paint.Color = Color.Transparent;
            Result.DrawPaint(paint);

            path.MoveTo(Scale.ToDevice(line.Start.X), Scale.ToDevice(line.Start.Y));
            path.LineTo(Scale.ToDevice(line.End.X), Scale.ToDevice(line.End.Y));
            path.Close();

            paint.StrokeWidth = Scale.ToDevice(line.Thickness);
            paint.SetPathEffect(null);
            paint.Color = line.Color.Render();
            paint.SetStyle(Paint.Style.Stroke);

            Result.DrawPath(path, paint);
        }

        void Draw(Drawing.Polygon polygon)
        {
            polygon.Changed.HandleOn(Thread.UI, Invalidate);

            if (!polygon.Points.HasMany()) return;

            using (var paint = new Paint { Color = polygon.FillColor.Render() })
            {
                paint.SetStyle(Paint.Style.Fill);

                using (var path = new Path())
                {
                    path.MoveTo(Scale.ToDevice(polygon.Points.FirstOrDefault().X),
                        Scale.ToDevice(polygon.Points.FirstOrDefault().Y));

                    polygon.Points.Skip(1).Do(p => path.LineTo(Scale.ToDevice(p.X), Scale.ToDevice(p.Y)));

                    Result.DrawPath(path, paint);

                    paint.SetStyle(Paint.Style.Stroke);
                    paint.Color = polygon.LineColor.Render();
                    paint.StrokeWidth = Scale.ToDevice(polygon.LineThickness);
                    Result.DrawPath(path, paint);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            View = null;
            base.Dispose(disposing);
        }
    }
}