#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Crosscutting/Silos.ApiGateway/Silos.ApiGateway.csproj", "src/Crosscutting/Silos.ApiGateway/"]
RUN dotnet restore "src/Crosscutting/Silos.ApiGateway/Silos.ApiGateway.csproj"
COPY . .
WORKDIR "/src/src/Crosscutting/Silos.ApiGateway"
RUN dotnet build "Silos.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Silos.ApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Silos.ApiGateway.dll"]