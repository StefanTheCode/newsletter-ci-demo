// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using Newsletter.Domain.Entities;

namespace Newsletter.Domain.Repositories;

public interface IArticleRepository
{
    Task<Article?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<Article>> GetAllPublishedAsync(CancellationToken cancellationToken = default);

    Task<int> CountPublishedAsync(CancellationToken cancellationToken = default);

    Task AddAsync(Article article, CancellationToken cancellationToken = default);
}
