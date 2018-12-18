using System;

namespace LoansManager.Util
{
    public static class StringExtensions
    {
        public static bool Empty(this string @this)
            => string.IsNullOrEmpty(@this);
    }
}
