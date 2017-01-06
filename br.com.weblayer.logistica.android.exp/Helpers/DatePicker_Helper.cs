using System;
using Android.App;
using Android.OS;
using Android.Widget;

namespace br.com.weblayer.logistica.android.exp.Helpers
{
    public class DatePickerHelper : DialogFragment, DatePickerDialog.IOnDateSetListener

    {
        public static readonly string TAG = "X:" + typeof(DatePickerHelper).Name.ToUpper();
        Action<DateTime> _dateSelectedHandler = delegate { };

        public static DatePickerHelper NewInstance(Action<DateTime> onDateSelected)
        {
            DatePickerHelper frag = new DatePickerHelper();
            frag._dateSelectedHandler = onDateSelected;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currently = DateTime.Now;
            DatePickerDialog dialog = new DatePickerDialog(Activity,this,currently.Year,currently.Month,currently.Day);
            return dialog;
        }

        public void OnDateSet(Android.Widget.DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);

            _dateSelectedHandler(selectedDate);
        }
    }
}