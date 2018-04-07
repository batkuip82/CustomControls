using SkiaSharp;
using SkiaSharp.Extended.Iconify;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace CustomControls.Controls
{
    public class IconifyLabel : SKCanvasView
    {
        private static float _defaultFontSize = (float)Device.GetNamedSize(NamedSize.Default, typeof(Label));

        public readonly static BindableProperty TextProperty = BindableProperty.Create(
            propertyName: nameof(Text),
            returnType: typeof(string),
            declaringType: typeof(IconifyLabel));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public readonly static BindableProperty FontSizeProperty = BindableProperty.Create(
            propertyName: nameof(FontSize),
            returnType: typeof(float),
            declaringType: typeof(IconifyLabel),
            defaultValue: _defaultFontSize);

        public float FontSize
        {
            get { return (float)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public IconifyLabel()
        {
            PaintSurface += OnPainting;
        }

        private void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(SKColors.White);

            var size = CanvasSize;

            var fontAwesome = Text;

            using (var textPaint = new SKPaint())
            {
                textPaint.IsAntialias = true;
                textPaint.TextSize = FontSize;
                textPaint.Typeface = SKTypeface.FromFamilyName("Arial");

                // create the lookup table
                var lookup = new SKTextRunLookup();

                // add FontAwesome
                lookup.AddTypeface(new FontAwesomeLookupEntry());

                // it may be better to cache this using the:
                var runs = SKTextRun.Create(Text, lookup);
                var padding = 0;
                var yOffset = padding + textPaint.TextSize;

                SKRect textBounds = new SKRect();
                textPaint.MeasureText(Text, ref textBounds);
                float margin = (info.Width - textBounds.Width) / 2;

                float px = margin + textBounds.Width / 2;
                float py = margin + textBounds.Height / 2;

                canvas.DrawText(runs, padding, yOffset, textPaint);

                // the DrawIconifiedText method will re-calculate the text runs
                //canvas.DrawIconifiedText(Text, padding, yOffset, textPaint);
            }
        }

    }
}
