using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using WebView = Xamarin.Forms.WebView;
using Android.Webkit;
using Android.Graphics;
using BiometricsPOC.Droid;

[assembly: ExportRenderer(typeof(BiometricsPOC.WebViewWithArgs), typeof(WebViewWithArgsRenderer))]
namespace BiometricsPOC.Droid
{
    public class WebViewWithArgsRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.SetWebViewClient(new WebViewWithArgsClient(WebViewWithArgs));
                SetupControlSettings();
            }
        }

        public WebViewWithArgs WebViewWithArgs
        {
            get { return Element as WebViewWithArgs; }
        }

        private void SetupControlSettings()
        {
            Control.Settings.JavaScriptEnabled = true;
            Control.Settings.DomStorageEnabled = true;
        }
    }

    internal class WebViewWithArgsClient : WebViewClient
    {
        private readonly WebViewWithArgs _webViewWithArgs;

        internal WebViewWithArgsClient(WebViewWithArgs webViewWithArgs)
        {
            _webViewWithArgs = webViewWithArgs;
        }

        public override void OnPageStarted(Android.Webkit.WebView view, string url, Bitmap favicon)
        {
            base.OnPageStarted(view, url, favicon);

            _webViewWithArgs.OnNavigating(new WebViewNavigationEventArgs
            {
                Url = url
            });
        }

        public override void OnPageFinished(Android.Webkit.WebView view, string url)
        {
            base.OnPageFinished(view, url);

            _webViewWithArgs.OnNavigated(new WebViewNavigatedEventArgs
            {
                Url = url
            });         
        }
    }
}