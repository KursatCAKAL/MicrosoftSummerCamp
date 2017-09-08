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
using Com.Ebanx.Swipebtn;
using Android.Support.V7.App;

namespace App6
{
    [Activity(Label = "ActivityTrying",Icon ="@drawable/icon", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class ActivityTrying : AppCompatActivity,IOnStateChangeListener
    {
        //SwipeButton deneme;

        public void OnStateChange(bool p0)
        {
            Toast.MakeText(this, "Active" + p0, ToastLength.Short).Show();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.trying);

            //var deneme = FindViewById<SwipeButton>(Resource.Id.swipedeneme);

            //deneme.SetOnStateChangeListener(this);
        }
    }
}