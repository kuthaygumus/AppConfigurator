FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 55855
EXPOSE 44399

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY AppConfigurator/AppConfigurator.csproj AppConfigurator/
RUN dotnet restore AppConfigurator/AppConfigurator.csproj
COPY . .
WORKDIR /src/AppConfigurator
RUN dotnet build AppConfigurator.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish AppConfigurator.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AppConfigurator.dll"]
