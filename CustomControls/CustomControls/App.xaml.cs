using CustomControls.Pages;
using SkiaSharp.Extended.Iconify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CustomControls
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            // register the default fonts that we want
            SKTextRunLookup.Instance.AddFontAwesome();

            MainPage = new CustomControls.MainPage();
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
