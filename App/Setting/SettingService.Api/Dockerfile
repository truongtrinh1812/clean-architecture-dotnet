FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 7005
EXPOSE 5005

ENV ASPNETCORE_URLS=http://+:5005

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["App/Setting/SettingService.Api/SettingService.Api.csproj", "App/Setting/SettingService.Api/"]
RUN dotnet restore "App/Setting/SettingService.Api/SettingService.Api.csproj" --disable-parallel
COPY . .
WORKDIR "/src/App/Setting/SettingService.Api"
RUN dotnet build "SettingService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SettingService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SettingService.Api.dll"]
