FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5002

ENV ASPNETCORE_URLS=https://+:5002

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["App/WebBlazor/Server/WebBlazor.Server/WebBlazor.Server.csproj", "App/WebBlazor/Server/WebBlazor.Server/"]
RUN dotnet restore "App/WebBlazor/Server/WebBlazor.Server/WebBlazor.Server.csproj" --disable-parallel
COPY . .
WORKDIR "/src/App/WebBlazor/Server/WebBlazor.Server"
RUN dotnet build "WebBlazor.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebBlazor.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebBlazor.Server.dll"]
