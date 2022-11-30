using MediatR;

namespace Task7.Application.CQs.Game.Command.Restart;

public class RestartGameCommand : IRequest<Domain.Game>
{
    public Guid ConnectionId { get; set; }

    public RestartGameCommand(Guid connectionId)
    {
        ConnectionId = connectionId;
    }
}