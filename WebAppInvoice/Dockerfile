FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebAppInvoice/WebAppInvoice.csproj", "WebAppInvoice/"]
RUN dotnet restore "WebAppInvoice/WebAppInvoice.csproj"
COPY . .
WORKDIR "/src/WebAppInvoice"
RUN dotnet build "WebAppInvoice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAppInvoice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAppInvoice.dll"]
