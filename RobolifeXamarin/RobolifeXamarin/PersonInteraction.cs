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
using Com.XW.Repo;
using Android.Support.V7.App;
using static Com.XW.Repo.BubbleSeekBar;
using IR.Sohreco.Circularpulsingbutton;
using Android.Views.Animations;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Com.Ebanx.Swipebtn;

namespace App6
{
    [Activity(Label = "PersonInteraction",Theme ="@style/Theme.AppCompat.Light.NoActionBar")]
    public class PersonInteraction : AppCompatActivity,IOnProgressChangedListener,IOnStateChangeListener
    {
        BubbleSeekBar seekBarLenght;
        //BubbleSeekBar seekBarWeight;
        BubbleSeekBar seekBarNeck;
        BubbleSeekBar seekBarShoulder;
        BubbleSeekBar seekBarChest;
        BubbleSeekBar seekBarBiceps;
        BubbleSeekBar seekBarWaist;
        BubbleSeekBar seekBarHips;
        BubbleSeekBar seekBarTihgs;
        BubbleSeekBar seekBarKnee;
        BubbleSeekBar seekBarCalves;
        Model3D gelen3DModel;
        SwipeButton swipeGoData;

        private CircularPulsingButton btnGoRouteDataResult;
        public void GetProgressOnActionUp(int p0, float p1)
        {
            
        }

        public void GetProgressOnFinally(int p0, float p1)
        {
            throw new NotImplementedException();
        }

        public void OnProgressChanged(int p0, float p1)
        {
            
        }

        public void OnStateChange(bool p0)
        {
            StartActivity(typeof(ActivityRoutingData));//Burada veri yolla

            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

            Intent activityRoutingnInteraction = new Intent(this, typeof(ActivityRoutingData));
            gelen3DModel = JsonConvert.DeserializeObject<Model3D>(Intent.GetStringExtra("3D_Model"));
            activityRoutingnInteraction.PutExtra("3D_Model", JsonConvert.SerializeObject(gelen3DModel));

            StartActivity(activityRoutingnInteraction);

            Toast.MakeText(this, "İşlem Tamamlandı..", ToastLength.Short).Show();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PersonValueInteraction);
            gelen3DModel = JsonConvert.DeserializeObject<Model3D>(Intent.GetStringExtra("3D_Model"));

           btnGoRouteDataResult = FindViewById<CircularPulsingButton>(Resource.Id.btnGoRoutingData);
           btnGoRouteDataResult.Click += BtnGoRouteDataResult_Click;



            swipeGoData = FindViewById<SwipeButton>(Resource.Id.swipeGoRoutingData);
            swipeGoData.SetOnStateChangeListener(this);


            seekBarLenght = FindViewById<BubbleSeekBar>(Resource.Id.seekBarLengthBubble);
           // seekBarWeight = FindViewById<BubbleSeekBar>(Resource.Id.seekBarWeightBubble);
            seekBarNeck = FindViewById<BubbleSeekBar>(Resource.Id.seekBarNeckBubble);
            seekBarShoulder = FindViewById<BubbleSeekBar>(Resource.Id.seekBarShoulderBubble);
            seekBarChest = FindViewById<BubbleSeekBar>(Resource.Id.seekBarChestBubble);
            seekBarBiceps = FindViewById<BubbleSeekBar>(Resource.Id.seekBarBicepsBubble);
            seekBarWaist = FindViewById<BubbleSeekBar>(Resource.Id.seekBarWaistBubble);
            seekBarHips = FindViewById<BubbleSeekBar>(Resource.Id.seekBarHipsBubble);
            seekBarTihgs = FindViewById<BubbleSeekBar>(Resource.Id.seekBarTighsBubble);
            seekBarKnee = FindViewById<BubbleSeekBar>(Resource.Id.seekBarKneeBubble);
            seekBarCalves = FindViewById<BubbleSeekBar>(Resource.Id.seekBarCalvesBubble);


            seekBarLenght.SetProgress(gelen3DModel.Boy);
            //seekBarWeight.SetProgress(gelen3DModel.Kilo);
            seekBarNeck.SetProgress( gelen3DModel.Neck);
            seekBarChest.SetProgress(gelen3DModel.Chest);
            seekBarShoulder.SetProgress(gelen3DModel.Omuz);
            seekBarWaist.SetProgress(gelen3DModel.Karin);
            seekBarHips.SetProgress( gelen3DModel.Hips);
            seekBarTihgs.SetProgress(gelen3DModel.Tighs);
            seekBarKnee.SetProgress( gelen3DModel.Knee);
            seekBarCalves.SetProgress(gelen3DModel.Calves);

            // bir bak seekBarBiceps.SetLayerType(LayerType.Hardware,Android.Graphics.Paint co)
            seekBarsAnimation();






        }
        public async Task seekBarsAnimation() {


            Animation anm_seekBarLenght = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.fade_in_animation);
            seekBarLenght.StartAnimation(anm_seekBarLenght);


            //Animation anm_seekBarWeight = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.fade_in_animation_delay_level2);
            //seekBarWeight.StartAnimation(anm_seekBarWeight);

            Animation anm_seekBarNeck = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.fade_in_animation_delay_level3);
            seekBarNeck.StartAnimation(anm_seekBarNeck);

            Animation anm_seekBarShoulder = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.fade_in_animation_delay_level4);
            seekBarShoulder.StartAnimation(anm_seekBarShoulder);

            Animation anm_seekBarChest = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.fade_in_animation_delay_level5);
            seekBarChest.StartAnimation(anm_seekBarChest);

            Animation anm_seekBarBiceps = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.fade_in_animation_delay_level6);
            seekBarBiceps.StartAnimation(anm_seekBarBiceps);

            Animation anm_seekBarWaist = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.fade_in_animation_delay_level7);
            seekBarWaist.StartAnimation(anm_seekBarWaist);

            Animation anm_seekBarHips = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.fade_in_animation_delay_level8);
            seekBarHips.StartAnimation(anm_seekBarHips);

            Animation anm_seekBarTihgs = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.fade_in_animation_delay_level9);
            seekBarTihgs.StartAnimation(anm_seekBarTihgs);

            Animation anm_seekBarKnee = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.fade_in_animation_delay_level10);
            seekBarKnee.StartAnimation(anm_seekBarKnee);

            Animation anm_seekBarCalves = Android.Views.Animations.AnimationUtils.LoadAnimation(this, Resource.Animation.fade_in_animation_delay_level11);
            seekBarCalves.StartAnimation(anm_seekBarCalves);
        }

        private void BtnGoRouteDataResult_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ActivityRoutingData));



        }

      
    }
}