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
COPY ["RabbitFood.API/RabbitFood.API.csproj", "RabbitFood.API/"]
RUN dotnet restore "RabbitFood.API/RabbitFood.API.csproj"

# Start shell as default
CMD [ "bash" ]
