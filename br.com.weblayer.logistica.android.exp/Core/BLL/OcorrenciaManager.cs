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
    class OcorrenciaManager
    {
        public List<Ocorrencia> GetOcorrencia()
        {
            return new OcorrenciaRepository().List();
        }


    }
}