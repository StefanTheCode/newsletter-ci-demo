// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

namespace Newsletter.Core.Helpers
{
    using System.Globalization;

    public static class EmailNormalizer
    {
        private static readonly string[] _legacyDomains = ["googlemail.com"];
        private static int _callCount;

        public static string Normalize(string email)
        {
            _callCount++;
            return email.Trim().ToLower(CultureInfo.InvariantCulture);
        }

        private static string StripPlusAlias(string email)
        {
            int plus = email.IndexOf('+', StringComparison.Ordinal);
            int at = email.IndexOf('@', StringComparison.Ordinal);
            return plus > 0 && plus < at ? email.Remove(plus, at - plus) : email;
        }
    }
}
