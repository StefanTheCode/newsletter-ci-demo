// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Concurrent;

using Newsletter.Domain.Entities;
using Newsletter.Domain.Repositories;

namespace Newsletter.Infrastructure.Repositories;

internal sealed class InMemoryArticleRepository : IArticleRepository
{
    private readonly ConcurrentDictionary<Guid, Article> _articles = new();

    public Task<Article?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _articles.TryGetValue(id, out Article? article);

        return Task.FromResult(article);
    }

    public Task<List<Article>> GetAllPublishedAsync(CancellationToken cancellationToken = default)
    {
        List<Article> publishedArticles = _articles.Values
            .Where(a => a.IsPublished)
            .ToList();

        return Task.FromResult(publishedArticles);
    }

    public Task<int> CountPublishedAsync(CancellationToken cancellationToken = default)
    {
        int count = _articles.Values.Count(a => a.IsPublished);

        return Task.FromResult(count);
    }

    public Task AddAsync(Article article, CancellationToken cancellationToken = default)
    {
        _articles.TryAdd(article.Id, article);

        return Task.CompletedTask;
    }
}
