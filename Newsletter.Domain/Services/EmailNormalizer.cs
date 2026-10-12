// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

namespace Newsletter.Core.Helpers
{
    using System.Globalization;

    public static class EmailNormalizer
    {
        public static string Normalize(string email)
        {
            return email.Trim().ToLower(CultureInfo.InvariantCulture);
        }
    }
}
