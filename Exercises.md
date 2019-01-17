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
mkdir c:\Data1
echo Container1 > c:\Data1\Data.txt
docker run --name Example1 -d -p 8123:80 -v C:\Data1:/Data  davidbetteridgeproactis/example 

Browse to http://localhost:8123

## Run a second instance
mkdir c:\Data2
echo Container2 > c:\Data2\Data.txt
docker run --name Example2 -d -p 8124:80 -v C:\Data2:/Data  davidbetteridgeproactis/example 

Browse to http://localhost:8124


## docker-compose 
```
docker-compose up 
```

```
docker-compose -d up
docker-compose down
```


## Make your own
dotnet publish
docker build --tag davidbetteridgeproactis/example:Live  .
docker push davidbetteridgeproactis/example:latest