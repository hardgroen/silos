namespace Silos.Core.Testing;

public record class DummyCommand(DummyAggregateId Id) : ICommand {}