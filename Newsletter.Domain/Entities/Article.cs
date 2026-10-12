// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

namespace Newsletter.Domain.Entities;

public sealed class Article
{
    private Article(Guid id, string title, string content)
    {
        Id = id;
        Title = title;
        Content = content;
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string Content { get; private set; }

    public string? Slug { get; private set; }

    public DateTime? PublishedOnUtc { get; private set; }

    public bool IsPublished { get; private set; }

    public static Article Create(string title, string content)
    {
        return new Article(Guid.NewGuid(), title, content);
    }

    public void Publish()
    {
        if (IsPublished)
        {
            throw new InvalidOperationException("Article is already published.");
        }

        IsPublished = true;
        Slug = Title.Trim().ToLowerInvariant().Replace(' ', '-');
        PublishedOnUtc = DateTime.UtcNow;
    }
}
