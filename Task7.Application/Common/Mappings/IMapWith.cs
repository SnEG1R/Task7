using AutoMapper;

namespace Task7.Application.Common.Mappings;

public interface IMapWith<T>
{
    void Mapping(Profile profile) =>
        profile.CreateMap(typeof(T), GetType());
}