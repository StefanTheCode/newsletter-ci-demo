// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using Newsletter.Domain.Entities;
using Newsletter.Domain.Repositories;

namespace Newsletter.Application.Articles.GetPublishedArticles;

public sealed class GetPublishedArticlesQueryHandler
{
    private readonly IArticleRepository _articleRepository;

    public GetPublishedArticlesQueryHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<IReadOnlyList<ArticleResponse>> HandleAsync(CancellationToken cancellationToken = default)
    {
        List<Article> articles = await _articleRepository.GetAllPublishedAsync(cancellationToken);

        return articles
            .Select(a => new ArticleResponse(a.Id, a.Title, a.PublishedOnUtc))
            .ToList();
    }
}
