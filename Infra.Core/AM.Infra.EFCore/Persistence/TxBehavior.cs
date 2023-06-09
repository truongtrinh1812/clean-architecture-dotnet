﻿using AM.Core.Domain.Entities;
using AM.Core.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Text.Json;

namespace AM.Infra.EFCore.Persistence
{
    public class TxBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : notnull, IRequest<TResponse>
         where TResponse : notnull
    {
        private readonly IDomainEventContext _domainEventContext;
        private readonly IDbFacadeResolver _dbFacadeResolver;
        private readonly IMediator _mediator;
        private readonly ILogger<TxBehavior<TRequest, TResponse>> _logger;

        public TxBehavior(IDbFacadeResolver dbFacadeResolver, IDomainEventContext domainEventContext,
            IMediator mediator, ILogger<TxBehavior<TRequest, TResponse>> logger)
        {
            _domainEventContext = domainEventContext ?? throw new ArgumentNullException(nameof(domainEventContext));
            _dbFacadeResolver = dbFacadeResolver ?? throw new ArgumentNullException(nameof(dbFacadeResolver));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is not ITxRequest)
            {
                return await next();
            }

            _logger.LogInformation("{Prefix} Handled command {MediatRRequest}", nameof(TxBehavior<TRequest, TResponse>), typeof(TRequest).FullName);
            _logger.LogDebug("{Prefix} Handled command {MediatRRequest} with content {RequestContent}", nameof(TxBehavior<TRequest, TResponse>), typeof(TRequest).FullName, JsonSerializer.Serialize(request));
            _logger.LogInformation("{Prefix} Open the transaction for {MediatRRequest}", nameof(TxBehavior<TRequest, TResponse>), typeof(TRequest).FullName);
            var strategy = _dbFacadeResolver.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                // Achieving atomicity
                await using var transaction = await _dbFacadeResolver.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

                var response = await next();
                _logger.LogInformation("{Prefix} Executed the {MediatRRequest} request", nameof(TxBehavior<TRequest, TResponse>), typeof(TRequest).FullName);

                await transaction.CommitAsync(cancellationToken);

                var domainEvents = _domainEventContext.GetDomainEvents().ToList();
                _logger.LogInformation("{Prefix} Published domain events for {MediatRRequest}", nameof(TxBehavior<TRequest, TResponse>), typeof(TRequest).FullName);

                var tasks = domainEvents
                    .Select(async @event =>
                    {
                        // because we have int identity
                        //var id = (response as dynamic)?.Id;
                        //@event.MetaData.Add("id", id);

                        await _mediator.Publish(new EventWrapper(@event), cancellationToken);
                        _logger.LogDebug(
                            "{Prefix} Published domain event {DomainEventName} with payload {DomainEventContent}", nameof(TxBehavior<TRequest, TResponse>), @event.GetType().FullName, JsonSerializer.Serialize(@event));
                    });

                await Task.WhenAll(tasks);

                return response;
            });
        }
    }
}
