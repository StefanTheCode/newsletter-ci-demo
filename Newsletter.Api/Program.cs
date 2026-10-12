// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using Newsletter.Api.Contracts;
using Newsletter.Application.Commands;
using Newsletter.Application.Exceptions;
using Newsletter.Application.Handlers;
using Newsletter.Domain.Entities;
using Newsletter.Domain.Repositories;
using Newsletter.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddInfrastructure();
builder.Services.AddTransient<CreateSubscriberCommandHandler>();
builder.Services.AddTransient<PublishArticleCommandHandler>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapPost("/api/subscribers", async (
    CreateSubscriberRequest request,
    CreateSubscriberCommandHandler handler,
    CancellationToken cancellationToken) =>
{
    try
    {
        var command = new CreateSubscriberCommand(request.Email, request.FirstName);
        Guid id = await handler.HandleAsync(command, cancellationToken);
        return Results.Created($"/api/subscribers/{id}", new { id });
    }
    catch (NewsletterException ex)
    {
        return Results.Conflict(new { error = ex.Message });
    }
});

app.MapGet("/api/subscribers", async (ISubscriberRepository repository, CancellationToken cancellationToken) =>
{
    List<Subscriber> subscribers = await repository.GetAllActiveAsync(cancellationToken);
    return Results.Ok(subscribers);
});

app.MapPost("/api/articles", async (CreateArticleRequest request, IArticleRepository repository, CancellationToken cancellationToken) =>
{
    Article article = Article.Create(request.Title, request.Content);
    await repository.AddAsync(article, cancellationToken);
    return Results.Created($"/api/articles/{article.Id}", new { id = article.Id });
});

app.MapPost("/api/articles/{id:guid}/publish", async (Guid id, PublishArticleCommandHandler handler, CancellationToken cancellationToken) =>
{
    try
    {
        await handler.HandleAsync(new PublishArticleCommand(id), cancellationToken);
        return Results.NoContent();
    }
    catch (NewsletterException ex)
    {
        return Results.NotFound(new { error = ex.Message });
    }
});

app.MapGet("/api/articles", async (IArticleRepository repository, CancellationToken cancellationToken) =>
{
    List<Article> articles = await repository.GetAllPublishedAsync(cancellationToken);
    return Results.Ok(articles);
});

await app.RunAsync();
