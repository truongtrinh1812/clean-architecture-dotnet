using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Infra.Authentication
{
    public class AuthenticationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IEnumerable<IAuthorizationRequirement> _authorizationRequirements;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthenticationBehavior<TRequest, TResponse>> _logger;

        public AuthenticationBehavior(
            IAuthorizationService authorizationService,
            IEnumerable<IAuthorizationRequirement> authorizationRequirements,
            IHttpContextAccessor httpContextAccessor,
            ILogger<AuthenticationBehavior<TRequest, TResponse>> logger)
        {
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            _authorizationRequirements = authorizationRequirements ?? throw new ArgumentNullException(nameof(authorizationRequirements));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is not IAuthRequest)
            {
                return await next();
            }

            _logger.LogInformation("[{Prefix}] Starting AuthBehavior", nameof(AuthenticationBehavior<TRequest, TResponse>));
            var currentUser = _httpContextAccessor.HttpContext?.User;
            if (currentUser == null)
            {
                throw new Exception("You need to login.");
            }

            var result = await _authorizationService.AuthorizeAsync(
                _httpContextAccessor.HttpContext?.User,
                null,
                _authorizationRequirements.Where(x => x is TRequest));

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException(result.Failure?.FailedRequirements.First().ToString());
            }

            return await next();
        }
    }
}
