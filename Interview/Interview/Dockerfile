FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Interview/Interview.csproj", "Interview/"]
RUN dotnet restore "Interview/Interview.csproj"
COPY . .
WORKDIR "/src/Interview"
RUN dotnet build "Interview.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Interview.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Interview.dll"]
