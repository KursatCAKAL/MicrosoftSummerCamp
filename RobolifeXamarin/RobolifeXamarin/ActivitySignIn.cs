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
using Android.Support.V7.App;
using Morxander.Zaman;

namespace App6
{
    [Activity(Label = "ActivitySignIn",Icon="@drawable/icon",Theme ="@style/Theme.AppCompat.Light.NoActionBar")]
    public class ActivitySignIn : AppCompatActivity
    {
        ZamanTextView textViewDeneme;
        ZamanTextView textViewDeneme2;
        EditText myText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Sign_In);

            //textViewDeneme = FindViewById<ZamanTextView>(Resource.Id.txtZaman1);
         //  textViewDeneme2 = FindViewById<ZamanTextView>(Resource.Id.txtZaman2);
         //    < LinearLayout
         //   android: orientation = "horizontal"
         //   android: minWidth = "25px"
         //   android: minHeight = "25px"
         //   android: layout_width = "match_parent"
         //   android: layout_height = "39.0dp"
         //   android: id = "@+id/linearLayout2" >
 
         //    < TextView
         //       android: text = "Password:"
         //       android: textAppearance = "?android:attr/textAppearanceMedium"
         //       android: layout_width = "wrap_content"
         //       android: layout_height = "match_parent"
         //       android: id = "@+id/textView2" />
 
         //    < android.support.design.widget.TextInputEditText
         //       android: id = "@+id/txtZaman2"
         //       android: hint = "Hour"
         //       android: layout_width = "260.0dp"
         //       android: layout_height = "35.5dp"
         //       android: textSize = "30sp"
         //       android: drawableStart = "@drawable/ic_person_black_24dp"
         //       android: drawableLeft = "@drawable/ic_person_black_24dp" />
 
         //</ LinearLayout >
        }
    }
}