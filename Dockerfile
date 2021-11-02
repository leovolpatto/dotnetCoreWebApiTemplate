FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Eccosys.Workflow.Api/Eccosys.Workflow.Api.csproj", "Eccosys.Workflow.Api/"]
RUN dotnet restore "Eccosys.Workflow.Api/Eccosys.Workflow.Api.csproj"
COPY . .
WORKDIR "/src/Eccosys.Workflow.Api"
RUN dotnet build "Eccosys.Workflow.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Eccosys.Workflow.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eccosys.Workflow.Api.dll"]