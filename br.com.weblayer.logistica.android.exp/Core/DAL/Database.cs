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
using Environment = System.Environment;
using br.com.weblayer.logistica.android.exp.Core.Model;

namespace br.com.weblayer.logistica.android.exp.Core.DAL
{
    public class Database : SQLiteConnection
    {
        private static readonly string Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "database.db");

        public Database(string databasePath, bool storeDateTimeAsTicks = true) : base(databasePath, storeDateTimeAsTicks)
        {
        }

        public Database(string databasePath, SQLiteOpenFlags openFlags, bool storeDateTimeAsTicks = true) : base(databasePath, openFlags, storeDateTimeAsTicks)
        {
        }


        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(Path, true);
        }

        public static void Initialize()
        {
            CreateDatabase(GetConnection());
            new EntregaRepository().MakeDataMock();
            new OcorrenciaRepository().MakeDataMock();
        }

        private static void CreateDatabase(SQLiteConnection connection)
        {

            CreateTables(connection);
        }

        private static void CreateTables(SQLiteConnection connection)
        {
            using (connection)
            {
                connection.CreateTable<Entrega>();
                connection.CreateTable<Ocorrencia>();
            }
        }
    }
}