namespace Zebble.Plugin.Renderer
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Windows.UI.Xaml;
    using Zebble;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DrawingRenderer : INativeRenderer
    {
        public Task<FrameworkElement> Render(Renderer renderer)
        {
            FrameworkElement result = new UWPDrawing(renderer);
            return Task.FromResult(result);
        }

        void IDisposable.Dispose() { }
    }
}