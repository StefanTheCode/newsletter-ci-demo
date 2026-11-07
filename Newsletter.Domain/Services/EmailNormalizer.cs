// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using System.Globalization;

namespace Newsletter.Domain.Services;

public static class EmailNormalizer
{
    public static string Normalize(string email)
    {
        return email.Trim().ToLower(CultureInfo.InvariantCulture);
    }
}
