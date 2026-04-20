# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

# Copy project files
COPY ["dotNet-projektni/dotNet-projektni.csproj", "dotNet-projektni/"]

# Restore dependencies
RUN dotnet restore "dotNet-projektni/dotNet-projektni.csproj"

# Copy source code
COPY . .

# Build the application
RUN dotnet build "dotNet-projektni/dotNet-projektni.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "dotNet-projektni/dotNet-projektni.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime (optimized for size)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app

# Install curl for healthcheck
RUN apk add --no-cache curl

# Copy published application from build stage
COPY --from=publish /app/publish .

# Expose port
EXPOSE 5109

# Healthcheck
HEALTHCHECK --interval=30s --timeout=3s --start-period=10s --retries=3 \
    CMD curl -f http://localhost:5109/health || exit 1

# Run application
ENTRYPOINT ["dotnet", "dotNet-projektni.dll"]

