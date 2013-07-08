using System.Globalization;

namespace Catrobat.Core.Misc.Helpers
{
    public static class FormatHelper
    {
        public static int ParseInt(string value)
        {
            return int.Parse(value, CultureInfo.InvariantCulture);
        }

        public static double ParseDouble(string value)
        {
            return double.Parse(value, CultureInfo.InvariantCulture);
        }

        public static float ParseFloat(string value)
        {
            return float.Parse(value, CultureInfo.InvariantCulture);
        }

        public static string ConvertInt(int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static string ConvertDouble(double value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static string ConvertFloat(float value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}