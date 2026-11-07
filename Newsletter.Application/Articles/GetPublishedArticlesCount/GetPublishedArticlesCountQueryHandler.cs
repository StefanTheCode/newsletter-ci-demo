// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using Newsletter.Domain.Repositories;

namespace Newsletter.Application.Articles.GetPublishedArticlesCount;

public sealed class GetPublishedArticlesCountQueryHandler
{
    private readonly IArticleRepository _articleRepository;

    public GetPublishedArticlesCountQueryHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<PublishedArticlesCountResponse> HandleAsync(CancellationToken cancellationToken = default)
    {
        int count = await _articleRepository.CountPublishedAsync(cancellationToken);

        return new PublishedArticlesCountResponse(count);
    }
}
