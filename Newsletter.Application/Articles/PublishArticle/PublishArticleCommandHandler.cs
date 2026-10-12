// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using Newsletter.Application.Commands;
using Newsletter.Application.Exceptions;
using Newsletter.Domain.Entities;
using Newsletter.Domain.Repositories;

namespace Newsletter.Application.Handlers;

public sealed class PublishArticleCommandHandler
{
    private readonly IArticleRepository _articleRepository;

    public PublishArticleCommandHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task HandleAsync(PublishArticleCommand command, CancellationToken cancellationToken = default)
    {
        Article? article = await _articleRepository.GetByIdAsync(command.ArticleId, cancellationToken);

        if (article is null)
        {
            throw new NewsletterException($"Article with id '{command.ArticleId}' was not found.");
        }

        article.Publish();
    }
}
