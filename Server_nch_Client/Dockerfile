FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Server_nch_Client/Server_nch_Client.csproj", "Server_nch_Client/"]
RUN dotnet restore "Server_nch_Client/Server_nch_Client.csproj"
COPY . .
WORKDIR "/src/Server_nch_Client"
RUN dotnet build "Server_nch_Client.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Server_nch_Client.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Server_nch_Client.dll"]
