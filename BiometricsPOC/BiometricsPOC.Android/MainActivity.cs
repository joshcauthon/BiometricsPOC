using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using WebView = Android.Webkit.WebView;
using Intent = Android.Content.Intent;

namespace BiometricsPOC.Droid
{
    [Activity(Label = "BiometricsPOC", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [IntentFilter(new[] { Intent.ActionView }, Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable }, DataScheme = "g2g")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected WebView WebView { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            if (savedInstanceState != null)
            {
                WebView.RestoreState(savedInstanceState);
            }

            var intent = Intent;
            var uri = intent.DataString;

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(uri));
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            WebView?.SaveState(outState);
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);

            WebView?.RestoreState(savedInstanceState);
        }
    }
}

