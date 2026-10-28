FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ScrabbleScoreboard.Player.Api/ScrabbleScoreboard.Player.Api.csproj", "ScrabbleScoreboard.Player.Api/"]
COPY ["ScrabbleScoreboard.Domain/ScrabbleScoreboard.Domain.csproj", "ScrabbleScoreboard.Domain/"]
RUN dotnet restore "ScrabbleScoreboard.Player.Api/ScrabbleScoreboard.Player.Api.csproj"
COPY . .
WORKDIR "/src/ScrabbleScoreboard.Player.Api"
RUN dotnet build "./ScrabbleScoreboard.Player.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ScrabbleScoreboard.Player.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScrabbleScoreboard.Player.Api.dll"]
