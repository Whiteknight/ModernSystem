namespace ModernSystem.Numbers
{
    public static class ModernDecimal
    {
        public static decimal? Parse(string s)
        {
            decimal i;
            bool ok = decimal.TryParse(s, out i);
            return ok ? (decimal?)i : null;
        }
    }
}
