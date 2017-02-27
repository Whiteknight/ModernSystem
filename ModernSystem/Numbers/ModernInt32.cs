namespace ModernSystem.Numbers
{
    public static class ModernInt32
    {
        public static int? Parse(string s)
        {
            int i;
            bool ok = int.TryParse(s, out i);
            return ok ? (int?)i : null;
        }
    }
}
