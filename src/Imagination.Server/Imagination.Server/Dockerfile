#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS http://*:5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Imagination.Server/Imagination.Server/Imagination.Server.csproj", "src/Imagination.Server/Imagination.Server/"]
RUN dotnet restore "src/Imagination.Server/Imagination.Server/Imagination.Server.csproj"
COPY . .
WORKDIR "/src/src/Imagination.Server/Imagination.Server"
RUN dotnet build "Imagination.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Imagination.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Imagination.Server.dll"]

# install System.Drawing native dependencies
RUN apt-get update \
    && apt-get install -y --allow-unauthenticated \
        libc6-dev \
        libgdiplus \
        libx11-dev \
     && rm -rf /var/lib/apt/lists/*

