// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using Microsoft.Extensions.DependencyInjection;

using Newsletter.Domain.Repositories;
using Newsletter.Infrastructure.Repositories;

namespace Newsletter.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ISubscriberRepository, InMemorySubscriberRepository>();
        services.AddSingleton<IArticleRepository, InMemoryArticleRepository>();

        return services;
    }
}
