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
    public class Activity_Sobre : Activity_Base
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        private List<string> mItems;
        private ListView mListView;

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

            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            FindViews();
            BindData();                
        }

        private void FindViews()
        {
            mListView = FindViewById<ListView>(Resource.Id.listaAjuda);
        }

        private void BindData()
        {
            mItems = new List<string>();

            mItems.Add("Novidades");
            mItems.Add("Versão\n" + GetVersion());

            mListView.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, mItems);
            mListView.ItemClick += mListView_ItemClick;
        }

        private void mListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (e.Id == 0)
            {
                StartActivity(typeof(Activity_Novidades));
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_toolbar, menu);
            menu.RemoveItem(Resource.Id.action_deletar);
            menu.RemoveItem(Resource.Id.action_adicionar);
            menu.RemoveItem(Resource.Id.action_ajuda);
            menu.RemoveItem(Resource.Id.action_sobre);
            menu.RemoveItem(Resource.Id.action_sair);

            return base.OnCreateOptionsMenu(menu);
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

        private string GetVersion()
        {
            return Application.Context.PackageManager.GetPackageInfo(Application.Context.ApplicationContext.PackageName, 0).VersionName;
        }
     }
}