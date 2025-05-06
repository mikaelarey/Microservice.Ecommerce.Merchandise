# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution and all project files
COPY *.sln ./
COPY Merchandise.Application/*.csproj Merchandise.Application/
COPY Merchandise.Domain/*.csproj Merchandise.Domain/
COPY Merchandise.Infrastructure/*.csproj Merchandise.Infrastructure/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the code
COPY . ./

# Publish the web app
RUN dotnet publish Merchandise.Application/Merchandise.Application.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Merchandise.Application.dll"]
