// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

namespace Newsletter.Application.Commands;

public sealed record CreateSubscriberCommand(string Email, string FirstName);
