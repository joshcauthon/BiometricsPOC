﻿using BiometricsPOC.Helpers;
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

            //This is used when using the g2g links on iOS, as it uses the OpenURL() then comes to OnResume
            if (Device.RuntimePlatform == Device.iOS)
            {
                if (!string.IsNullOrEmpty(Settings.RedirectUrl))
                {
                    var url = Helpers.Helpers.HandleUrl(Settings.RedirectUrl);

                    //clears out redirectUrl as it is no longer needed, 
                    //also helps keep iOS from reloading the page when hitting OnResume()
                    Settings.RedirectUrl = string.Empty;

                    MainPage = new WebViewArgsPage(url);
                }
            }
        }
    }
}
