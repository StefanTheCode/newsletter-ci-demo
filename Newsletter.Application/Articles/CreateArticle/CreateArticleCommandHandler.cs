// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using Newsletter.Domain.Entities;
using Newsletter.Domain.Repositories;

namespace Newsletter.Application.Articles.CreateArticle;

public sealed class CreateArticleCommandHandler
{
    private readonly IArticleRepository _articleRepository;

    public CreateArticleCommandHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<Guid> HandleAsync(CreateArticleCommand command, CancellationToken cancellationToken = default)
    {
        Article article = Article.Create(command.Title, command.Content);

        await _articleRepository.AddAsync(article, cancellationToken);

        return article.Id;
    }
}
