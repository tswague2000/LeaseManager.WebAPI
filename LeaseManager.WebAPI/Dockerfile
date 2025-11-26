# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project files
COPY LeaseManager.WebAPI/LeaseManager.WebAPI.csproj ./LeaseManager.WebAPI/
COPY LeaseManager.Core.Domain/LeaseManager.Core.Domain.csproj ./LeaseManager.Core.Domain/
COPY LeaseManager.Infrastucture/LeaseManager.Infrastucture.csproj ./LeaseManager.Infrastucture/

# Restore dependencies
RUN dotnet restore LeaseManager.WebAPI/LeaseManager.WebAPI.csproj

# Copy source code
COPY LeaseManager.WebAPI/ ./LeaseManager.WebAPI/
COPY LeaseManager.Core.Domain/ ./LeaseManager.Core.Domain/
COPY LeaseManager.Infrastucture/ ./LeaseManager.Infrastucture/

# Build
RUN dotnet build -c Release -o /build

# Publish
RUN dotnet publish LeaseManager.WebAPI/LeaseManager.WebAPI.csproj -c Release -o /publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published files from build stage
COPY --from=build /publish .

# Expose port
EXPOSE 5000

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:5000/api/health || exit 1

# Set environment
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:5000

# Run application
ENTRYPOINT ["dotnet", "LeaseManager.WebAPI.dll"]