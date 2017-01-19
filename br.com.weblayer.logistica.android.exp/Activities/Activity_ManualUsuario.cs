using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using br.com.weblayer.logistica.android.exp.Fragments;
using Android.Views;
using System;
using static Android.App.ActionBar;

namespace br.com.weblayer.logistica.android.exp.Activities
{
    [Activity(Label = "Manual do Usuário", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Activity_ManualUsuario : Activity_Base
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        ViewPager pager;
        GenericFragmentPagerAdaptor adapter;
        private int TAG;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_ManualUsuario;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            TAG = 1;

            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Ajuda";

            /*var*/ pager = FindViewById<ViewPager>(Resource.Id.pager);
            /*var */adapter = new GenericFragmentPagerAdaptor(SupportFragmentManager);

            //MenuManual
            adapter.AddFragmentView((i, v, b) =>
            {
                var view = i.Inflate(Resource.Layout.Fragment_MenuManual, v, false);
                return view;
            });

            //Introdução
            adapter.AddFragmentView((i, v, b) =>
            {
                var view = i.Inflate(Resource.Layout.Fragment_Introducao, v, false);
                return view;
            });

            //UtilizandoApp
            adapter.AddFragmentView((i, v, b) =>
            {
                var view = i.Inflate(Resource.Layout.Fragment_UtilizandoApp, v, false);
                return view;
            });

            //Permissoes
            adapter.AddFragmentView((i, v, b) =>
            {
                var view = i.Inflate(Resource.Layout.Fragment_PermissoesApp, v, false);
                return view;
            });

            //EnviarEmail
            adapter.AddFragmentView((i, v, b) =>
            {
                var view = i.Inflate(Resource.Layout.Fragment_EnviarEmailApp, v, false);
                return view;
            });

            //RegistrosVisualizar
            adapter.AddFragmentView((i, v, b) =>
            {
                var view = i.Inflate(Resource.Layout.Fragment_Registros, v, false);
                return view;
            });

            //RegistrosDeletar
            adapter.AddFragmentView((i, v, b) =>
            {
                var view = i.Inflate(Resource.Layout.Fragment_RegistrosDeletar, v, false);
                return view;
            });

            pager.Adapter = adapter;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    if (TAG == 1)
                    {
                        pager.CurrentItem = 0;
                        TAG = 0;
                    }
                    else
                    {
                        Finish();
                    }
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_toolbar, menu);
            menu.RemoveItem(Resource.Id.action_deletar);
            menu.RemoveItem(Resource.Id.action_adicionar);
            menu.RemoveItem(Resource.Id.action_ajuda);
            menu.RemoveItem(Resource.Id.action_sobre);

            return base.OnCreateOptionsMenu(menu);
        }

        //[Java.Interop.Export("SobreWeblayerClick")] // The value found in android:onClick attribute.
        //public void SobreWeblayerClick(View v) // Does not need to match value in above attribute.
        //{
        //    StartActivity(typeof(Activity_SobreWeblayer));
        //}

        [Java.Interop.Export("IntroduçãoClick")] // The value found in android:onClick attribute.
        public void IntroduçãoClick(View v) // Does not need to match value in above attribute.
        {
            TAG = 1;
            pager.CurrentItem = 1;
        }

        [Java.Interop.Export("UtilizandoAppClick")] // The value found in android:onClick attribute.
        public void UtilizandoAppClick(View v) // Does not need to match value in above attribute.
        {
            TAG = 1;
            pager.CurrentItem = 2;
        }

        [Java.Interop.Export("PermissoesClick")] // The value found in android:onClick attribute.
        public void PermissoesClick(View v) // Does not need to match value in above attribute.
        {
            TAG = 1;
            pager.CurrentItem = 3;
        }

        [Java.Interop.Export("EnviarEmailClick")] // The value found in android:onClick attribute.
        public void EnviarEmailClick(View v) // Does not need to match value in above attribute.
        {
            TAG = 1;
            pager.CurrentItem = 4;
        }

        [Java.Interop.Export("RegistrosClick")] // The value found in android:onClick attribute.
        public void RegistrosClick(View v) // Does not need to match value in above attribute.
        {
            TAG = 1;
            pager.CurrentItem = 5;
        }

        [Java.Interop.Export("DeletarRegistrosClick")] // The value found in android:onClick attribute.
        public void DeletarRegistrosClick(View v) // Does not need to match value in above attribute.
        {
            TAG = 1;
            pager.CurrentItem = 6;
        }
    }
}