using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Gms.Vision;
using Android.Gms.Vision.Texts;
using Android.Util;
using Android.Graphics;
using Android.Runtime;
using System;
using Android.Support.V4.App;
using Android;
using Android.Content.PM;
using static Android.Gms.Vision.Detector;
using Java.Lang;
using System.Threading;
using Android.Content;
using Newtonsoft.Json;
using Android.Webkit;
using System.Threading.Tasks;
using Java.Text;
using Android.Views.Animations;
using Com.Ebanx.Swipebtn;

namespace App6
{
    [Activity(Label = "App6",  Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : AppCompatActivity, ISurfaceHolderCallback, IProcessor
    {
       
        //private WebView webViewKullan;
        //private WebSettings webSettingsKullan;

        private SurfaceView cameraView;
        public static int baslangic;
        private CameraSource cameraSource;
        private const int RequestCameraPermissionID = 1001;
        private TextView textView;//text recognizer analizer

        private TextView txtBoyAd;
        private TextView txtKiloAd;
        private TextView txtKarinAd;
        private TextView txtOmuzAd;
        private TextView txtChestAd;

        private TextView txtBoyDeger;
        private TextView txtKiloDeger;
        private TextView txtKarinDeger;
        private TextView txtOmuzDeger;
        private TextView txtChestDeger;
        private TextView txtHipsDeger;
        private TextView txtTighsDeger;
        private TextView txtKneeDeger;
        private TextView txtCalvesDeger;
        private TextView txtNeckDeger;

        private SwipeButton swipeBtnGoGoGo;
        //private CircularPulsingButton btnCircularCheckButon;
        //private CircularPulsingButton btnCircularPulsingButton;



        private JavaList lstTutData;
        private LinearLayout rlt;
        private Bitmap bmp;
        private LinearLayout layout_Deneme;
        private ImageView imageCameraProp;
        private ImageView imageDataProp;

        public object ImageAnalysis { get; private set; }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestCameraPermissionID:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            cameraSource.Start(cameraView.Holder);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
         

            rlt = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            swipeBtnGoGoGo = FindViewById<SwipeButton>(Resource.Id.swipeBtn_Data);
            swipeBtnGoGoGo.Click += SwipeBtnGoGoGo_Click;
            swipeBtnGoGoGo.StateChange += SwipeBtnGoGoGo_StateChange;

            cameraView = FindViewById<SurfaceView>(Resource.Id.surface_view);




            imageCameraProp = FindViewById<ImageView>(Resource.Id.imageCamera);
            imageCameraProp.SetImageResource(Resource.Drawable.Kamera_Button);
            imageCameraProp.Click += ImageCameraProp_Click;

            imageDataProp = FindViewById<ImageView>(Resource.Id.imageData);
            imageDataProp.SetImageResource(Resource.Drawable.data_final);
            imageDataProp.Click += ImageDataProp_Click;


            txtBoyAd = FindViewById<TextView>(Resource.Id.txtBoyAd);
          
            txtOmuzAd = FindViewById<TextView>(Resource.Id.txtOmuzAd);
            txtChestAd = FindViewById<TextView>(Resource.Id.txtChestAd);
            layout_Deneme = FindViewById<LinearLayout>(Resource.Id.linearLayout1);

            txtBoyDeger = FindViewById<TextView>(Resource.Id.txtBoyDeger);
            txtKarinDeger = FindViewById<TextView>(Resource.Id.txtKarinDeger);
            txtOmuzDeger = FindViewById<TextView>(Resource.Id.txtOmuzDeger);
            txtChestDeger = FindViewById<TextView>(Resource.Id.txtChestDeger);
            txtHipsDeger = FindViewById<TextView>(Resource.Id.txtHipsValue);
            txtTighsDeger = FindViewById<TextView>(Resource.Id.txtThigsValue);
            txtCalvesDeger = FindViewById<TextView>(Resource.Id.txtCalvesValue);
            txtKneeDeger = FindViewById<TextView>(Resource.Id.txtKneeValue);
            txtNeckDeger = FindViewById<TextView>(Resource.Id.txtNeckValue);

            txtCalvesDeger = FindViewById<TextView>(Resource.Id.txtCalvesValue);
            TextRecognizer textRecognizer = new TextRecognizer.Builder(ApplicationContext).Build();
            if (!textRecognizer.IsOperational)
                Log.Error("Main Activity", "Detector dependencies are not yet available");
            else
            {
                cameraSource = new CameraSource.Builder(ApplicationContext, textRecognizer)
                    .SetFacing(CameraFacing.Back)
                    .SetRequestedPreviewSize(1280, 1024)
                    .SetRequestedFps(2.0f)
                    .SetAutoFocusEnabled(true)
                    .Build();

                cameraView.Holder.AddCallback(this);
                textRecognizer.SetProcessor(this);
            }
       
           //btnCircularCheckButon = FindViewById<CircularPulsingButton>(Resource.Id.btnCircularCheck);

           //btnCircularCheckButon.Click +=//btnCircularCheckButon_Click;
        }

        private void SwipeBtnGoGoGo_StateChange(object sender, StateChangeEventArgs e)
        {
            Animation cameraProp2 = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.image_scale_animation);
            imageDataProp.StartAnimation(cameraProp2);

            System.Threading.Thread.Sleep(100);

            Model3D modelSend = new Model3D();

            modelSend.Boy = float.Parse(txtBoyDeger.Text.ToString());
            modelSend.Neck = float.Parse(txtNeckDeger.Text.ToString());
            modelSend.Omuz = float.Parse(txtOmuzDeger.Text.ToString());
            modelSend.Chest = float.Parse(txtChestDeger.Text.ToString());
            modelSend.Karin = float.Parse(txtKarinDeger.Text.ToString());
            modelSend.Hips = float.Parse(txtHipsDeger.Text.ToString());
            modelSend.Tighs = float.Parse(txtTighsDeger.Text.ToString());
            modelSend.Knee = float.Parse(txtKneeDeger.Text.ToString());
            modelSend.Calves = float.Parse(txtCalvesDeger.Text.ToString());


            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

            Intent activityPersonInteraction = new Intent(this, typeof(PersonInteraction));

            //JsonConvert ve JsonConverter farkını iyice gözet.
            activityPersonInteraction.PutExtra("3D_Model", JsonConvert.SerializeObject(modelSend));

            StartActivity(activityPersonInteraction);
        }

        private void SwipeBtnGoGoGo_Click(object sender, EventArgs e)
        {
            Animation cameraProp2 = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.image_scale_animation);
            imageDataProp.StartAnimation(cameraProp2);

            System.Threading.Thread.Sleep(100);

            Model3D modelSend = new Model3D();

            modelSend.Boy = float.Parse(txtBoyDeger.Text.ToString());
            //modelSend.Kilo = float.Parse(txtKiloDeger.Text.ToString());
            modelSend.Neck = float.Parse(txtNeckDeger.Text.ToString());
            modelSend.Omuz = float.Parse(txtOmuzDeger.Text.ToString());
            modelSend.Chest = float.Parse(txtChestDeger.Text.ToString());
            modelSend.Karin = float.Parse(txtKarinDeger.Text.ToString());
            modelSend.Hips = float.Parse(txtHipsDeger.Text.ToString());
            modelSend.Tighs = float.Parse(txtTighsDeger.Text.ToString());
            modelSend.Knee = float.Parse(txtKneeDeger.Text.ToString());
            modelSend.Calves = float.Parse(txtCalvesDeger.Text.ToString());


            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

            Intent activityPersonInteraction = new Intent(this, typeof(PersonInteraction));

            //JsonConvert ve JsonConverter farkını iyice gözet.
            activityPersonInteraction.PutExtra("3D_Model", JsonConvert.SerializeObject(modelSend));

            StartActivity(activityPersonInteraction);
        }

        private void ImageDataProp_Click(object sender, EventArgs e)
        {
            Animation cameraProp2 = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.image_scale_animation);
            imageDataProp.StartAnimation(cameraProp2);

            System.Threading.Thread.Sleep(100);

            Model3D modelSend = new Model3D();

            modelSend.Boy = float.Parse(txtBoyDeger.Text.ToString());
            //modelSend.Kilo = float.Parse(txtKiloDeger.Text.ToString());
            modelSend.Neck = float.Parse(txtNeckDeger.Text.ToString());
            modelSend.Omuz = float.Parse(txtOmuzDeger.Text.ToString());
            modelSend.Chest = float.Parse(txtChestDeger.Text.ToString());
            modelSend.Karin = float.Parse(txtKarinDeger.Text.ToString());
            modelSend.Hips = float.Parse(txtHipsDeger.Text.ToString());
            modelSend.Tighs = float.Parse(txtTighsDeger.Text.ToString());
            modelSend.Knee = float.Parse(txtKneeDeger.Text.ToString());
            modelSend.Calves = float.Parse(txtCalvesDeger.Text.ToString());


            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

            Intent activityPersonInteraction = new Intent(this, typeof(PersonInteraction));

            //JsonConvert ve JsonConverter farkını iyice gözet.
            activityPersonInteraction.PutExtra("3D_Model", JsonConvert.SerializeObject(modelSend));

            StartActivity(activityPersonInteraction);
        }
        private void ImageCameraProp_Click(object sender, EventArgs e)
        {
            Animation cameraProp1 = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.image_scale_animation);
            imageCameraProp.StartAnimation(cameraProp1);
            ThreadPool.QueueUserWorkItem(o => SlowLat());

        }
        public async Task startPersonActivity() {




        }
        

        private void SlowLat()
        {
            DecimalFormat df = new DecimalFormat("#.##");


         
            int  y, z, k, l;

            double a;
            a = Convert.ToDouble(ReceiveDetections(this));

            int counter = 1;
        
            switch (counter)
            {
                case 1:
                    
                    for (double i = 0; i < baslangic; i += a)
                    {
                        System.Threading.Thread.Sleep(50);
                        RunOnUiThread(() => txtBoyDeger.Text = df.Format(i / 0.9).ToString());
                        //RunOnUiThread(() => txtKiloDeger.Text = df.Format(i / 3.1).ToString());
                        RunOnUiThread(() => txtKarinDeger.Text = df.Format(i / 5.1).ToString());
                        RunOnUiThread(() => txtOmuzDeger.Text = df.Format(i / 7.9).ToString());
                        RunOnUiThread(() => txtChestDeger.Text = df.Format(i / 4.7).ToString());
                        RunOnUiThread(() => txtKneeDeger.Text = df.Format(i / 14.2).ToString());
                        RunOnUiThread(() => txtTighsDeger.Text = df.Format(i / 3.6).ToString());
                        RunOnUiThread(() => txtHipsDeger.Text = df.Format(i / 5.7).ToString());
                        RunOnUiThread(() => txtNeckDeger.Text = df.Format(i / 13.3).ToString());
                        RunOnUiThread(() => txtCalvesDeger.Text = df.Format(i / 10.3).ToString());
                    }
                    counter++;
                    break;
                case 2:
                    for (double i = 0; i < baslangic; i += a)
                    {
                        System.Threading.Thread.Sleep(50);
                        RunOnUiThread(() => txtBoyDeger.Text = df.Format(i / 0.9).ToString());
                        //RunOnUiThread(() => txtKiloDeger.Text = df.Format(i / 3.1).ToString());
                        RunOnUiThread(() => txtKarinDeger.Text = df.Format(i / 10.1).ToString());
                        RunOnUiThread(() => txtOmuzDeger.Text = df.Format(i / 17.9).ToString());
                        RunOnUiThread(() => txtChestDeger.Text = df.Format(i / 14.7).ToString());
                        RunOnUiThread(() => txtKneeDeger.Text = df.Format(i / 14.5).ToString());
                        RunOnUiThread(() => txtTighsDeger.Text = df.Format(i / 3.1).ToString());
                        RunOnUiThread(() => txtHipsDeger.Text = df.Format(i / 5.7).ToString());
                        RunOnUiThread(() => txtNeckDeger.Text = df.Format(i / 11.7).ToString());
                        RunOnUiThread(() => txtCalvesDeger.Text = df.Format(i / 12.3).ToString());
                    }
                    counter++;
                    break;
                case 3:
                    for (double i = 0; i < baslangic; i += a)
                    {
                        System.Threading.Thread.Sleep(50);
                        RunOnUiThread(() => txtBoyDeger.Text = df.Format(i / 0.9).ToString());
                        //RunOnUiThread(() => txtKiloDeger.Text = df.Format(i / 3.1).ToString());
                        RunOnUiThread(() => txtKarinDeger.Text = df.Format(i / 5.1).ToString());
                        RunOnUiThread(() => txtOmuzDeger.Text = df.Format(i / 7.9).ToString());
                        RunOnUiThread(() => txtChestDeger.Text = df.Format(i / 4.7).ToString());
                        RunOnUiThread(() => txtKneeDeger.Text = df.Format(i / 14.2).ToString());
                        RunOnUiThread(() => txtTighsDeger.Text = df.Format(i / 3.6).ToString());
                        RunOnUiThread(() => txtHipsDeger.Text = df.Format(i / 5.7).ToString());
                        RunOnUiThread(() => txtNeckDeger.Text = df.Format(i / 13.3).ToString());
                        RunOnUiThread(() => txtCalvesDeger.Text = df.Format(i / 10.3).ToString());
                    }
                    counter++;
                    break;
                case 4:
                    for (double i = 0; i < baslangic; i += a)
                    {
                        System.Threading.Thread.Sleep(50);
                        RunOnUiThread(() => txtBoyDeger.Text = df.Format(i / 0.9).ToString());
                        //RunOnUiThread(() => txtKiloDeger.Text = df.Format(i / 3.1).ToString());
                        RunOnUiThread(() => txtKarinDeger.Text = df.Format(i / 10.1).ToString());
                        RunOnUiThread(() => txtOmuzDeger.Text = df.Format(i / 17.9).ToString());
                        RunOnUiThread(() => txtChestDeger.Text = df.Format(i / 14.7).ToString());
                        RunOnUiThread(() => txtKneeDeger.Text = df.Format(i / 14.5).ToString());
                        RunOnUiThread(() => txtTighsDeger.Text = df.Format(i / 3.1).ToString());
                        RunOnUiThread(() => txtHipsDeger.Text = df.Format(i / 5.7).ToString());
                        RunOnUiThread(() => txtNeckDeger.Text = df.Format(i / 11.7).ToString());
                        RunOnUiThread(() => txtCalvesDeger.Text = df.Format(i / 12.3).ToString());
                    }
                    counter++;
                    break;
                default:
                    Toast.MakeText(this, "Dikkat değerlendirilen 4 açı tamamlandı.", ToastLength.Short).Show();
                    break;
            }
          

        }

      

      

        #region TextDetector_Not_BodyAnalizer
        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {

        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            if (ActivityCompat.CheckSelfPermission(ApplicationContext, Manifest.Permission.Camera) != Android.Content.PM.Permission.Granted)
            {
                //Request Permission
                ActivityCompat.RequestPermissions(this, new string[]
                    {
                        Android.Manifest.Permission.Camera
                    }, RequestCameraPermissionID);
                return;
            }
            cameraSource.Start(cameraView.Holder);
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            cameraSource.Stop();
        }

        public void ReceiveDetections(Detections detections)
        {
            #region CommentTextRecognation
            SparseArray items = detections.DetectedItems;
            if (items.Size() != 0)
            {
                textView.Post(() =>
                {
                    StringBuilder strBuilder = new StringBuilder();
                    for (int i = 0; i < items.Size(); ++i)
                    {
                        strBuilder.Append(((TextBlock)items.ValueAt(i)).Value);
                        strBuilder.Append("\n");
                    }
                    textView.Text = strBuilder.ToString();
                });
            }
            #endregion
        }

        public void Release()
        {
            throw new NotImplementedException();
        } 
        #endregion
    }

 
}

