#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Silos.Customers/Silos.Customers.csproj", "src/Services/Silos.Customers/"]
COPY ["src/Core/Silos.Core.Infrastructure/Silos.Core.Infrastructure.csproj", "src/Core/Silos.Core.Infrastructure/"]
COPY ["src/Core/Silos.Core/Silos.Core.csproj", "src/Core/Silos.Core/"]
RUN dotnet restore "src/Services/Silos.Customers/Silos.Customers.csproj"
COPY . .
WORKDIR "/src/src/Services/Silos.Customers"
RUN dotnet build "Silos.Customers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Silos.Customers.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Silos.Customers.dll"]