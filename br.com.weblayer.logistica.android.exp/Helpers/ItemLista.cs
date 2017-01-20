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
    public class ItemLista
    {
        private string titulo;
        private string subtitulo;

        public ItemLista(string titulo, string subtitulo)
        {
            this.titulo = titulo;
            this.subtitulo = subtitulo;
        }

        public string Titulo
        {
            get
            {
                return titulo;
            }
        }

        public string Subtitulo
        {
            get
            {
                return subtitulo;
            }
        }
    }
}