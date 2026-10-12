// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

namespace Newsletter.Api.Contracts;

public sealed record CreateSubscriberRequest(string Email, string FirstName);
