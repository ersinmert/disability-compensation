#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DisabilityCompensation.API/DisabilityCompensation.API.csproj", "DisabilityCompensation.API/"]
RUN dotnet restore "./DisabilityCompensation.API/./DisabilityCompensation.API.csproj"
COPY . .
WORKDIR "/src/DisabilityCompensation.API"
RUN dotnet build "./DisabilityCompensation.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DisabilityCompensation.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DisabilityCompensation.API.dll"]