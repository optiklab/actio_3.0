# actio_3.0
Migrating Actio sample project from Udemy to .NET Core 3.0


Anton Yarkov: 
https://github.com/optiklab/actio is the project created by guidelines from Udemy course about .NET microservices and CQRS pattern.
https://github.com/optiklab/actio_3.0 is the same project, but migrated to .NET 3.0.

Useful links
------------
https://rawrabbit.readthedocs.io/en/master/
https://rawrabbit.readthedocs.io/en/master/configuration.html


How to test alive
-----------------

1. Run mongo db instance:
```bash
$>docker run -d -p 27017:27017 mongo
```

2. Go to Api project and run on HTTP port 5000:
```bash
$>dotnet run
```

3. Go to Activities project and run on HTTP port 5005:
```bash
$>dotnet run --urls "http://+:5005"
```

Result: Check that default Categories collection is created in MongoDb

4. Go to Identity project and run on HTTP port 5050:
```bash
$>dotnet run --urls "http://+:5050"
```

5. Run Postman and execute to create an Activity inside of existing Category:
```bash
POST http://localhost:5000/activities HTTP/1.1
Host: localhost:5000
Content-Type: application/json
cache-control: no-cache
body:
{
    "category": "hobby",
    "name": "blah blah1"
}
```
Result: Check that Activity is created in MongoDb

6.  Run Postman and execute to create first user:
```bash
POST http://localhost:5000/users/register HTTP/1.1
Host: localhost:5000
Content-Type: application/json
cache-control: no-cache
body:
{
	"email":"user1@email.com",
	"name":"anton",
	"password":"test1234"
}

Result: Check that User is created in MongoDb
```

7.  Run Postman and execute:
```bash
GET http://localhost:5000/activities HTTP/1.1
Host: localhost:5000

Response: 401 unauthorized
```

8.  Run Postman and execute:
```bash
POST http://localhost:5050/login HTTP/1.1
Host: localhost:5050
Content-Type: application/json
cache-control: no-cache
body:
{
	"email":"user1@email.com",
	"password":"test1234"
}

Response:
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJiNmM3MDZhNi1jOGI5LTQ4NGItOTIzMC0zNjU2Y2UzM2JiOTMiLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUwNTAiLCJpYXQiOjE1NzIxMjk5MzcsImV4cCI6MTU3MjEzMDIzNywidW5pcXVlX25hbWUiOiJiNmM3MDZhNi1jOGI5LTQ4NGItOTIzMC0zNjU2Y2UzM2JiOTMifQ.lXQ5rrVyANYFmvIa8s6vJts165U2E7Q8sQtzfEUugjw",
    "expires": 1572130237
}
```
You can check content of token on https://jwt.io/
```bash
GET http://localhost:5000/activities HTTP/1.1
Host: localhost:5000
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJiNmM3MDZhNi1jOGI5LTQ4NGItOTIzMC0zNjU2Y2UzM2JiOTMiLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUwNTAiLCJpYXQiOjE1NzIxMjk5MzcsImV4cCI6MTU3MjEzMDIzNywidW5pcXVlX25hbWUiOiJiNmM3MDZhNi1jOGI5LTQ4NGItOTIzMC0zNjU2Y2UzM2JiOTMifQ.lXQ5rrVyANYFmvIa8s6vJts165U2E7Q8sQtzfEUugjw
cache-control: no-cache

Response:
HTTP 200 OK
Secured content!
```

How to debug
------------
1. Go to the .vscode in the root and edit launch.json to specify paths to the project to Debug
2. Run Start Debugging (don't forget to put breakpoints)

Hot to pack into docker file
----------------------------
1. Create Dockerfile in project directory (separate for each microservice)
2. Create appsettings.docker.json with changed parameters of URLs
3. Pack your app into docker:
```bash
$> \Actio\src\Actio.Api>dotnet publish -c Release -o ./bin/Docker
```

4. Build your docker:
```bash
$> docker build -t actio.api .
```

5. Build your docker:
```bash
$> docker run -p 5000:5000 actio.api
```
