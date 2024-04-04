FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TagManage.API/TagManage.API.csproj", "TagManage.API/"]
COPY ["TagManage.Data/TagManage.Data.csproj", "TagManage.Data/"]
COPY ["TagManage.Domain/TagManage.Domain.csproj", "TagManage.Domain/"]

RUN dotnet restore "./TagManage.API/TagManage.API.csproj"
COPY . .

WORKDIR "/src/TagManage.API"
RUN dotnet build "./TagManage.API.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "./TagManage.API.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TagManage.API.dll"]