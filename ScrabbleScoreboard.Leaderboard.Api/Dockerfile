FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ScrabbleScoreboard.Leaderboard.Api/ScrabbleScoreboard.Leaderboard.Api.csproj", "ScrabbleScoreboard.Leaderboard.Api/"]
COPY ["ScrabbleScoreboard.Domain/ScrabbleScoreboard.Domain.csproj", "ScrabbleScoreboard.Domain/"]
RUN dotnet restore "ScrabbleScoreboard.Leaderboard.Api/ScrabbleScoreboard.Leaderboard.Api.csproj"
COPY . .
WORKDIR "/src/ScrabbleScoreboard.Leaderboard.Api"
RUN dotnet build "./ScrabbleScoreboard.Leaderboard.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ScrabbleScoreboard.Leaderboard.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScrabbleScoreboard.Leaderboard.Api.dll"]
