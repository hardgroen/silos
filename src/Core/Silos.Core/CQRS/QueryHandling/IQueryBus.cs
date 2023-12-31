﻿namespace Silos.Core.CQRS.QueryHandling;

public interface IQueryBus
{
    Task<TResponse> Send<TResponse>(IQuery<TResponse> query);
}
