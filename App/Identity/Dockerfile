FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001

ENV ASPNETCORE_URLS=https://+:5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["App/Identity/Identity.csproj", "App/Identity/"]
RUN dotnet restore "App/Identity/Identity.csproj" --disable-parallel
COPY . .
WORKDIR "/src/App/Identity"
RUN dotnet build "Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.csproj" -c Release -o /app/publish 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.dll"]
