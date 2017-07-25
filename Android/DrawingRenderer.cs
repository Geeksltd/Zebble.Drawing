namespace Zebble.Plugin.Renderer
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Zebble;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DrawingRenderer : INativeRenderer
    {
        Android.Views.View Result;

        public Task<Android.Views.View> Render(Renderer renderer)
        {
            Result = new AndroidDrawing((Drawing)renderer.View);
            return Task.FromResult(Result);
        }

        void IDisposable.Dispose()
        {
            Result?.Dispose();
            Result = null;
        }
    }
}