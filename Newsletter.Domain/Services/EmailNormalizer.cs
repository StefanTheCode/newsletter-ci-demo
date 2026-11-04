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

#pragma warning disable S1144 // TODO: will be used for Gmail aliases later
    private static string RemoveDots(string localPart)
    {
        return localPart.Replace(".", string.Empty, StringComparison.Ordinal);
    }
#pragma warning restore S1144
}
