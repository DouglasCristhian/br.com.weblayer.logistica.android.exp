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
    public class TimePickerHelper : DialogFragment, TimePickerDialog.IOnTimeSetListener
    {
        // TAG can be any string of your choice.
        public static readonly string TAG = "X:" + typeof(TimePickerHelper).Name.ToUpper();

        // Initialize this value to prevent NullReferenceExceptions.
        Action<DateTime> _dateSelectedHandler = delegate { };

        public static TimePickerHelper NewInstance(Action<DateTime> onDateSelected)
        {
            TimePickerHelper frag = new TimePickerHelper();
            frag._dateSelectedHandler = onDateSelected;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currently = DateTime.Now;
            TimePickerDialog dialog = new TimePickerDialog(Activity,
                this,
                currently.Hour,
                currently.Minute,
                false
            );
            return dialog;
        }

        //public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        public void OnTimeSet(TimePicker view, int hour, int minute)
        {
            // Note: monthOfYear is a value between 0 and 11, not 1 and 12!
            DateTime selectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, 0);
            _dateSelectedHandler(selectedDate);
        }
    }
}