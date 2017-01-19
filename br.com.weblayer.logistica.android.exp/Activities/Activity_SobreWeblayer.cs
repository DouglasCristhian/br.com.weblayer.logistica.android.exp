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

namespace br.com.weblayer.logistica.android.exp.Activities
{
    [Activity(Label = "Sobre")]
    public class Activity_SobreWeblayer : Activity_Base
    {
        TextView txtVersaoApp;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_SobreWeblayer;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ImageView img;
            img = FindViewById<ImageView>(Resource.Id.Weblayer_Logo);
            txtVersaoApp = FindViewById<TextView>(Resource.Id.txtVersaoApp);

            img.SetBackgroundResource(Resource.Drawable.Weblayer_Logo);

            string versao = GetVersionCode();
            txtVersaoApp.Text = "Versão do Aplicativo: " + versao;

        }

        public string GetVersionCode()
        {
            return Application.Context.ApplicationContext.PackageManager.GetPackageInfo(Application.Context.ApplicationContext.PackageName, 0).VersionName;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();

                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}