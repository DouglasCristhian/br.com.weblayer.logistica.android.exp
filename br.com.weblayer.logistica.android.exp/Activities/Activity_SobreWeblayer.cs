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
using System.Collections;
using br.com.weblayer.logistica.android.exp.Adapters;
using br.com.weblayer.logistica.android.exp.Helpers;

namespace br.com.weblayer.logistica.android.exp.Activities
{
    [Activity(Label = "Sobre")]
    public class Activity_SobreWeblayer : Activity_Base
    {
        private CustomAdapter adapter;
        private ListView lv;
        private List<ItemLista> itens;

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

            FindViews();
            adapter = new CustomAdapter(this, Resource.Layout.ItemLista_Model, GetItens());

            lv.Adapter = adapter;
            lv.ItemClick += Lv_ItemClick;
        }

        private void Lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (e.Position == 1)
            {
                Toast.MakeText(this, "Ir para tela de novidades", ToastLength.Short).Show();
            }
        }

        private void FindViews()
        {
            lv = FindViewById<ListView>(Resource.Id.listaAjuda);
        }

        private List<ItemLista> GetItens()
        {
            string versao = GetVersion();

            itens = new List<ItemLista>()
            {
                new ItemLista("Novidades", ""),
                new ItemLista("Versão\n", versao.ToString()),
            };

          return itens;
        }

        private string GetVersion()
        {
            return Application.Context.ApplicationContext.PackageManager.GetPackageInfo(Application.Context.ApplicationContext.PackageName, 0).VersionName;
        }
    }
}