// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

namespace Newsletter.Application.Articles.GetPublishedArticles;

public sealed record ArticleResponse(Guid Id, string Title, DateTime? PublishedOnUtc);
