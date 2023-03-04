using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.Globalization.NumberFormatting;

namespace R3peat
{
    internal class NumberBoxIntegerFormatter :  ModernWpf.Controls.INumberBoxNumberFormatter
    {
        DecimalFormatter _formatter;
        public NumberBoxIntegerFormatter()
        {
            _formatter = new DecimalFormatter();
            _formatter.FractionDigits = 0;
        }
        public string FormatDouble(double value)
        {
            return _formatter.FormatDouble(Math.Floor(value));
        }

        public double? ParseDouble(string text)
        {
            return _formatter.ParseDouble(text);
        }
    }
}
