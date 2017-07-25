namespace Zebble
{
    using System;
    using Zebble.Plugin;

    public static class DrawingPluginExtensions
    {
        public static TView FillColor<TView>(this TView view, Color value)
            where TView : Drawing.Polygon
        {
            return view.Set(x => x.FillColor = value);
        }

        public static TView LineColor<TView>(this TView view, Color value)
            where TView : Drawing.Polygon
        {
            return view.Set(x => x.LineColor = value);
        }

        public static TView LineThickness<TView>(this TView view, float value)
            where TView : Drawing.Polygon
        {
            return view.Set(x => x.LineThickness = value);
        }

        /// <summary>E.g. (10,20) -> (40,0) -> (70,50%)</summary>
        public static TView PointsData<TView>(this TView view, string value)
            where TView : Drawing.Polygon
        {
            return view.Set(x => x.PointsData = value);
        }
    }
}
