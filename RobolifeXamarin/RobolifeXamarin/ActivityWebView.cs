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
using Android.Webkit;

namespace App6
{
    [Activity(Label = "ActivityWebView")]
    public class ActivityWebView : Activity
    {
        

        private WebView webViewDene;
        private WebSettings webSettingsDene;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            

            SetContentView(Resource.Layout.WebView);


            webViewDene = FindViewById<WebView>(Resource.Id.webView1);
            webViewDene.Settings.JavaScriptEnabled = true;
            webViewDene.SetWebViewClient(new HelloWebViewClient());
            webViewDene.LoadUrl("http://kursatcakal.azurewebsites.net/");
            

        }
    }
    public class HelloWebViewClient :WebViewClient
    {

        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {

            view.LoadUrl(url);

            return false;

        }
    }
}