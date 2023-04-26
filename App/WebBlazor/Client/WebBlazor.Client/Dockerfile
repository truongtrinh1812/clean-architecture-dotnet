FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 7002

ENV ASPNETCORE_URLS=https://+:7002

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["App/WebBlazor/Client/WebBlazor.Client/WebBlazor.Client.csproj", "App/WebBlazor/Client/WebBlazor.Client/"]
RUN dotnet restore "App/WebBlazor/Client/WebBlazor.Client/WebBlazor.Client.csproj" --disable-parallel
COPY . .
WORKDIR "/src/App/WebBlazor/Client/WebBlazor.Client"
RUN dotnet build "WebBlazor.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebBlazor.Client.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY App/WebBlazor/Client/nginx.conf /etc/nginx/nginx.conf
