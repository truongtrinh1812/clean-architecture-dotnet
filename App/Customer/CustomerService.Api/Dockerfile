FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["App/Customer/CustomerService.Api/CustomerService.Api.csproj", "App/Customer/CustomerService.Api/"]
RUN dotnet restore "App/Customer/CustomerService.Api/CustomerService.Api.csproj" --disable-parallel
COPY . .
WORKDIR "/src/App/Customer/CustomerService.Api"
RUN dotnet build "CustomerService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerService.Api.dll"]
