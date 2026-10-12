// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

namespace Newsletter.Domain.Entities;

public sealed class Subscriber
{
    private Subscriber(Guid id, string email, string firstName, DateTime subscribedOnUtc, bool isActive)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        SubscribedOnUtc = subscribedOnUtc;
        IsActive = isActive;
    }

    public Guid Id { get; private set; }

    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public DateTime SubscribedOnUtc { get; private set; }

    public bool IsActive { get; private set; }

    public static Subscriber Create(string email, string firstName)
    {
        return new Subscriber(Guid.NewGuid(), email, firstName, DateTime.UtcNow, isActive: true);
    }
}
