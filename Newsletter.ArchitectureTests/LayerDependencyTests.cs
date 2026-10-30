// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using NetArchTest.Rules;

namespace Newsletter.ArchitectureTests;

public sealed class LayerDependencyTests
{
    public static TheoryData<string, string> ForbiddenDependencies => new()
    {
        { "Domain", "Newsletter.Application" },
        { "Domain", "Newsletter.Infrastructure" },
        { "Domain", "Newsletter.Api" },
        { "Application", "Newsletter.Infrastructure" },
        { "Application", "Newsletter.Api" },
        { "Infrastructure", "Newsletter.Api" },
    };

    [Theory]
    [MemberData(nameof(ForbiddenDependencies))]
    public void Layer_Should_Not_Depend_On_Outer_Layer(string layer, string forbiddenNamespace)
    {
        TestResult result = Types
            .InAssembly(Layers.ByName(layer))
            .ShouldNot()
            .HaveDependencyOn(forbiddenNamespace)
            .GetResult();

        result.ShouldPass($"{layer} must not depend on {forbiddenNamespace}. Dependencies point inward, toward the Domain.");
    }

    [Fact]
    public void Domain_Should_Not_Depend_On_Frameworks()
    {
        TestResult result = Types
            .InAssembly(Layers.Domain)
            .ShouldNot()
            .HaveDependencyOnAny(
                "System.Text.Json",
                "Microsoft.AspNetCore",
                "Microsoft.EntityFrameworkCore",
                "Microsoft.Extensions.DependencyInjection")
            .GetResult();

        result.ShouldPass(
            "Domain must stay framework-free. Move serialization, persistence and DI concerns to Api or Infrastructure.");
    }
}
