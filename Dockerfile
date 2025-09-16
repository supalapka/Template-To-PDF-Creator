# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy solution and project files
COPY *.sln ./
COPY TemplateToPdfCreator/*.csproj ./TemplateToPdfCreator/
RUN dotnet restore ./TemplateToPdfCreator/TemplateToPdfCreator.csproj

# Copy all source files and publish
COPY TemplateToPdfCreator/. ./TemplateToPdfCreator/
WORKDIR /source/TemplateToPdfCreator
RUN dotnet publish -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "TemplateToPdfCreator.dll"]
