namespace br.com.weblayer.logistica.android.exp.Adapters
{
    class mySpinner
    {
        public int id;
        public string ds;

        public mySpinner(int idprod, string dsprod)
        {
            id = idprod;
            ds = dsprod;
        }

        public int Id()
        {
            return id;
        }

        public override string ToString()
        {
            return ds;
        }
    }
}