using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Task7.Application.Common.Constants;
using Task7.Application.Interfaces;

namespace Task7.Application.CQs.Game.Queries.GetAllGame;

public class GetAllGameQueryHandler
    : IRequestHandler<GetAllGameQuery, GetAllGameVm>
{
    private readonly ITicTacToeDbContext _ticTacToeDbContext;
    private readonly IMapper _mapper;

    public GetAllGameQueryHandler(ITicTacToeDbContext ticTacToeDbContext, IMapper mapper)
    {
        _ticTacToeDbContext = ticTacToeDbContext;
        _mapper = mapper;
    }

    public async Task<GetAllGameVm> Handle(GetAllGameQuery request,
        CancellationToken cancellationToken)
    {
        var games = await _ticTacToeDbContext.Games
            .Where(g => g.Status != GameStatuses.Completed)
            .ProjectTo<GameDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetAllGameVm() { Games = games };
    }
}