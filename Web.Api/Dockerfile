# Get base SDK image from Microsoft
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy the CSPROJ file and restore the dependencies (NuGet)
COPY ./Web.Api/*.csproj ./
RUN dotnet restore

# Copy the project files and build our release
COPY ./Web.Api ./
RUN dotnet publish -c Release -o out

# Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "Web.Api.dll" ]