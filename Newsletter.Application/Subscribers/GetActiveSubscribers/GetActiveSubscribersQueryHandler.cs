// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

using Newsletter.Domain.Entities;
using Newsletter.Domain.Repositories;

namespace Newsletter.Application.Subscribers.GetActiveSubscribers;

public sealed class GetActiveSubscribersQueryHandler
{
    private readonly ISubscriberRepository _subscriberRepository;

    public GetActiveSubscribersQueryHandler(ISubscriberRepository subscriberRepository)
    {
        _subscriberRepository = subscriberRepository;
    }

    public async Task<IReadOnlyList<SubscriberResponse>> HandleAsync(CancellationToken cancellationToken = default)
    {
        List<Subscriber> subscribers = await _subscriberRepository.GetAllActiveAsync(cancellationToken);

        return subscribers
            .Select(s => new SubscriberResponse(s.Id, s.Email, s.FirstName))
            .ToList();
    }
}
