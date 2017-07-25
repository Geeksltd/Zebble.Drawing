namespace Zebble.Plugin.Renderer
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Zebble;
    using UIKit;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DrawingRenderer : INativeRenderer
    {
        UIView Result;

        public Task<UIView> Render(Renderer renderer)
        {
            Result = new IosDrawing((Drawing)renderer.View);
            return Task.FromResult(Result);
        }

        void IDisposable.Dispose()
        {
            Result?.Dispose();
            Result = null;
        }
    }
}