namespace ModernSystem.Numbers
{
    public static class ModernFloat
    {
        public static float? Parse(string s)
        {
            float i;
            bool ok = float.TryParse(s, out i);
            return ok ? (float?)i : null;
        }
    }
}
