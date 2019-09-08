FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["ServerlessTODOList.Frontend/ServerlessTODOList.Frontend.csproj", "ServerlessTODOList.Frontend/"]
COPY ["ServerlessTODOList.Common/ServerlessTODOList.Common.csproj", "ServerlessTODOList.Common/"]
COPY ["ServerlessTODOList.DataAccess/ServerlessTODOList.DataAccess.csproj", "ServerlessTODOList.DataAccess/"]
RUN dotnet restore "ServerlessTODOList.Frontend/ServerlessTODOList.Frontend.csproj"
COPY . .
WORKDIR "/src/ServerlessTODOList.Frontend"
RUN dotnet build "ServerlessTODOList.Frontend.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ServerlessTODOList.Frontend.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ServerlessTODOList.Frontend.dll"]