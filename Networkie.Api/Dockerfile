FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 2876

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Networkie.Api/Networkie.Api.csproj", "Networkie.Api/"]
COPY ["Networkie.Application/Networkie.Application.csproj", "Networkie.Application/"]
COPY ["Networkie.Persistence.Abstractions/Networkie.Persistence.Abstractions.csproj", "Networkie.Persistence.Abstractions/"]
COPY ["Networkie.Entities/Networkie.Entities.csproj", "Networkie.Entities/"]
COPY ["Networkie.Infrastructure/Networkie.Infrastructure.csproj", "Networkie.Infrastructure/"]
COPY ["Networkie.Persistence/Networkie.Persistence.csproj", "Networkie.Persistence/"]
COPY ["Networkie.Persistence.EntityFrameworkCore/Networkie.Persistence.EntityFrameworkCore.csproj", "Networkie.Persistence.EntityFrameworkCore/"]
RUN dotnet restore "Networkie.Api/Networkie.Api.csproj"
COPY . .
WORKDIR "/src/Networkie.Api"
RUN dotnet build "Networkie.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Networkie.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Networkie.Api.dll"]
