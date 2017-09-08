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
using System.Threading.Tasks;
using Android.Graphics;
using Com.Hitomi.Cmlibrary;
using Android.Support.V7.App;

namespace App6
{
    [Activity(Label = "MainPage", MainLauncher = true, Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainPage : AppCompatActivity, IOnMenuSelectedListener
    {
        string[] arrayName = {  "Analize", "Sign", "Register", "Contact","About" };
        private ImageView imgViewMain;
        private ImageView imgHazne1;
        private ImageView imgHazne2;
        private ImageView imgHazne3;
        private ImageView imgHazne4;


        private ImageView imgHazneA;
        private ImageView imgHazneB;
        private ImageView imgHazneC;
        private ImageView imgHazneD;
        public void OnMenuSelected(int p0)
        {
            Toast.MakeText(this, $"You selected {arrayName[p0]}", ToastLength.Long).Show();
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)View.SystemUiFlagHideNavigation;
            RootingAddres(p0);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MainMenu);

            imgHazne1 = FindViewById<ImageView>(Resource.Id.img1);
            imgHazne2 = FindViewById<ImageView>(Resource.Id.img2);
            imgHazne3 = FindViewById<ImageView>(Resource.Id.img3);
            imgHazne4 = FindViewById<ImageView>(Resource.Id.img4);

            imgHazne1.SetImageResource(Resource.Drawable.analize_1);
            imgHazne2.SetImageResource(Resource.Drawable.watch);
            imgHazne3.SetImageResource(Resource.Drawable.settings);
            imgHazne4.SetImageResource(Resource.Drawable.money);


            imgHazneA = FindViewById<ImageView>(Resource.Id.imageView1);
            imgHazneB = FindViewById<ImageView>(Resource.Id.imageView2);
            imgHazneC = FindViewById<ImageView>(Resource.Id.imageView3);
            imgHazneD = FindViewById<ImageView>(Resource.Id.imageView4);

            imgHazneA.SetImageResource(Resource.Drawable.a);
            imgHazneB.SetImageResource(Resource.Drawable.b);
            imgHazneC.SetImageResource(Resource.Drawable.c);
            imgHazneD.SetImageResource(Resource.Drawable.d);

            //imgViewMain = FindViewById<ImageView>(Resource.Id.imgMain);
            imgHazne2.Click += ImgHazne2_Click;
            var circle_menu = FindViewById<CircleMenu>(Resource.Id.circle_menu);


            circle_menu.SetMainMenu(Color.ParseColor("#CDCDCD"), Resource.Drawable.add, Resource.Drawable.remove)
                .AddSubMenu(Color.White, Resource.Drawable.analize2)
                .AddSubMenu(Color.ParseColor("#258CFF"), Resource.Drawable.sign_in)
                .AddSubMenu(Color.ParseColor("#6D4C41"), Resource.Drawable.tower)
                .AddSubMenu(Color.ParseColor("#1A237E"), Resource.Drawable.people_add)
                .AddSubMenu(Color.ParseColor("#b2e35a"), Resource.Drawable.people_ico)
                .AddSubMenu(Color.ParseColor("#002724"), Resource.Drawable.contact)

                .SetOnMenuSelectedListener(this);

            //imgViewMain.SetImageResource(Resource.Drawable.robotic);

           
        }

        private void ImgHazne2_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ActivityBeslenme));
        }

        public async Task RootingAddres(int take)
        {
            switch (arrayName[take])
            {
                case "Sign":
                    await Task.Delay(1050);
                    StartActivity(typeof(ActivitySignIn));
                    break;
                case "About":
                    await Task.Delay(1050);
                    StartActivity(typeof(MainActivity));
                    break;
                case "Analize":
                    await Task.Delay(1050);
                    StartActivity(typeof(MainActivity));
                    break;
                case "Register":
                    await Task.Delay(1050);
                    StartActivity(typeof(MainActivity));
                    break;
                case "Contact":
                    await Task.Delay(1050);
                    StartActivity(typeof(MainActivity));
                    break;
                default:
                    break;
            }
        }


    }
}