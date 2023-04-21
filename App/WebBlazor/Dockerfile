FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5002

ENV ASPNETCORE_URLS=http://+:5002

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["App/WebBlazor/WebBlazor.csproj", "App/WebBlazor/"]
RUN dotnet restore "App/WebBlazor/WebBlazor.csproj" --disable-parallel
COPY . .
WORKDIR "/src/App/WebBlazor"
RUN dotnet build "WebBlazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebBlazor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebBlazor.dll"]
