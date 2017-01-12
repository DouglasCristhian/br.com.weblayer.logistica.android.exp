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

namespace br.com.weblayer.logistica.android.exp.Core.DAL
{
    public class EntregaRepository 
    {
        public string Mensage { get; set; }

        public Entrega Get(int id)
        {
            return Database.GetConnection().Table<Entrega>().Where(x => x.id == id).FirstOrDefault();
        }

        public Entrega GetImage(int id, byte[] imagem)
        {
            return Database.GetConnection().Table<Entrega>().Where(x => x.id == id && x.Image == imagem).FirstOrDefault();
        }


        public void Save(Entrega entidade)
        {
            try
            {
                if (entidade.id > 0)
                    Database.GetConnection().Update(entidade);
                else
                    Database.GetConnection().Insert(entidade);
            }
            catch (Exception e)
            {
                Mensage = $"Falha ao inserir a entidade {entidade.GetType()}. Erro: {e.Message}";
            }
        }

        public void Delete(Entrega entidade)
        {
            Database.GetConnection().Delete(entidade);
        }

        public List<Entrega> List()
        {
            return Database.GetConnection().Table<Entrega>().ToList();
        }

        public void MakeDataMock()
        {
            if (List().Count > 0)
                return;

            //Save(new Entrega { ds_NFE = "35160903703339000142550000000594611002684151", id_ocorrencia = 1, dt_inclusao = DateTime.Parse("2016/03/01"), dt_entrega = DateTime.Parse("2016/05/01"), ds_observacao = "Exemplo para o manual do usuário do W/Transportador" });
            //Save(new Entrega { ds_NFE = "35160972456809001709550010000114581062554946", id_ocorrencia = 2, dt_inclusao = DateTime.Parse("2016/03/01"), dt_entrega = DateTime.Parse("2016/05/01")});
        }
    }
}