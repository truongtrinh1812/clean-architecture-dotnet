FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["App/AppGateway/AppGateway.csproj", "App/AppGateway/"]
RUN dotnet restore "App/AppGateway/AppGateway.csproj" --disable-parallel
COPY . .
WORKDIR "/src/App/AppGateway"
RUN dotnet build "AppGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AppGateway.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppGateway.dll"]
