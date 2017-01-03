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
using br.com.weblayer.logistica.android.exp.Core.Model;
using br.com.weblayer.logistica.android.exp.Core.DAL;

namespace br.com.weblayer.logistica.android.exp.Core.BLL
{
    public class EntregaManager
    {
        public string mensagem;

        public List<Entrega> GetEntrega()
        {
            return new EntregaRepository().List();
        }

        public void Save(Entrega obj)
        {
            var erros = "";

            if (obj.ds_NFE.Length < 5)
                erros = erros + "\n O código da nota é inválido! Ele deve ter, no mínimo, 6 caracteres";

            var Repository = new EntregaRepository();
            Repository.Save(obj);

            mensagem = $"Entrega {obj.id} atualizada com sucesso";
        }

        public void Delete(Entrega obj)
        {
            var Repository = new EntregaRepository();
            Repository.Delete(obj);

            mensagem = $"Entrega {obj.id} excluída com sucesso";
        }

   

    }
}