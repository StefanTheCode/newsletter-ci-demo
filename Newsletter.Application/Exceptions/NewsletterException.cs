// Copyright (c) TheCodeMan. All rights reserved.
// Licensed under the MIT license.

namespace Newsletter.Application.Exceptions
{
    public sealed class NewsletterException : Exception
    {
        public NewsletterException(string message)
            : base(message)
        {
        }
    }
}
