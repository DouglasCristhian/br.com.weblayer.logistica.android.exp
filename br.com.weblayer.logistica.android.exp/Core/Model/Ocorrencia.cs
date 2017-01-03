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
using SQLite;

namespace br.com.weblayer.logistica.android.exp.Core.Model
{
    public class Ocorrencia
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(200), NotNull]
        public string ds_descricao { get; set; }
    }
}