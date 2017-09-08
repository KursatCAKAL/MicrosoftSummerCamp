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

namespace App6
{
    [Activity(Label = "ActivityBeslenme")]
    public class ActivityBeslenme : Activity
    {
        private Button btnTut;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.beslenme_item_layout);
            List<Beslenme> lstSource = new List<Beslenme>();


            var lstData = FindViewById<ListView>(Resource.Id.lstView_Beslenme);
            btnTut = FindViewById<Button>(Resource.Id.button1);

            btnTut.Click += delegate
            {

                for (int i = 0; i < 20; i++)
                {
                    Beslenme beslenmeProgramı = new Beslenme()
                    {

                        Id = i,
                    };
                    lstSource.Add(beslenmeProgramı);
                }



            };
            var adapter = new CustomAdapter(this, lstSource);
            lstData.Adapter = adapter;
                

        }

       
    }
}