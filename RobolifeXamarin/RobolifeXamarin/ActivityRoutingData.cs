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
using Android.Media;
using System.Threading;
using System.Threading.Tasks;
using Android.Views.Animations;
using Com.Ebanx.Swipebtn;
using Android.Support.V7.App;
using Newtonsoft.Json;

namespace App6
{
    [Activity(Label = "ActivityRoutingData", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class ActivityRoutingData : AppCompatActivity, IOnStateChangeListener
    {
    
        private int pgDurum;
        private ImageView imgLeftCorner;
        private ImageView imgGo1;
        private ImageView imgGo2;
        private SwipeButton swipeNutration;
        private Button btn;
        Model3D gelen3DModel;
        private TextView txtLength;
        private TextView txtShoulds;
        private TextView txtNeck;
        private TextView txtWaist;
        private TextView txtHips;
        private TextView txtTihgs;
        private TextView txtKnee;
        private TextView txtCalves;
        private TextView txtBiceps;
        private TextView txtChest;

        private TextView txtEndomorph;
        private TextView txtEktomorph;
        private TextView txtMesomorph;
        //public void OnStateChange(bool p0)
        //{
        //    //throw new NotImplementedException();
        //}
        public void OnStateChange(bool p0)
        {
            Toast.MakeText(this, "Active" + p0, ToastLength.Short).Show();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.RouteDataResult);

            imgLeftCorner = FindViewById<ImageView>(Resource.Id.imageView1);
            gelen3DModel = JsonConvert.DeserializeObject<Model3D>(Intent.GetStringExtra("3D_Model"));
            //imgGo1 = FindViewById<ImageView>(Resource.Id.imageView2);
            //imgGo2 = FindViewById<ImageView>(Resource.Id.imageView3);
            imgLeftCorner.SetImageResource(Resource.Drawable.user_ico);
            //imgGo1.SetImageResource(Resource.Drawable.go);
            //imgGo2.SetImageResource(Resource.Drawable.go);
            //imgGo1.Click += ImgGo1_Click;

            txtLength = FindViewById<TextView>(Resource.Id.rdLength);
            txtShoulds = FindViewById<TextView>(Resource.Id.rdShoulds);
            txtNeck = FindViewById<TextView>(Resource.Id.rdNeck);
            txtTihgs = FindViewById<TextView>(Resource.Id.rdTihgs);
            txtWaist = FindViewById<TextView>(Resource.Id.rdWeist);
            txtKnee = FindViewById<TextView>(Resource.Id.rdKnee);
            txtBiceps = FindViewById<TextView>(Resource.Id.rdBiceps);
            txtChest = FindViewById<TextView>(Resource.Id.rdChest);
            txtCalves = FindViewById<TextView>(Resource.Id.rdKalves);
            txtHips = FindViewById<TextView>(Resource.Id.rdHips);

            txtLength.Text = gelen3DModel.Boy.ToString();
            txtShoulds.Text = gelen3DModel.Omuz.ToString();
            txtNeck.Text = gelen3DModel.Neck.ToString();
            txtTihgs.Text = gelen3DModel.Tighs.ToString();
            txtWaist.Text = gelen3DModel.Karin.ToString();
            txtKnee.Text = gelen3DModel.Knee.ToString();
            txtBiceps.Text =(gelen3DModel.Knee/2).ToString();
            txtHips.Text = gelen3DModel.Hips.ToString();
            txtCalves.Text = gelen3DModel.Calves.ToString();
            txtChest.Text = gelen3DModel.Chest.ToString();



            txtEktomorph = FindViewById<TextView>(Resource.Id.txtEktomorph);
            txtEndomorph = FindViewById<TextView>(Resource.Id.txtEndomorph);
            txtMesomorph= FindViewById<TextView>(Resource.Id.txtMesomorph);

            txtEktomorph.Text = "%73,3";
            txtEndomorph.Text = "%26,7";
            txtMesomorph.Text = "%0";









            swipeNutration = FindViewById<SwipeButton>(Resource.Id.swipe_btn_son);
            swipeNutration.SetOnStateChangeListener(this);

            imageAnimation();






        }
        public async Task imageAnimation() {

           
            Animation anmLeft = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.fade_in_animation);
            imgLeftCorner.StartAnimation(anmLeft);
            Animation anmLeft2 = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.fade_in_animation);
            imgGo1.StartAnimation(anmLeft2);

        }

      

        //private void ImgGo1_Click(object sender, EventArgs e)
        //{
        //    ProgressDialog progressbar = new ProgressDialog(this);

        //    progressbar.SetCancelable(true);


        //   progressbar.SetMessage("Veriler İşleniyor.");

        //    progressbar.SetProgressStyle(ProgressDialogStyle.Horizontal);
        //    progressbar.Progress = 0;
        //    progressbar.Max = 100;
        //    progressbar.Show();
        //    pgDurum = 0;
        //    new System.Threading.Thread(new ThreadStart(delegate
        //    {
        //        while (pgDurum < 100)
        //        {
        //            pgDurum += 10;
        //            progressbar.Progress = pgDurum;
        //            Java.Lang.Thread.Sleep(100);
        //        }
        //        RunOnUiThread(() => { progressbar.SetMessage("Sonuçlar Hazirlandi."); });

        //    })).Start();




        //}

    }
}