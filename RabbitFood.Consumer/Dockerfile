FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5077

ENV ASPNETCORE_URLS=http://+:5077

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["RabbitFood.Consumer/RabbitFood.Consumer.csproj", "RabbitFood.Consumer/"]
RUN dotnet restore "RabbitFood.Consumer/RabbitFood.Consumer.csproj"
COPY . .
WORKDIR "/src/RabbitFood.Consumer"
RUN dotnet build "RabbitFood.Consumer.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "RabbitFood.Consumer.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RabbitFood.Consumer.dll"]
