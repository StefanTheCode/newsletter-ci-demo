// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using NetArchTest.Rules;

namespace Newsletter.ArchitectureTests;

public sealed class FeatureIsolationTests
{
    public static TheoryData<string, string> FeaturePairs => new()
    {
        { "Newsletter.Application.Subscribers", "Newsletter.Application.Articles" },
        { "Newsletter.Application.Articles", "Newsletter.Application.Subscribers" },
    };

    [Theory]
    [MemberData(nameof(FeaturePairs))]
    public void Feature_Should_Not_Depend_On_Another_Feature(string feature, string otherFeature)
    {
        TestResult result = Types
            .InAssembly(Layers.Application)
            .That()
            .ResideInNamespace(feature)
            .ShouldNot()
            .HaveDependencyOn(otherFeature)
            .GetResult();

        result.ShouldPass($"{feature} must not depend on {otherFeature}. Share through the Domain, not across features.");
    }
}
