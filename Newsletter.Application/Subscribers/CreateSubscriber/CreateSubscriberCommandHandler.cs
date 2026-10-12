// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using Newsletter.Application.Exceptions;
using Newsletter.Domain.Entities;
using Newsletter.Domain.Repositories;

namespace Newsletter.Application.Subscribers.CreateSubscriber;

public sealed class CreateSubscriberCommandHandler
{
    private readonly ISubscriberRepository _subscriberRepository;

    public CreateSubscriberCommandHandler(ISubscriberRepository subscriberRepository)
    {
        _subscriberRepository = subscriberRepository;
    }

    public async Task<Guid> HandleAsync(CreateSubscriberCommand command, CancellationToken cancellationToken = default)
    {
        Subscriber? existing = await _subscriberRepository.GetByEmailAsync(command.Email, cancellationToken);

        if (existing is not null)
        {
            throw new NewsletterException($"A subscriber with the email '{command.Email}' already exists.");
        }

        Subscriber subscriber = Subscriber.Create(command.Email, command.FirstName);

        await _subscriberRepository.AddAsync(subscriber, cancellationToken);

        return subscriber.Id;
    }
}
