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

namespace br.com.weblayer.logistica.android.exp.Helpers
{
    class MyHolder
    {
        public TextView Titulo;
        public TextView Subtitulo;

        public MyHolder(View itemView)
        {
            Titulo = itemView.FindViewById<TextView>(Resource.Id.tituloTxt);
            Subtitulo = itemView.FindViewById<TextView>(Resource.Id.subtituloTxt);
        }
    }
}