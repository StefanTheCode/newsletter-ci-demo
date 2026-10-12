// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using Newsletter.Core.Helpers;

namespace Newsletter.Tests;

public sealed class EmailNormalizerTests
{
    [Fact]
    public void Normalize_TrimsAndLowercases()
    {
        Assert.Equal("ana@example.com", EmailNormalizer.Normalize("  Ana@Example.COM "));
    }
}
