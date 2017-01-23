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
    public class Substring_Helper
    {
        public string Substring_CNPJ(string NFE)
        {
            string cnpj;

            if (NFE.Length < 20)
            {
                cnpj = null;
            }
            else
            {
                cnpj = NFE.Substring(6, 14);
            }

            return cnpj;
        }

        public string Substring_NumeroNF(string NFE)
        {
            string num_NF;

            if (NFE.Length < 34)
            {
                num_NF = null;
            }
            else
            {
                num_NF = NFE.Substring(25, 9);
            }

            return num_NF;
        }

        public string Substring_SerieNota(string NFE)
        {
            string serie_nota;

            if (NFE.Length < 25)
            {
                serie_nota = null;
            }
            else
            {
                serie_nota = NFE.Substring(22, 3);
            }

            return serie_nota;
        }
    }
}