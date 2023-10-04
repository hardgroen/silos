namespace Silos.Core.CQRS.CommandHandling;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{ }