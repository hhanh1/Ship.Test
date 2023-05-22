namespace Ship.Common.Extentions
{
    public static class StringExtension
    {
        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        public static DateTime ToDateTime(this string value)
        {
            var result = DateTime.MinValue;
            if (double.TryParse(value, out double d) && !double.IsNaN(d) && !double.IsInfinity(d))
            {
                result = DateTime.FromOADate(double.Parse(value));
            }
            else
            {
                DateTime.TryParse(value, out result);
            }

            return result;
        }
        public static decimal ToDecimal(this string value)
        {
            var result = decimal.Zero;
            decimal.TryParse(value, out result);
            return result;
        }
        public static int ToInt(this string value)
        {
            var result = 0;
            int.TryParse(value, out result);
            return result;
        }
    }
}
