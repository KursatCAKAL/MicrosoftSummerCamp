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
using Newtonsoft.Json;
using Java.Lang;
using System.Threading;
using Android.Webkit;

namespace App6
{
    [Activity(Label = "ImageProccessing", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class Activity1 : Activity
    {
        int pgDurum;
        private Model3D modelGelen;


      


        private TextView txtViewBoy;
        private TextView txtViewKilo;
        private TextView txtViewKarin;
        private TextView txtViewGogus;
        private TextView txtViewOmuz;
        private TextView txtViewBoyKontrol;

        private SeekBar seekbarBoyTut;
        private SeekBar seekbarKarin;
        private SeekBar seekbarKilo;
        private SeekBar seekbarOmuz;
        private SeekBar seekbarGogus;
        private SeekBar seekbarControlBoy;

        private Button veriAnalizButonu;
        private Button blogGitButton;
        private Button routingPage;
     

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Second);
            string a, b, c;
            modelGelen = JsonConvert.DeserializeObject<Model3D>(Intent.GetStringExtra("3D_Model"));
            a = modelGelen.Boy.ToString();
            b = modelGelen.Karin.ToString();
            c = modelGelen.Kilo.ToString();
            //Tanımlamış olduğun elemanları initialize etmeyi unutma.
            txtViewBoy = FindViewById<TextView>(Resource.Id.textView2);
            txtViewKilo = FindViewById<TextView>(Resource.Id.textView4);
            txtViewKarin = FindViewById<TextView>(Resource.Id.textView6);
            txtViewGogus= FindViewById<TextView>(Resource.Id.textView8);
            txtViewOmuz = FindViewById<TextView>(Resource.Id.textView10);
            txtViewBoyKontrol=FindViewById<TextView>(Resource.Id.textView12);

            seekbarBoyTut = FindViewById<SeekBar>(Resource.Id.seekBarBoy);
            seekbarKarin = FindViewById<SeekBar>(Resource.Id.seekBar2);
            seekbarKilo = FindViewById<SeekBar>(Resource.Id.seekBar3);
            seekbarGogus = FindViewById<SeekBar>(Resource.Id.seekBar4);
            seekbarOmuz = FindViewById<SeekBar>(Resource.Id.seekBar5);



            veriAnalizButonu = FindViewById<Button>(Resource.Id.btnVeriAnaliz);

            blogGitButton = FindViewById<Button>(Resource.Id.btnBlogGit);
            blogGitButton.Click += BlogGitButton_Click;

            routingPage = FindViewById<Button>(Resource.Id.btnRoutingButton);
            routingPage.Click += RoutingPage_Click;

            veriAnalizButonu.Click += VeriAnalizButonu_Click;
            seekbarBoyTut.Progress = Convert.ToInt16(modelGelen.Boy.ToString());
            seekbarBoyTut.Max = 270;
            
            seekbarBoyTut.ProgressChanged += SeekbarBoyTut_ProgressChanged;
            //
            seekbarControlBoy = FindViewById<SeekBar>(Resource.Id.seekBar6);

            seekbarControlBoy.Progress=Convert.ToInt16(modelGelen.Boy.ToString());
            seekbarControlBoy.SetProgress(Convert.ToInt32(modelGelen.Karin.ToString()), false);
            seekbarControlBoy.Max = 300;
            seekbarControlBoy.ProgressChanged += SeekbarControlBoy_ProgressChanged;

            seekbarKarin.Progress = Convert.ToInt16(modelGelen.Karin.ToString());
            //seekbarKarin.SetProgress(Convert.ToInt32(modelGelen.Karin.ToString()), false);
            seekbarKarin.Max = 100;
            seekbarKarin.ProgressChanged += SeekbarKarin_ProgressChanged;

            seekbarKilo.Progress = Convert.ToInt16(modelGelen.Kilo.ToString());
            seekbarKilo.Max = 300;
            seekbarKilo.ProgressChanged += SeekbarKilo_ProgressChanged;


            seekbarGogus.Progress = Convert.ToInt16(modelGelen.Chest.ToString());
            seekbarGogus.Max = 300;
            seekbarGogus.ProgressChanged += SeekbarGogus_ProgressChanged;

            seekbarOmuz.Progress = Convert.ToInt16(modelGelen.Omuz.ToString());
            seekbarOmuz.Max = 100;
            seekbarOmuz.ProgressChanged += SeekbarOmuz_ProgressChanged;

          

            


            txtViewBoy.Text = modelGelen.Boy.ToString();
            txtViewKarin.Text = modelGelen.Karin.ToString();
            txtViewKilo.Text = modelGelen.Kilo.ToString();
            txtViewOmuz.Text = modelGelen.Omuz.ToString();
            txtViewGogus.Text = modelGelen.Chest.ToString();
            txtViewBoyKontrol.Text = modelGelen.Boy.ToString();





        }

        private void RoutingPage_Click(object sender, EventArgs e)
        {

            Intent activityRoutingView = new Intent(this, typeof(ActivityRoutingData));
            //JsonConvert ve JsonConverter farkını iyice gözet.
            StartActivity(activityRoutingView);
        }

        private void BlogGitButton_Click(object sender, EventArgs e)
        {
            Intent activityWebView = new Intent(this, typeof(ActivityWebView));
            //JsonConvert ve JsonConverter farkını iyice gözet.
            StartActivity(activityWebView);
        }

        private void SeekbarControlBoy_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            txtViewBoyKontrol.Text = e.Progress.ToString();
        }

        private void SeekbarBoyTut_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            txtViewBoy.Text = e.Progress.ToString();
        }

        private void VeriAnalizButonu_Click(object sender, EventArgs e)
        {
            ProgressDialog progressbar = new ProgressDialog(this);

            progressbar.SetCancelable(true);

            progressbar.SetMessage("Veriler İşleniyor.");

            progressbar.SetProgressStyle(ProgressDialogStyle.Horizontal);
            progressbar.Progress = 0;
            progressbar.Max = 100;
            progressbar.Show();
            pgDurum = 0;
            new System.Threading.Thread(new ThreadStart(delegate
            {
                while (pgDurum<100)
                {
                    pgDurum += 10;
                    progressbar.Progress = pgDurum;
                    Java.Lang.Thread.Sleep(100);
                }
                RunOnUiThread(() => { progressbar.SetMessage("Sonuçlar Hazirlandi."); });
               
            })).Start();
           


        }
       
        private void SeekbarOmuz_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            txtViewOmuz.Text = e.Progress.ToString();
        }

        private void SeekbarGogus_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            txtViewGogus.Text = e.Progress.ToString();
        }

        private void SeekbarKilo_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            txtViewKilo.Text = e.Progress.ToString();
        }

        private void SeekbarKarin_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            txtViewKarin.Text = e.Progress.ToString();
        }

        
    }
}