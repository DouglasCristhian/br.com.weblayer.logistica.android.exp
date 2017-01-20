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
using br.com.weblayer.logistica.android.exp.Helpers;
using Android.Graphics;

namespace br.com.weblayer.logistica.android.exp.Adapters
{
    class CustomAdapter : ArrayAdapter
    {
        private Context c;
        private List<ItemLista> itens;
        private int resource;
        private LayoutInflater inflater;

        public CustomAdapter(Context context, int resource, List<ItemLista> objects) : base(context, resource, objects)
        {
            this.c = context;
            this.resource = resource;
            this.itens = objects;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (inflater == null)
            {
                inflater = (LayoutInflater)c.GetSystemService(Context.LayoutInflaterService);
            }

            if (convertView == null)
            {
                convertView = inflater.Inflate(resource, parent, false);
            }

            MyHolder holder = new MyHolder(convertView);
            holder.Titulo.Text = itens[position].Titulo;
            holder.Subtitulo.Text = itens[position].Subtitulo;

            return convertView;
        }
    }
}