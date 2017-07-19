using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BiometricsPOC
{
    public delegate void WebViewNavigatingHandler(object sender, WebViewNavigationEventArgs args);
    public delegate void WebViewNavigatedHandler(object sender, WebViewNavigatedEventArgs args);

    public class WebViewWithArgs : WebView
    {
        public new event WebViewNavigatingHandler Navigating;
        public new event WebViewNavigatedHandler Navigated;

        public virtual void OnNavigating(WebViewNavigationEventArgs args)
        {
            Navigating?.Invoke(this, args);
        }

        public virtual void OnNavigated(WebViewNavigatedEventArgs args)
        {
            Navigated?.Invoke(this, args);
        }
    }

    public class WebViewNavigationEventArgs : EventArgs
    {
        public string Url { get; set; }
    }

    public class WebViewNavigatedEventArgs : WebViewNavigationEventArgs
    {
        public WebViewNavigationMode NavigationMode { get; set; }
    }

    public enum WebViewNavigationMode
    {
        Back,
        Forward,
        New,
        Refresh,
        Reset
    }

}