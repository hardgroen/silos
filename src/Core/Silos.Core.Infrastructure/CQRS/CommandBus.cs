﻿namespace Silos.Core.Infrastructure.CQRS;

public class CommandBus : ICommandBus
{
    private readonly IMediator _mediator;
    private readonly ILogger<CommandBus> _logger;

    public CommandBus(IMediator mediator, ILogger<CommandBus> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public Task Send<TCommand>(TCommand command) where TCommand : ICommand
    {
        _logger.LogInformation("Sending command: {command}", command);
        return _mediator.Send(command);
    }
}
