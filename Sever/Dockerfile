# base SDk images docker
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /src

#Copy project file and restore 

COPY *.csproj ./
RUN dotnet restore

# Copy the project files and builds our release

COPY ./ ./

RUN dotnet publish -c release -o /out 

# Generate run time image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build-env /src/out ./
ENTRYPOINT ["dotnet", "Sever.dll"]


