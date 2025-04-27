# Base image with .NET 8 SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

# Install additional dependencies (like git, curl, or node if needed)
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        curl \
        git \
        unzip \
        libicu-dev \
    && apt-get clean \
    && dotnet dev-certs https --trust \
    && rm -rf /var/lib/apt/lists/*

# Install the .NET Core Debugger (vsdbg)
RUN curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg

# Create and set working directory
WORKDIR /app

# Set up environment variables
ENV DOTNET_USE_POLLING_FILE_WATCHER=true \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true \
    DOTNET_NOLOGO=true \
    ASPNETCORE_URLS=http://+:5000

EXPOSE 5000

# Optional: Copy project files (in case you want to pre-restore in the image build)
COPY ["RabbitFood.Shared/RabbitFood.Shared.csproj", "RabbitFood.Shared/"]
RUN dotnet restore "RabbitFood.Shared/RabbitFood.Shared.csproj"

COPY ["RabbitFood.Consumer/RabbitFood.Consumer.csproj", "RabbitFood.Consumer/"]
RUN dotnet restore "RabbitFood.Consumer/RabbitFood.Consumer.csproj"

# Start shell as default
CMD [ "bash" ]
