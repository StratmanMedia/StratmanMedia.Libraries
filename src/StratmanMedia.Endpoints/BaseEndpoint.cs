using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StratmanMedia.Endpoints
{
    [ApiController]
    public static class BaseEndpoint
    {
        public static class WithRequest<TRequest>
        {
            public abstract class WithResponse<TResponse> : ControllerBase
            {
                public abstract Task<ActionResult<TResponse>> HandleAsync(
                    TRequest request,
                    CancellationToken cancellationToken = default
                );
            }

            public abstract class WithoutResponse : ControllerBase
            {
                public abstract Task<ActionResult> HandleAsync(
                    TRequest request,
                    CancellationToken cancellationToken = default
                );
            }
        }

        public static class WithoutRequest
        {
            public abstract class WithResponse<TResponse> : ControllerBase
            {
                public abstract Task<ActionResult<TResponse>> HandleAsync(
                    CancellationToken cancellationToken = default
                );
            }

            public abstract class WithoutResponse : ControllerBase
            {
                public abstract Task<ActionResult> HandleAsync(
                    CancellationToken cancellationToken = default
                );
            }
        }
    }
}
