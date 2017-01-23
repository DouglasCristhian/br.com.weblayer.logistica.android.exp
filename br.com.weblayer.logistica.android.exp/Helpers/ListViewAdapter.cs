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
    class ListViewAdapter : BaseAdapter<ItemLista>
    {
        public List<ItemLista> mItems;
        private Context mContext;

        public ListViewAdapter(Context context, List<ItemLista> items)
        {
            mItems = items;
            mContext = context;
        }

        public override ItemLista this[int position]
        {
            get
            {
                return mItems[position];
            }
        }

        public override int Count
        {
            get
            {
                return mItems.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.ItemLista_Model, null, false);
            }

            TextView txtTitulo = row.FindViewById<TextView>(Resource.Id.txtTitulo);
            txtTitulo.Text = mItems[position].Titulo;

            //TextView txtSubtitulo = row.FindViewById<TextView>(Resource.Id.txtSubtitulo);
            //txtSubtitulo.Text = mItems[position].SubTitulo;

            //if (mItems[position].SubTitulo == null)
            //{
            //    mItems[position].SubTitulo;
            //}

            //else
            //    txtSubtitulo.Text = mItems[position].SubTitulo;

            //if (txtSubtitulo.Text == "")
            //{
            //    txtSubtitulo.Visibility = ViewStates.Gone;
            //}

            return row;
        }
    }
}