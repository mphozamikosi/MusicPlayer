# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./MusicPlayerAPI/MusicPlayerAPI.csproj" --disable-parallel
RUN dotnet publish "./MusicPlayerAPI/MusicPlayerAPI.csproj" -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS build
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "MusicPlayerAPI.dll"]