namespace ModernSystem.Numbers
{

    public static class ModernDouble
    {
        public static double? Parse(string s)
        {
            double i;
            bool ok = double.TryParse(s, out i);
            return ok ? (double?)i : null;
        }

        public static double ParseOrDefault(string s, double defaultValue)
        {
            double i;
            return double.TryParse(s, out i) ? i : defaultValue;
        }
    }
}
