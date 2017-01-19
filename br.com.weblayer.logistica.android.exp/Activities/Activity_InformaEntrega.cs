using System;
using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Android.Views;
using Android.Content;
using Android.Provider;
using Android.OS;
using ZXing.Mobile;
using br.com.weblayer.logistica.android.exp.Helpers;
using Android;
using Android.Content.PM;
using br.com.weblayer.logistica.android.exp.Core.Model;
using br.com.weblayer.logistica.android.exp.Core.BLL;
using static Android.Widget.AdapterView;
using br.com.weblayer.logistica.android.exp.Adapters;
using br.com.weblayer.logistica.android.exp.Core.DAL;
using Android.Graphics;
using JavaUri = Android.Net.Uri;
using System.IO;
using System.Globalization;

namespace br.com.weblayer.logistica.android.exp.Activities
{
    [Activity(MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class Activity_InformaEntrega : Activity_Base
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        public static string MyPREFERENCES = "MyPrefs";
        MobileBarcodeScanner scanner;
        private Java.IO.File imagefile;
        private Spinner spinnerOcorrencia;
        private List<mySpinner> ocorr;
        private EditText txtCodigoNF;
        private TextView lblCNPJ;
        private TextView lblNumeroNF;
        private EditText txtObservacao;
        private TextView txtDataEntrega;
        private TextView txtHoraEntrega;
        private TextView lblObservacao;
        private Button btnEscanearNF;
        private Button btnAnexarImagem;
        private Button btnEnviar;
        private Button btnCancelar;
        private Button btnEnviarViaEmail;
        private ImageView imageView;
        private byte[] bytes;
        private Android.Graphics.Bitmap bitmap;
        private Entrega entrega;
        private string operacao;
        private string spinOcorrencia;
        private string descricaoocorrencia;
        private int count;
        Android.Net.Uri contentUri;
        private bool camcheck;
        private bool lercheck;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.Activity_InformaEntrega;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RestoreForm();

                var jsonnota = Intent.GetStringExtra("JsonEntrega");
                if (jsonnota == null)
                {
                    entrega = null;
                }
                else
                {
                    entrega = new EntregaRepository().Get(int.Parse(jsonnota));
                    operacao = "selecionado";
                }

                FindViews();
                BindData();
                BindViews();

                ocorr = PopulateOcorrenciaList();
                spinnerOcorrencia.Adapter = new ArrayAdapter<mySpinner>(this, Android.Resource.Layout.SimpleListItem1, ocorr);

                if (entrega != null)
                    spinnerOcorrencia.SetSelection(getIndexByValue(spinnerOcorrencia, entrega.id_ocorrencia));
            
        }

        //DEFININDO OBJETOS E EVENTOS
        private int getIndexByValue(Spinner spinner, long myId)
        {
            int index = 0;

            var adapter = (ArrayAdapter<mySpinner>)spinner.Adapter;
            for (int i = 0; i < spinner.Count; i++)
            {
                if (adapter.GetItemId(i) == myId)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_toolbar, menu);

            if (operacao != "selecionado")
            {
                menu.RemoveItem(Resource.Id.action_deletar);
                menu.RemoveItem(Resource.Id.action_adicionar);
            }
            else
                menu.RemoveItem(Resource.Id.action_adicionar);

            return base.OnCreateOptionsMenu(menu);
        }

        private void FindViews()
        {
            txtCodigoNF = FindViewById<EditText>(Resource.Id.txtCodigoNF);
            spinnerOcorrencia = FindViewById<Spinner>(Resource.Id.spinnerOcorrencia);
            spinnerOcorrencia.ItemSelected += new EventHandler<ItemSelectedEventArgs>(SpinnerOcorrencia_ItemSelected);
            txtDataEntrega = FindViewById<TextView>(Resource.Id.txtDataEntrega);
            txtHoraEntrega = FindViewById<TextView>(Resource.Id.txtHoraEntrega);
            txtObservacao = FindViewById<EditText>(Resource.Id.txtObservacao);
            lblObservacao = FindViewById<TextView>(Resource.Id.lblObservacao);
            lblCNPJ = FindViewById<TextView>(Resource.Id.lblCNPJ);
            lblNumeroNF = FindViewById<TextView>(Resource.Id.lblNumeroNF);
            btnAnexarImagem = FindViewById<Button>(Resource.Id.btnAnexarImagem);
            btnEscanearNF = FindViewById<Button>(Resource.Id.btnEscanearNF);
            btnCancelar = FindViewById<Button>(Resource.Id.btnCancelar);
            btnEnviar = FindViewById<Button>(Resource.Id.btnEnviar);
            btnEnviarViaEmail = FindViewById<Button>(Resource.Id.btnEnviarViaEmail);
            imageView = FindViewById<ImageView>(Resource.Id.imageView);

            if (operacao == "selecionado")
            {
                spinnerOcorrencia.Enabled = false;
                txtObservacao.Focusable = false;
                txtCodigoNF.Focusable = false;

                btnEscanearNF.Visibility = ViewStates.Gone;
                btnAnexarImagem.Visibility = ViewStates.Gone;
                btnEnviar.Visibility = ViewStates.Gone;
                btnCancelar.Visibility = ViewStates.Gone;
            }
          

            imageView = FindViewById<ImageView>(Resource.Id.imageView);

            MobileBarcodeScanner.Initialize(Application);
            scanner = new MobileBarcodeScanner();

            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            this.Title = "Informar Entrega";
            toolbar.MenuItemClick += Toolbar_MenuItemClick;
        }

        private void BindViews()
        {
            if (entrega == null)
                return;

            txtCodigoNF.Text = entrega.ds_NFE;
            spinOcorrencia = entrega.id_ocorrencia.ToString();
            txtDataEntrega.Text = entrega.dt_entrega.Value.ToString("dd/MM/yyyy");
            txtHoraEntrega.Text = entrega.dt_entrega.Value.ToString("HH:mm");
            txtObservacao.Text = entrega.ds_observacao.ToString();

            if (entrega.ds_NFE.Length >= 34)
            {
                Substring_Helper sub = new Substring_Helper();
                lblCNPJ.Text = "CNPJ Emissor: " + sub.Substring_CNPJ(entrega.ds_NFE);
                lblNumeroNF.Text = "N�mero NF: " + sub.Substring_NumeroNF(entrega.ds_NFE) + "/" + sub.Substring_SerieNota(entrega.ds_NFE);
            }
            else
            {
                lblNumeroNF.Text = "N�mero NF: ";
            }


            if (entrega.Image != null)
            {
                ByteHelper helper = new ByteHelper();
                bitmap = helper.ByteArrayToImage(entrega.Image);
                imageView.SetImageBitmap(bitmap);
            }
        }

        private void BindModel()
        {
            if (entrega == null)
                entrega = new Entrega();

            entrega.ds_NFE = txtCodigoNF.Text.ToString();

            string datahora = (txtDataEntrega.Text + " " + txtHoraEntrega.Text);

            entrega.dt_entrega = DateTime.Parse(datahora);
            entrega.dt_inclusao = DateTime.Now;
            var minhaocorrencia = ocorr[spinnerOcorrencia.SelectedItemPosition];
            entrega.id_ocorrencia = minhaocorrencia.Id();
            entrega.ds_observacao = txtObservacao.Text.ToString();
            if (bytes != null)
            {
                entrega.Image = bytes;
                entrega.ds_ImageUri = imagefile.AbsolutePath;
            }            
        }

        private void BindData()
        {
            if (entrega == null)
            {
                txtDataEntrega.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtHoraEntrega.Text = DateTime.Now.ToString("HH:mm");
                btnEnviarViaEmail.Visibility = ViewStates.Gone;
            }

            if (entrega != null && entrega.ds_observacao == "")
            {
                txtObservacao.Visibility = ViewStates.Gone;
                lblObservacao.Visibility = ViewStates.Gone;
            }

            if (operacao != "selecionado")
            {
                txtDataEntrega.Click += TxtDataEntrega_Click;
                txtHoraEntrega.Click += TxtHoraEntrega_Click;
            }

            btnEscanearNF.Click += BtnEscanearNF_Click;
            btnAnexarImagem.Click += ValidarPermissoes;
            btnEnviar.Click += BtnEnviar_Click;
            btnCancelar.Click += BtnCancelar_Click;
            btnEnviarViaEmail.Click += BtnEnviarViaEmail_Click;
            txtCodigoNF.FocusChange += TxtCodigoNF_FocusChange;
        }

        private void TxtCodigoNF_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (!e.HasFocus)
            {
                Substring_Helper sub = new Substring_Helper();
                lblCNPJ.Text = "CNPJ Emissor: " + sub.Substring_CNPJ(txtCodigoNF.Text.ToString());
                string numero_serie = sub.Substring_NumeroNF(txtCodigoNF.Text.ToString());
                if (numero_serie != null)
                {
                    lblNumeroNF.Text = "N�mero NF: " + sub.Substring_NumeroNF(txtCodigoNF.Text.ToString()) + " / " +  sub.Substring_SerieNota(txtCodigoNF.Text.ToString());
                }
                else
                {
                    lblNumeroNF.Text = "N�mero NF: ";
                }              
            }       
        }

        private List<mySpinner> PopulateOcorrenciaList()
        {
            List<mySpinner> lista = new List<mySpinner>();
            var listaOcorrencias = new OcorrenciaRepository().List();

            lista.Add(new mySpinner(0, "Selecione.."));

            foreach (var item in listaOcorrencias)
            {
                lista.Add(new mySpinner(item.id, item.ds_descricao));
            }

            return lista;
        }

        private bool ValidateViews()
        {
            var validacao = true;
            if (txtCodigoNF.Length() == 0)
            {
                validacao = false;
                txtCodigoNF.Error = "Nota Fiscal inv�lida!";
            }

            if (spinnerOcorrencia.SelectedItemPosition == 0)
            {
                validacao = false;
                Toast.MakeText(this, "Por favor, selecione a ocorr�ncia", ToastLength.Short).Show();
            }

            //TODO: TERMINAR VALIDA��ES
            return validacao;
        }

        private void SpinnerOcorrencia_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            txtCodigoNF.ClearFocus();
            spinOcorrencia = spinnerOcorrencia.SelectedItem.ToString();
        }

        //EVENTOS CLICK
        private void BtnEnviarViaEmail_Click(object sender, EventArgs e)
        {
            //Enviar ap�s a inser��o
            SendByEmail();
        }

        private void BtnEnviar_Click(object sender, EventArgs e)
        {
            //Enviar no momento da inser��o
            Save();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void TxtDataEntrega_Click(object sender, EventArgs e)
        {
            DatePickerHelper frag = DatePickerHelper.NewInstance(delegate (DateTime time)
            {
                //var teste = DateTime.Now.ToString("hh:mm:ss");
                txtDataEntrega.Text = time.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"));
            });

            frag.Show(FragmentManager, DatePickerHelper.TAG);
        }

        private void TxtHoraEntrega_Click(object sender, EventArgs e)
        {           
            TimePickerHelper frag = TimePickerHelper.NewInstance(delegate (DateTime time)
            {
                txtHoraEntrega.Text = time.ToString("HH:mm");
            });

            frag.Show(FragmentManager, TimePickerHelper.TAG);
        }

        private void Toolbar_MenuItemClick(object sender, Android.Support.V7.Widget.Toolbar.MenuItemClickEventArgs e)
        {
            switch (e.Item.ItemId)
            {
                case Resource.Id.action_ajuda:
                    StartActivity(typeof(Activity_ManualUsuario));
                    break;

                case Resource.Id.action_deletar:
                    Delete();
                    break;
            }
        }

        private async void BtnEscanearNF_Click(object sender, EventArgs e)
        {
            scanner.UseCustomOverlay = false;
            scanner.TopText = "Aguarde o escaneamento do c�digo de barras";

            var result = await scanner.Scan();
            HandleScanResult(result);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void TirarFoto()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            imagefile = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), Java.Lang.String.ValueOf(count++) + ".jpeg");
            Android.Net.Uri tempuri = Android.Net.Uri.FromFile(imagefile);
            SaveForm();
            intent.PutExtra(MediaStore.ExtraOutput, tempuri);
            StartActivityForResult(intent, 0);
        }

        //EVENTOS RESULTADOS

        private void DefinirOcorrencia()
        {
            if (entrega.id_ocorrencia == 1)
                descricaoocorrencia = "ENTREGA";
            if (entrega.id_ocorrencia == 2)
                descricaoocorrencia = "INFORMATIVO";
            if (entrega.id_ocorrencia == 3)
                descricaoocorrencia = "REENTREGA";
            if (entrega.id_ocorrencia == 4)
                descricaoocorrencia = "DEVOLU��O";
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if ((requestCode == 111))
            {
                if (grantResults[0] == Permission.Granted)
                {
                    camcheck = true;
                }
                else
                    Toast.MakeText(this, "N�o � poss�vel usar a c�mera sem as devidas permiss�es", ToastLength.Long).Show();
                return;
            }

            if ((requestCode == 222))
            {
                if (grantResults[0] == Permission.Granted)
                {
                    lercheck = true;
                }
                else
                {
                    Toast.MakeText(this, "N�o � poss�vel usar a c�mera sem as devidas permiss�es", ToastLength.Long).Show();
                    return;
                }
            }

            if ((camcheck == true) && (lercheck == true))
            {
                TirarFoto();
            }
        }

        private void ValidarPermissoes(object sender, System.EventArgs e)
        {
            if (CheckSelfPermission(Manifest.Permission.ReadExternalStorage) == Permission.Granted)
            {
                if ((CheckSelfPermission(Manifest.Permission.Camera) == Android.Content.PM.Permission.Granted))
                {
                    TirarFoto();
                }
                else
                {
                    string[] permissionRequest = { Manifest.Permission.Camera };
                    RequestPermissions(permissionRequest, 111);
                }
            }
            else
            {
                string[] permissionRequest = { Manifest.Permission.ReadExternalStorage };
                RequestPermissions(permissionRequest, 222);
            }
        }

        public void HandleScanResult(ZXing.Result result)
        {
            if (result != null && !string.IsNullOrEmpty(result.Text))
            {
                txtCodigoNF.Text = result.Text;

                Substring_Helper sub = new Substring_Helper();
                lblCNPJ.Text = "CNPJ Emissor: " + sub.Substring_CNPJ(result.Text.ToString());
                lblNumeroNF.Text = "N�mero NF: " + sub.Substring_NumeroNF(result.Text.ToString()) + "/" + sub.Substring_SerieNota(result.Text.ToString());
            }
            else if (result.Text.Length < 44)
            {
                Toast.MakeText(this, "C�digo inv�lido! O c�digo de barras deve ter 44 caracteres", ToastLength.Long).Show();
                return;
            }
            else
            {
                Toast.MakeText(this, "Escaneamento cancelado!", ToastLength.Short).Show();
            }             
        }

        protected override void OnActivityResult(int requestCode, Android.App.Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 0)
            {
                switch (resultCode)
                {
                    case Android.App.Result.Ok:
                        if (imagefile.Exists())
                        {
                            //Salvar imagem na galeria
                            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                            contentUri = Android.Net.Uri.FromFile(imagefile);
                            mediaScanIntent.SetData(contentUri);
                            SendBroadcast(mediaScanIntent);

                            //Converter image para byte
                            Java.Net.URI juri = new Java.Net.URI(contentUri.ToString());
                            ByteHelper helper = new ByteHelper();
                            bytes = helper.imageToByteArray(juri, bytes);

                            System.IO.Stream stream = ContentResolver.OpenInputStream(contentUri);
                            imageView.SetImageBitmap(BitmapFactory.DecodeStream(stream));
                        }
                        else
                        {
                            Toast.MakeText(this, "O arquivo n�o foi salvo devido � um erro", ToastLength.Short).Show();
                        }
                        break;

                    case Result.Canceled:
                        break;

                    default:
                        break;
                }
            }
        }

        public void SendByEmail()
        {
            var email = new Intent(Android.Content.Intent.ActionSend);
            email.PutExtra(Android.Content.Intent.ExtraCc, "Testando 123");

            if (entrega.Image != null || entrega.ds_ImageUri != null)
            {
                imagefile = new Java.IO.File(entrega.ds_ImageUri);

                if (!imagefile.Exists())
                {
                    ByteHelper helper = new ByteHelper();
                    bitmap = helper.ByteArrayToImage(entrega.Image);

                    var stream = new FileStream(imagefile.AbsolutePath, FileMode.Create);
                    bitmap.Compress(Bitmap.CompressFormat.Jpeg, 50, stream);
                    stream.Close();

                    Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                    Android.Net.Uri uri = Android.Net.Uri.FromFile(imagefile);
                    mediaScanIntent.SetData(uri);
                    SendBroadcast(mediaScanIntent);
                }

                if (imagefile.Exists())
                {
                    ByteHelper helper = new ByteHelper();
                    bitmap = helper.ByteArrayToImage(entrega.Image);

                    var stream = new FileStream(imagefile.AbsolutePath, FileMode.Create);
                    bitmap.Compress(Bitmap.CompressFormat.Jpeg, 50, stream);
                    stream.Close();
                }

                Android.Net.Uri contentUri = JavaUri.FromFile(imagefile);
                email.PutExtra(Intent.ExtraStream, contentUri);
            }

            DefinirOcorrencia();
            email.PutExtra(Intent.ExtraSubject, "NFE: " + entrega.ds_NFE + "; Ocorr�ncia: " + entrega.id_ocorrencia.ToString() + "; Data: " + DateTime.Parse(entrega.dt_entrega.ToString()));
            email.PutExtra(Intent.ExtraText, "NFe: " + entrega.ds_NFE +
                                             "\nOcorr�ncia: " + descricaoocorrencia +
                                             "\nData de Inclus�o: " + entrega.dt_inclusao +
                                             "\nData de Entrega: " + entrega.dt_entrega +
                                             "\nObserva��o: " + entrega.ds_observacao);
            email.SetType("application/image");
            Intent.CreateChooser(email, "Send Email Via");

            try
            {
                StartActivityForResult(email, 0);
            }
            catch (Exception e)
            {
                Toast.MakeText(this, "Email n�o enviado devido � um erro:" + e.Message, ToastLength.Long).Show();
            }

        }



        //SAVE E RESTORE
        private void Delete()
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);

            alert.SetTitle("Tem certeza que deseja excluir este registro?");

            alert.SetNegativeButton("N�o", (senderAlert, args) =>
            {

            });

            alert.SetPositiveButton("Sim", (senderAlert, args) =>
            {
                try
                {
                    var Ent = new EntregaManager();
                    Ent.Delete(entrega);

                    Intent myIntent = new Intent(this, typeof(Activity_Menu));
                    myIntent.PutExtra("mensagem", Ent.mensagem);
                    SetResult(Android.App.Result.Ok, myIntent);
                    Finish();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                }

            });

            RunOnUiThread(() =>
            {
                alert.Show();
            });
        }

        private void Save()
        { 
            if (!ValidateViews())
                return;
            try
            {
                BindModel();

                var Ent = new EntregaManager();
                Ent.Save(entrega);

                Intent myIntent = new Intent(this, typeof(Activity_Menu));
                myIntent.PutExtra("mensagem", Ent.mensagem);
                SetResult(Result.Ok, myIntent);

                SendByEmail();

                Finish();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }

            
        }     

        private void SaveForm()
        {
            var prefs = Application.Context.GetSharedPreferences(MyPREFERENCES, FileCreationMode.WorldWriteable);
            var prefEditor = prefs.Edit();
            prefEditor.PutInt("Soma", count);
            prefEditor.Commit();
        }

        private void RestoreForm()
        {
            var prefs = Application.Context.GetSharedPreferences(MyPREFERENCES, FileCreationMode.WorldReadable);
            var somePref = prefs.GetInt("Soma", 0);
            count = somePref;
        }       
    }
}