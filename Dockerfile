# --- Build stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution và restore
COPY *.sln .
COPY PE_PRN231_SE150627_API/*.csproj PE_PRN231_SE150627_API/
COPY DataAccess/*.csproj DataAccess/
COPY Repository/*.csproj Repository/

RUN dotnet restore

# Copy toàn bộ source vào container
COPY . .

# Build project API
WORKDIR /src/PE_PRN231_SE150627_API
RUN dotnet publish -c Release -o /app/publish

# --- Runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "PE_PRN231_SE150627_API.dll"]
