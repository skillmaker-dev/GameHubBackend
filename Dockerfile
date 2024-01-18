FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 433

ENV ASPNETCORE_ENVIRONMENT="development"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GameHubApi/GameHubApi.csproj", "GameHubApi/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "GameHubApi/GameHubApi.csproj"
COPY . .
WORKDIR "/src/GameHubApi"
RUN dotnet build "GameHubApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GameHubApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameHubApi.dll"]
