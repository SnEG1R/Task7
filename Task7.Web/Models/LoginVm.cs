using AutoMapper;
using Task7.Application.Common.Mappings;
using Task7.Application.CQs.User.Commands.Login;

namespace Task7.Web.Models;

public class LoginVm : IMapWith<LoginCommand>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<LoginVm, LoginCommand>()
            .ForMember(l => l.Name,
                c => c.MapFrom(l => l.Name));
    }
}