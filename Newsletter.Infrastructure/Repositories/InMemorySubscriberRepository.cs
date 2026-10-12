// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Concurrent;

using Newsletter.Domain.Entities;
using Newsletter.Domain.Repositories;

namespace Newsletter.Infrastructure.Repositories;

internal sealed class InMemorySubscriberRepository : ISubscriberRepository
{
    private readonly ConcurrentDictionary<Guid, Subscriber> _subscribers = new();

    public Task<Subscriber?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _subscribers.TryGetValue(id, out Subscriber? subscriber);

        return Task.FromResult(subscriber);
    }

    public Task<Subscriber?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        Subscriber? subscriber = _subscribers.Values
            .FirstOrDefault(s => s.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(subscriber);
    }

    public Task<List<Subscriber>> GetAllActiveAsync(CancellationToken cancellationToken = default)
    {
        List<Subscriber> activeSubscribers = _subscribers.Values
            .Where(s => s.IsActive)
            .ToList();

        return Task.FromResult(activeSubscribers);
    }

    public Task AddAsync(Subscriber subscriber, CancellationToken cancellationToken = default)
    {
        _subscribers.TryAdd(subscriber.Id, subscriber);

        return Task.CompletedTask;
    }
}
