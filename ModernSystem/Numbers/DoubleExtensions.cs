using System;

namespace ModernSystem.Numbers
{
    public static class DoubleExtensions
    {
        public const double DefaultEpsilon = 0.00000001;

        public static bool AlmostEquals(this double a, double b, double epsilon = DefaultEpsilon)
        {
            var diff = Math.Abs(a - b);
            return diff < epsilon;
        }
    }
}
