// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

namespace Newsletter.Application.Subscribers.GetActiveSubscribers;

public sealed record SubscriberResponse(Guid Id, string Email, string FirstName);
