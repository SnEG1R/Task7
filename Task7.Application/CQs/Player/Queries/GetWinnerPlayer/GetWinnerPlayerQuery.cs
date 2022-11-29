using MediatR;

namespace Task7.Application.CQs.Player.Queries.GetWinnerPlayer;

public class GetWinnerPlayerQuery : IRequest<string?>
{
    public Guid ConnetionId { get; set; }
}