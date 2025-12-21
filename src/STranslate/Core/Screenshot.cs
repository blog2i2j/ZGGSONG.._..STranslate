using ScreenGrab;
using System.Drawing;

namespace STranslate.Core;

public class Screenshot(Settings settings) : IScreenshot
{
    public Bitmap? GetScreenshot()
    {
        if (ScreenGrabber.IsCapturing)
            return default;
        var bitmap = ScreenGrabber.CaptureDialog(settings.ShowScreenshotAuxiliaryLines);
        if (bitmap == null)
            return default;
        return bitmap;
    }

    public async Task<Bitmap?> GetScreenshotAsync()
    {
        if (ScreenGrabber.IsCapturing)
            return default;
        var bitmap = await ScreenGrabber.CaptureAsync(settings.ShowScreenshotAuxiliaryLines);
        if (bitmap == null)
            return default;
        return bitmap;
    }
}
