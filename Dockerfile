#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ship.Api/Ship.Api.csproj", "Ship.Api/"]
COPY ["Ship.Bussiness/Ship.Bussiness.csproj", "Ship.Bussiness/"]
COPY ["Ship.Common/Ship.Common.csproj", "Ship.Common/"]
RUN dotnet restore "Ship.Api/Ship.Api.csproj"
COPY . .
WORKDIR "/src/Ship.Api"
RUN dotnet build "Ship.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ship.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ship.Api.dll"]
