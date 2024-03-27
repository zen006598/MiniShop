FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY *.sln .
COPY src/minishop/*.csproj ./src/minishop/
COPY test/minishop.test/*.csproj ./test/minishop.test/
RUN dotnet restore

COPY . .
RUN dotnet build

FROM build AS testrunner
WORKDIR /app/test/minishop.test
CMD ["dotnet", "test", "--logger:trx"]

FROM build AS test
WORKDIR /app/test/minishop.test
RUN dotnet test --logger:trx

FROM build AS publish
WORKDIR /app/src/minishop
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=publish /app/src/minishop/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "minishop.dll"]
