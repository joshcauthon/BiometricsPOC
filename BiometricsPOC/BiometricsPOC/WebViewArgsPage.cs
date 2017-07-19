using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BiometricsPOC
{
    public class WebViewArgsPage : ContentPage
    {
        public WebViewArgsPage(string url)
        {
            var source = Helpers.Helpers.HandleUrl(url);

            var webView = new WebViewWithArgs
            {
                Source = source,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            webView.Navigating += (sender, args) =>
            {
                Debug.WriteLine("Navigating event hit...");
                Debug.WriteLine($"URL: {args.Url}");
            };

            webView.Navigated += (sender, args) =>
            {
                Debug.WriteLine("Navigated event hit...");
                Debug.WriteLine($"URL: {args.Url}");
            };

        
            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { webView }
            };
        }
    }
}