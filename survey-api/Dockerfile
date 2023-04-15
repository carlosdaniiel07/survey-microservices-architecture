FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY ["Survey.Microservices.Architecture.Api/Survey.Microservices.Architecture.Api.csproj", "Survey.Microservices.Architecture.Api/"]
COPY ["Survey.Microservices.Architecture.Application/Survey.Microservices.Architecture.Application.csproj", "Survey.Microservices.Architecture.Application/"]
COPY ["Survey.Microservices.Architecture.Domain/Survey.Microservices.Architecture.Domain.csproj", "Survey.Microservices.Architecture.Domain/"]
COPY ["Survey.Microservices.Architecture.Infrastructure.Data/Survey.Microservices.Architecture.Infrastructure.Data.csproj", "Survey.Microservices.Architecture.Infrastructure.Data/"]
COPY ["Survey.Microservices.Architecture.Infrastructure.IoC/Survey.Microservices.Architecture.Infrastructure.IoC.csproj", "Survey.Microservices.Architecture.Infrastructure.IoC/"]
COPY ["Survey.Microservices.Architecture.Infrastructure.Service/Survey.Microservices.Architecture.Infrastructure.Service.csproj", "Survey.Microservices.Architecture.Infrastructure.Service/"]

RUN dotnet restore "Survey.Microservices.Architecture.Api/Survey.Microservices.Architecture.Api.csproj"

COPY . .

WORKDIR "/src/Survey.Microservices.Architecture.Api"

RUN dotnet build "Survey.Microservices.Architecture.Api.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "Survey.Microservices.Architecture.Api.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Survey.Microservices.Architecture.Api.dll"]