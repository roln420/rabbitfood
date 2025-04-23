FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5077

ENV ASPNETCORE_URLS=http://+:5077

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["RabbitFood.API/RabbitFood.API.csproj", "RabbitFood.API/"]
RUN dotnet restore "RabbitFood.API/RabbitFood.API.csproj"
COPY . .
WORKDIR "/src/RabbitFood.API"
RUN dotnet build "RabbitFood.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "RabbitFood.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RabbitFood.API.dll"]
