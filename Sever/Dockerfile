# base SDk images docker
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /app

#Copy project file and restore 

COPY *.csproj ./
RUN dotnet restore

# Copy the project files and builds our release

COPY . ./
RUN dotnet publish -c Release -o out

# Generate run time image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 80 
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Server.dll"]


