﻿using Microsoft.AspNetCore.Mvc;

namespace StratmanMedia.WebServices.Endpoints;

[ApiController]
public static class BaseEndpoint
{
    public static class WithRequest<TRequest>
    {
        public abstract class WithResponse<TResponse> : BaseController
        {
            public abstract Task<ActionResult<TResponse>> HandleAsync(
                TRequest request,
                CancellationToken cancellationToken = default
            );
        }

        public abstract class WithoutResponse : BaseController
        {
            public abstract Task<ActionResult> HandleAsync(
                TRequest request,
                CancellationToken cancellationToken = default
            );
        }
    }

    public static class WithoutRequest
    {
        public abstract class WithResponse<TResponse> : BaseController
        {
            public abstract Task<ActionResult<TResponse>> HandleAsync(
                CancellationToken cancellationToken = default
            );
        }

        public abstract class WithoutResponse : BaseController
        {
            public abstract Task<ActionResult> HandleAsync(
                CancellationToken cancellationToken = default
            );
        }
    }
}