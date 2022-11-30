FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Task7.Web/Task7.Web.csproj", "Task7.Web/"]
COPY ["Task7.Persistence/Task7.Persistence.csproj", "Task7.Persistence/"]
COPY ["Task7.Application/Task7.Application.csproj", "Task7.Application/"]
COPY ["Task7.Domain/Task7.Domain.csproj", "Task7.Domain/"]
RUN dotnet restore "Task7.Web/Task7.Web.csproj"
COPY . .
WORKDIR "/src/Task7.Web"
RUN dotnet build "Task7.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Task7.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Task7.Web.dll"]
