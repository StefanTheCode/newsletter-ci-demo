// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using System.Reflection;

namespace Newsletter.ArchitectureTests;

internal static class Layers
{
    public static readonly Assembly Domain = typeof(Newsletter.Domain.Entities.Subscriber).Assembly;
    public static readonly Assembly Application = typeof(Newsletter.Application.Exceptions.NewsletterException).Assembly;
    public static readonly Assembly Infrastructure = typeof(Newsletter.Infrastructure.DependencyInjection).Assembly;
    public static readonly Assembly Api = typeof(Newsletter.Api.Contracts.CreateSubscriberRequest).Assembly;

    public static Assembly ByName(string name) => name switch
    {
        nameof(Domain) => Domain,
        nameof(Application) => Application,
        nameof(Infrastructure) => Infrastructure,
        nameof(Api) => Api,
        _ => throw new ArgumentOutOfRangeException(nameof(name), name, "Unknown layer."),
    };
}
