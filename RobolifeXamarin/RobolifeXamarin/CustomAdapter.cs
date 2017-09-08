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
using Java.Lang;

namespace App6
{
    public class ViewHolder : Java.Lang.Object
    {

        public TextView txtGun { get; set; }
        public TextView txtOgun1 { get; set; }
        public TextView txtOgun2 { get; set; }
        public TextView txtOgun3 { get; set; }
        public TextView txtOgun4 { get; set; }

    }
    public class CustomAdapter : BaseAdapter
    {
        private Activity activity;

        private List<Beslenme> diyetler;

        public CustomAdapter(Activity activity, List<Beslenme> diyet)
        {
            this.activity = activity;
            this.diyetler = diyet;
        }
        public override int Count
        {
            get { return diyetler.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return diyetler[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.beslenme_item_data_layout,parent,false);

            var txtGun = view.FindViewById<TextView>(Resource.Id.textView1);
            var txtOgun1 = view.FindViewById<TextView>(Resource.Id.textView2);
            var txtOgun2 = view.FindViewById<TextView>(Resource.Id.textView3);
            var txtOgun3 = view.FindViewById<TextView>(Resource.Id.textView4);
            var txtOgun4 = view.FindViewById<TextView>(Resource.Id.textView5);

            txtGun.Text = diyetler[position].Gun;
            txtOgun1.Text = diyetler[position].ogun1;
            txtOgun2.Text = diyetler[position].ogun2;
            txtOgun3.Text = diyetler[position].ogun3;
            txtOgun4.Text = diyetler[position].ogun4;

            return view;

        }
    }
}