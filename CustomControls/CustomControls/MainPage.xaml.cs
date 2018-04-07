using CustomControls.Pages;
using SkiaSharp;
using SkiaSharp.Extended.Iconify;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CustomControls
{
    public partial class MainPage : ContentPage
    {
        private Command<string> _navigateCommand;
        public Command<string> NavigateCommand
        {
            get => _navigateCommand ?? (_navigateCommand = new Command<string>(async (param) =>
            {
                Page page = null;

                switch (param)
                {
                    case "counterAnimation":
                        page = new CounterAnimationsPage();
                        break;
                }

                await Navigation.PushAsync(page);
            }));
        }


        public MainPage()
        {
            InitializeComponent();
        }

        private void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.Clear(SKColors.White);

            var fontAwesome = "I {{fa-heart-o color=ff0000}} to {{fa-code}} on {{fa-windows color=1BA1E2}}!";

            using (var textPaint = new SKPaint())
            {
                textPaint.IsAntialias = true;
                textPaint.TextSize = 48;
                textPaint.Typeface = SKTypeface.FromFamilyName("Arial");

                // the DrawIconifiedText method will re-calculate the text runs
                // it may be better to cache this using the:
                //     var runs = SKTextRun.Create(text, lookup);
                // and then drawing it using the DrawText method.
                var padding = 24;
                var yOffset = padding + textPaint.TextSize;

                canvas.DrawIconifiedText(fontAwesome, padding, yOffset, textPaint);
            }
        }
    }
}
