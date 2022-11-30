using AutoMapper;
using Task7.Application.Common.Mappings;

namespace Task7.Application.CQs.Game.Queries.GetAllGame;

public class GameDto : IMapWith<Domain.Game>
{
    public string PlayerName { get; set; }
    public Guid ConnectionId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Game, GameDto>()
            .ForMember(g => g.ConnectionId,
                c => c.MapFrom(g => g.ConnectionId))
            .ForMember(g => g.PlayerName,
                c => c.MapFrom(g => g.Players
                    .First().Name));
    }
}