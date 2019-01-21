## Before you start
* Make sure that the docker service is running
* And Docker for the desktop is configured to run Linux containers

## Use an existing image
```
docker images
docker pull davidbetteridgeproactis/example
docker images

docker ps -a
docker run -d -p 8122:80 davidbetteridgeproactis/example 
docker ps -a
```

Browse to http://localhost:8122

```
docker stop .....
docker rm ....
docker ps -a
```


## Map a folder
```powershell
mkdir c:\Data1
echo Container1 > c:\Data1\Data.txt
docker run --name Example1 -d -p 8123:80 -v C:\Data1:/Data  davidbetteridgeproactis/example 
```
Browse to http://localhost:8123

## Run a second instance
```powershell
mkdir c:\Data2
echo Container2 > c:\Data2\Data.txt
docker run --name Example2 -d -p 8124:80 -v C:\Data2:/Data  davidbetteridgeproactis/example 
```
Browse to http://localhost:8124


## docker-compose 
```powershell
docker-compose up 
```
or
```powershell
docker-compose -d up
docker-compose down
```

Browse to http://localhost:9001
Browse to http://localhost:9002
Browse to http://localhost:9002/Data
Browse to http://localhost:9002/Data/machineName=Service1


## 3rd Party Software
```powershell
docker pull sebp/elk
docker run –p 5601:5601 –p 9200:9200 –p 5044:4044 –it -–name elk sebp/elk
```

## Make your own
Create a new dotnet core website,  without docker support.
Run it,  the home page should be displayed.  Change it to make it your own

Create a docker file
```
FROM microsoft/dotnet:2.1-aspnetcore-runtime
ARG source
WORKDIR /app
EXPOSE 80
LABEL com.proactis.product="training_application"
COPY ${source:-release/netcoreapp2.1/publish} .
ENTRYPOINT ["dotnet", "ExampleWebSite.dll"]
```

```
dotnet publish
docker build --tag davidbetteridgeproactis/example:Live  .
docker login
docker push davidbetteridgeproactis/example:latest
```
Try pulling and running someone elses site.