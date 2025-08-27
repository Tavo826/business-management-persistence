# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar primero los archivos de proyecto (.csproj) para aprovechar la cache
COPY ["BusinessPersistence/BusinessPersistence/BusinessPersistence.csproj", "BusinessPersistence/BusinessPersistence/"]
COPY ["BusinessPersistence/Application/Application.csproj", "BusinessPersistence/Application/"]
COPY ["BusinessPersistence/Domain/Domain.csproj", "BusinessPersistence/Domain/"]
COPY ["BusinessPersistence/Persistence/Persistence.csproj", "BusinessPersistence/Persistence/"]

# Restaurar dependencias
RUN dotnet restore "BusinessPersistence/BusinessPersistence/BusinessPersistence.csproj"

# Copiar todo el código
COPY . .

# Build
WORKDIR "/src/BusinessPersistence/BusinessPersistence"
RUN dotnet build "BusinessPersistence.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "BusinessPersistence.csproj" -c Release -o /app/publish

# Imagen final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_EnableDiagnostics=0

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BusinessPersistence.dll"]

