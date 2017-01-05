using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using br.com.weblayer.logistica.android.exp.Core.Model;

namespace br.com.weblayer.logistica.android.exp.Adapters
{
    [Activity(Label = "Adapter_Entrega_ListView")]
    public class Adapter_Entrega_ListView : BaseAdapter<Entrega>
    {
        public List<Entrega> mItems;
        public Context mContext;
        private string descricaoocorrencia;

        public Adapter_Entrega_ListView(Context context, List<Entrega> items)
        {
            mItems = items;
            mContext = context;
        }

        public override Entrega this[int position]
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.Adapter_Entrega_ListView, null, false);

            descricaoocorrencia = "";
            if (mItems[position].id_ocorrencia == 1)
                descricaoocorrencia = "ENTREGA";
            if (mItems[position].id_ocorrencia == 2)
                descricaoocorrencia = "INFORMATIVO";
            if (mItems[position].id_ocorrencia == 3)
                descricaoocorrencia = "REENTREGA";
            if (mItems[position].id_ocorrencia == 4)
                descricaoocorrencia = "DEVOLUÇÃO";

            row.FindViewById<TextView>(Resource.Id.ds_NFE).Text = "NFe: " + mItems[position].ds_NFE;

            row.FindViewById<TextView>(Resource.Id.id_ocorrencia).Text = "Ocorrencia: " + descricaoocorrencia;

            row.FindViewById<TextView>(Resource.Id.dt_inclusao).Text = "Data de Inclusão: " + mItems[position].dt_inclusao.ToString();
            row.FindViewById<TextView>(Resource.Id.dt_entrega).Text = "Data de Entrega: " + mItems[position].dt_entrega.Value.ToString("dd/MM/yyyy");

            return row;
        }
    }
}