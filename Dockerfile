
# base 
# create runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# stage /app
WORKDIR /app 
# exposr app
EXPOSE 8080
EXPOSE 8081

# build
# sdk to create build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# stage /src
WORKDIR /src
COPY . .
WORKDIR /src/sfmsBackEnd2
RUN dotnet restore "sfmsBackEnd2.csproj"
RUN dotnet build "sfmsBackEnd2.csproj" -c Release -o /app/build

# publish
FROM build AS publish
RUN dotnet publish "sfmsBackEnd2.csproj" -c Release -o /app/publish

# stage chanhes will loose its path
# final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet","sfmsBackEnd2.dll" ]