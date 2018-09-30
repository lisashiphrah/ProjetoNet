FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS http://*:62263
EXPOSE 62263

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY src/Eventos.IO.Services.Api/Eventos.IO.Services.Api.csproj src/Eventos.IO.Services.Api/
COPY src/Evento.IO.Domain.Core/Eventos.IO.Domain.Core.csproj src/Evento.IO.Domain.Core/
COPY src/Eventos.IO.Infra.CrossCutting.AspNetFilters/Eventos.IO.Infra.CrossCutting.AspNetFilters.csproj src/Eventos.IO.Infra.CrossCutting.AspNetFilters/
COPY src/Eventos.IO.Infra.CrossCutting.IoC/Eventos.IO.Infra.CrossCutting.IoC.csproj src/Eventos.IO.Infra.CrossCutting.IoC/
COPY src/Eventos.IO.Infra.Data/Eventos.IO.Infra.Data.csproj src/Eventos.IO.Infra.Data/
COPY src/Evento.IO.Domain/Eventos.IO.Domain.csproj src/Evento.IO.Domain/
COPY src/Eventos.IO.Infra.CrossCutting.Bus/Eventos.IO.Infra.CrossCutting.Bus.csproj src/Eventos.IO.Infra.CrossCutting.Bus/
COPY src/Eventos.IO.Infra.CrossCutting.Identity/Eventos.IO.Infra.CrossCutting.Identity.csproj src/Eventos.IO.Infra.CrossCutting.Identity/
COPY src/Eventos.IO.Application/Eventos.IO.Application.csproj src/Eventos.IO.Application/
RUN dotnet restore src/Eventos.IO.Services.Api/Eventos.IO.Services.Api.csproj
COPY . .
WORKDIR /src/src/Eventos.IO.Services.Api
RUN dotnet build Eventos.IO.Services.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Eventos.IO.Services.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Eventos.IO.Services.Api.dll"]
