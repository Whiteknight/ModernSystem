namespace ModernSystem.Numbers
{
    public static class ModernInt64
    {
        public static long? Parse(string s)
        {
            long i;
            bool ok = long.TryParse(s, out i);
            return ok ? (long?)i : null;
        }
    }
}
