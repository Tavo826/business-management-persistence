# -------- Build stage --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj para aprovechar cache
COPY ["BusinessPersistance/BusinessPersistance/BusinessPersistance.csproj", "BusinessPersistance/"]
COPY ["BusinessPersistance/Persistence/Persistence.csproj", "Persistence/"]
COPY ["BusinessPersistance/Domain/Domain.csproj", "Domain/"]
COPY ["BusinessPersistance/Application/Application.csproj", "Application/"]

RUN dotnet restore "BusinessPersistance/BusinessPersistance/BusinessPersistance.csproj"

# Copiar el resto del código
COPY . .

# Publicar la app
RUN dotnet publish "BusinessPersistance/BusinessPersistance/BusinessPersistance.csproj" -c Release -o /app/publish /p:UseAppHost=false

# -------- Runtime stage --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_EnableDiagnostics=0

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BusinessPersistance.dll"]
