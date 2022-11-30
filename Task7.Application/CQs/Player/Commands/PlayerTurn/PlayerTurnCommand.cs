using MediatR;

namespace Task7.Application.CQs.Player.Commands.PlayerTurn;

public class PlayerTurnCommand : IRequest<Domain.Game>
{
    public string PlayerName { get; set; }
    public Guid ConnectionId { get; set; }
    public int PositionField { get; set; }
}