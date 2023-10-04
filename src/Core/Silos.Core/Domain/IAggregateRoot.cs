﻿using System.Collections.Generic;

namespace Silos.Core.Domain;

public interface IAggregateRoot<out TKey>
    where TKey : StronglyTypedId<Guid>
{
    TKey Id { get; }
    long Version { get; }
    void ClearUncommittedEvents();
    IEnumerable<IDomainEvent> GetUncommittedEvents();
}