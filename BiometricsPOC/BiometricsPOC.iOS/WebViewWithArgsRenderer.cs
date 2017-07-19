using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using BiometricsPOC;
using BiometricsPOC.iOS;
using System.Diagnostics;

[assembly: ExportRenderer(typeof(WebViewWithArgs), typeof(WebViewWithArgsRenderer))]
namespace BiometricsPOC.iOS
{
    public class WebViewWithArgsRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Delegate = new WebViewDelegate(WebViewWithArgs);
            }
        }

        public WebViewWithArgs WebViewWithArgs
        {
            get { return Element as WebViewWithArgs; }
        }
    }

    internal class WebViewDelegate : UIWebViewDelegate
    {
        private WebViewWithArgs _webViewWithArgs;

        public WebViewDelegate(WebViewWithArgs webViewWithArgs)
        {
            _webViewWithArgs = webViewWithArgs;
        }

        [Export("webView:shouldStartLoadWithRequest:navigationType:")]
        public override bool ShouldStartLoad(UIWebView webView, NSUrlRequest request, UIWebViewNavigationType navigationType)
        {
            webView.UserInteractionEnabled = true;

            _webViewWithArgs.OnNavigating(new WebViewNavigationEventArgs
            {
                Url = request.Url.AbsoluteString
            });

            return true;
        }

        public override void LoadStarted(UIWebView webView)
        {

        }

        public override void LoadFailed(UIWebView webView, NSError error)
        {
            Debug.WriteLine($"Error: {error.ToString()}");
        }

        [Export("webViewDidFinishLoad:")]
        public override void LoadingFinished(UIWebView webView)
        {
            webView.UserInteractionEnabled = true;

            _webViewWithArgs.OnNavigated(new WebViewNavigatedEventArgs
            {
                Url = webView.Request.Url.AbsoluteString
            });
        }
    }
}