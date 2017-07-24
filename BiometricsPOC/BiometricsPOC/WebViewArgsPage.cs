using Plugin.Fingerprint.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BiometricsPOC
{
    public class WebViewArgsPage : ContentPage
    {
        private CancellationTokenSource _cancel;

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

                if (args.Url.Contains("payment-authenticate"))
                {
                    Debug.WriteLine("Calling Fingerprint scan...");
                    FingerprintAuthenticate();
                }
            };


            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { webView }
            };
        }

        private async void FingerprintAuthenticate()
        {
            await AuthenticateAsync("Please verify your fingerprint.");
        }

        private async Task AuthenticateAsync(string reason, string cancel = null, string fallback = null)
        {
            _cancel = new CancellationTokenSource();

            var dialogConfig = new AuthenticationRequestConfiguration(reason)
            {
                CancelTitle = "Cancel",
                FallbackTitle = "Use Alternate",
                AllowAlternativeAuthentication = false
            };

            var result = await Plugin.Fingerprint.CrossFingerprint.Current.AuthenticateAsync(dialogConfig, _cancel.Token);

            await SetResultAsync(result);
        }

        private async Task SetResultAsync(FingerprintAuthenticationResult result)
        {
            if (result.Authenticated)
            {
                var alert = Application.Current.MainPage.DisplayAlert("Fingerprint scan successful", "Your fingerprint scan was successful.", "OK");
            }
            else
            {
                Debug.WriteLine($"Error in Fingerprint Scan");
                Debug.WriteLine($"{result.ErrorMessage}");

                //var alert = Application.Current.MainPage.DisplayAlert("Fingerprint scan failed", $"{result.ErrorMessage}", "OK");
            }
        }
    }
}