FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TPlatform.TelegramService/TPlatform.TelegramService.Api.csproj", "TPlatform.TelegramService/"]
COPY ["TPlatform.TelegramService.Dto/TPlatform.TelegramService.Dto.csproj", "TPlatform.TelegramService.Dto/"]
COPY ["TPlatform.TelegramService.Services/TPlatform.TelegramService.Services.csproj", "TPlatform.TelegramService.Services/"]
RUN dotnet restore "TPlatform.TelegramService/TPlatform.TelegramService.Api.csproj"
COPY . .
WORKDIR "/src/TPlatform.TelegramService"
RUN dotnet build "TPlatform.TelegramService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TPlatform.TelegramService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TPlatform.TelegramService.Api.dll"]
