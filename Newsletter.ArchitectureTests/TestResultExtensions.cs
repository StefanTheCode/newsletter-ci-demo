// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using NetArchTest.Rules;

namespace Newsletter.ArchitectureTests;

internal static class TestResultExtensions
{
    public static void ShouldPass(this TestResult result, string rule)
    {
        if (result.IsSuccessful)
        {
            return;
        }

        IEnumerable<string> violations = result.FailingTypeNames ?? [];

        Assert.Fail($"{rule}\nViolating types:\n  - {string.Join("\n  - ", violations)}");
    }
}
