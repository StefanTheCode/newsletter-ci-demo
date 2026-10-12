// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using Newsletter.Domain.Entities;

namespace Newsletter.Domain.Repositories;

public interface ISubscriberRepository
{
    Task<Subscriber?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Subscriber?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<List<Subscriber>> GetAllActiveAsync(CancellationToken cancellationToken = default);

    Task AddAsync(Subscriber subscriber, CancellationToken cancellationToken = default);
}
