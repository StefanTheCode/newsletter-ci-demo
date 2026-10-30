// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using NetArchTest.Rules;

namespace Newsletter.ArchitectureTests;

public sealed class ConventionTests
{
    [Fact]
    public void Domain_Entities_Should_Be_Sealed()
    {
        TestResult result = Types
            .InAssembly(Layers.Domain)
            .That()
            .ResideInNamespace("Newsletter.Domain.Entities")
            .And()
            .AreClasses()
            .Should()
            .BeSealed()
            .GetResult();

        result.ShouldPass("Domain entities are not extension points - mark them sealed.");
    }

    [Fact]
    public void Repositories_In_Domain_Should_Be_Interfaces()
    {
        TestResult result = Types
            .InAssembly(Layers.Domain)
            .That()
            .HaveNameEndingWith("Repository")
            .Should()
            .BeInterfaces()
            .GetResult();

        result.ShouldPass("The Domain declares repository contracts (interfaces). Implementations belong in Infrastructure.");
    }

    [Fact]
    public void Handlers_Should_Be_Sealed_And_Live_In_A_Feature_Folder()
    {
        TestResult result = Types
            .InAssembly(Layers.Application)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .BeSealed()
            .And()
            .ResideInNamespaceMatching(@"^Newsletter\.Application\.\w+\.\w+$")
            .GetResult();

        result.ShouldPass("Handlers are sealed and live in Application/<Feature>/<UseCase>/.");
    }

    [Fact]
    public void Infrastructure_Implementations_Should_Not_Be_Public()
    {
        TestResult result = Types
            .InAssembly(Layers.Infrastructure)
            .That()
            .ResideInNamespace("Newsletter.Infrastructure.Repositories")
            .Should()
            .NotBePublic()
            .GetResult();

        result.ShouldPass("Repository implementations stay internal - consumers depend on the Domain interface only.");
    }
}
