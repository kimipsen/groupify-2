FROM mcr.microsoft.com/devcontainers/dotnet:dev-10.0-preview

WORKDIR /workspaces

COPY .config/dotnet-tools.json ./.config/dotnet-tools.json
RUN dotnet tool restore

COPY . .
RUN dotnet restore