#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Crosscutting/Silos.IdentityServer/Silos.IdentityServer.csproj", "src/Crosscutting/Silos.IdentityServer/"]
COPY ["src/Core/Silos.Core/Silos.Core.csproj", "src/Core/Silos.Core/"]
RUN dotnet restore "src/Crosscutting/Silos.IdentityServer/Silos.IdentityServer.csproj"
COPY . .
WORKDIR "/src/src/Crosscutting/Silos.IdentityServer"
RUN dotnet build "Silos.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Silos.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Silos.IdentityServer.dll"]