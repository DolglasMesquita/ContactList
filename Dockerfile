FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ContactList/ContactList.csproj", "ContactList/"]
RUN dotnet restore "ContactList/ContactList.csproj"
COPY . .
WORKDIR "/src/ContactList"
RUN dotnet build "ContactList.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactList.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet ContactList.dll