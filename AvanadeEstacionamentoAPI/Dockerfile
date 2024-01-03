#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AvanadeEstacionamentoAPI/AvanadeEstacionamento.API.csproj", "AvanadeEstacionamentoAPI/"]
COPY ["AvanadeEstacionamento.Data/AvanadeEstacionamento.Data.csproj", "AvanadeEstacionamento.Data/"]
COPY ["AvanadeEstacionamento.Domain/AvanadeEstacionamento.Domain.csproj", "AvanadeEstacionamento.Domain/"]
RUN dotnet restore "AvanadeEstacionamentoAPI/AvanadeEstacionamento.API.csproj"
COPY . .
WORKDIR "/src/AvanadeEstacionamentoAPI"
RUN dotnet build "AvanadeEstacionamento.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AvanadeEstacionamento.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AvanadeEstacionamento.API.dll"]