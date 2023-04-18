FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5050

ENV ASPNETCORE_URLS=http://+:5050

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["App/OcelotAPIGateway/OcelotAPIGateway.csproj", "App/OcelotAPIGateway/"]
RUN dotnet restore "App/OcelotAPIGateway/OcelotAPIGateway.csproj" --disable-parallel
COPY . .
WORKDIR "/src/App/OcelotAPIGateway"
RUN dotnet build "OcelotAPIGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OcelotAPIGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OcelotAPIGateway.dll"]
