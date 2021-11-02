FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Eccosys.Workflow.Api/Workflow.csproj", "Workflow/"]
RUN dotnet restore "Eccosys.Workflow.Api/Workflow.csproj"
COPY . .
WORKDIR "/src/Workflow"
RUN dotnet build "Workflow.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Workflow.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Workflow.dll"]