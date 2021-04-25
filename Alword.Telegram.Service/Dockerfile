#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Alword.Telegram.Service/Alword.Telegram.Service.csproj", "Alword.Telegram.Service/"]
COPY ["Alword.Telegram.Logic/Alword.Telegram.Logic.csproj", "Alword.Telegram.Logic/"]
RUN dotnet restore "Alword.Telegram.Service/Alword.Telegram.Service.csproj"
COPY . .
WORKDIR "/src/Alword.Telegram.Service"
RUN dotnet build "Alword.Telegram.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Alword.Telegram.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Alword.Telegram.Service.dll"]