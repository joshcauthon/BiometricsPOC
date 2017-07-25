using BiometricsPOC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using System.Diagnostics;

namespace BiometricsPOC
{
    public partial class App : Application
    {
        public App(string url)
        {
            InitializeComponent();

            MainPage = new WebViewArgsPage(url);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes

            Debug.WriteLine("OnResume hit...");

            if (Device.RuntimePlatform == Device.iOS)
            {
                if (!string.IsNullOrEmpty(Settings.RedirectUrl))
                {
                    var url = Helpers.Helpers.HandleUrl(Settings.RedirectUrl);

                    Settings.RedirectUrl = string.Empty;

                    MainPage = new WebViewArgsPage(url);
                }
            }
        }
    }
}
