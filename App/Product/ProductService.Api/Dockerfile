FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5003
EXPOSE 7003

ENV ASPNETCORE_URLS=http://+:5003

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["App/Product/ProductService.Api/ProductService.Api.csproj", "App/Product/ProductService.Api/"]
RUN dotnet restore "App/Product/ProductService.Api/ProductService.Api.csproj" --disable-parallel
COPY . .
WORKDIR "/src/App/Product/ProductService.Api"
RUN dotnet build "ProductService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductService.Api.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.Api.dll"]
