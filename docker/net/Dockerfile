FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY ./ ./

RUN dotnet build -c Release

FROM build AS publish
RUN dotnet publish -c Release -o out

FROM base AS final
WORKDIR /app
COPY --from=publish /app/out .

ENTRYPOINT ["dotnet", "DeviceHub.API.dll"]