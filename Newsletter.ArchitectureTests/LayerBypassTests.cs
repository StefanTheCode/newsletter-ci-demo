// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using NetArchTest.Rules;

namespace Newsletter.ArchitectureTests;

public sealed class LayerBypassTests
{
    [Fact]
    public void Api_Should_Not_Use_Repositories_Directly()
    {
        TestResult result = Types
            .InAssembly(Layers.Api)
            .ShouldNot()
            .HaveDependencyOn("Newsletter.Domain.Repositories")
            .GetResult();

        result.ShouldPass("Endpoints must go through an Application handler, never straight to a repository.");
    }
}
