FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# COPY ["origem", "destino/"]
COPY ["src/B2BDashboard.Api/B2BDashboard.Api.csproj", "src/B2BDashboard.Api/"]
COPY ["src/B2BDashboard.Application/B2BDashboard.Application.csproj", "src/B2BDashboard.Application/"]
COPY ["src/B2BDashboard.Infrastructure/B2BDashboard.Infrastructure.csproj", "src/B2BDashboard.Infrastructure/"]
COPY ["src/B2BDashboard.Domain/B2BDashboard.Domain.csproj", "src/B2BDashboard.Domain/"]

RUN dotnet restore "src/B2BDashboard.Api/B2BDashboard.Api.csproj"

COPY src/ /src/

WORKDIR /src/src/B2BDashboard.Api
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT [ "dotnet", "B2BDashboard.Api.dll" ]