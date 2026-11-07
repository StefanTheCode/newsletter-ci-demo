// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using Newsletter.Api.Contracts;
using Newsletter.Application.Articles.CreateArticle;
using Newsletter.Application.Articles.GetPublishedArticles;
using Newsletter.Application.Articles.PublishArticle;
using Newsletter.Application.Exceptions;
using Newsletter.Application.Subscribers.CreateSubscriber;
using Newsletter.Application.Subscribers.GetActiveSubscribers;
using Newsletter.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Nullable annotations are compile-time only. Make the JSON boundary respect them at runtime too.
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.RespectNullableAnnotations = true;
    options.SerializerOptions.RespectRequiredConstructorParameters = true;
});

builder.Services.AddInfrastructure();
builder.Services.AddTransient<CreateSubscriberCommandHandler>();
builder.Services.AddTransient<GetActiveSubscribersQueryHandler>();
builder.Services.AddTransient<CreateArticleCommandHandler>();
builder.Services.AddTransient<PublishArticleCommandHandler>();
builder.Services.AddTransient<GetPublishedArticlesQueryHandler>();

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

app.MapGet("/api/subscribers", async (GetActiveSubscribersQueryHandler handler, CancellationToken cancellationToken) =>
    Results.Ok(await handler.HandleAsync(cancellationToken)));

app.MapPost("/api/articles", async (CreateArticleRequest request, CreateArticleCommandHandler handler, CancellationToken cancellationToken) =>
{
    Guid id = await handler.HandleAsync(new CreateArticleCommand(request.Title, request.Content), cancellationToken);
    return Results.Created($"/api/articles/{id}", new { id });
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

app.MapGet("/api/articles", async (GetPublishedArticlesQueryHandler handler, CancellationToken cancellationToken) =>
    Results.Ok(await handler.HandleAsync(cancellationToken)));

app.MapGet("/api/subscribers/count", async (GetActiveSubscribersQueryHandler handler, CancellationToken cancellationToken) =>
{
    var subscribers = await handler.HandleAsync(cancellationToken);
    return Results.Ok(new { count = subscribers.Count });
});

await app.RunAsync();
