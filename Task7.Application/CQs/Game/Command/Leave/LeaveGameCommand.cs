using MediatR;

namespace Task7.Application.CQs.Game.Command.Leave;

public class LeaveGameCommand : IRequest<Domain.Game>
{
    public Guid ConnectionId { get; set; }
    public string PlayerName { get; set; }

    public LeaveGameCommand(Guid connectionId, string playerName)
    {
        ConnectionId = connectionId;
        PlayerName = playerName;
    }
}