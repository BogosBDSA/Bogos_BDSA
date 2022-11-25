# Welcome to bogos

fluentassertions and libgit2sharp have to be added
libgit is needed in both test and program, fluentassertions is only needed in tests

## Import them using

important to do in the correct folders

``` rust
dotnet add package LibGit2Sharp --version 0.27.0-preview-0182

dotnet add package FluentAssertions --version 6.8.0
```

### This is the testing repository

<https://github.com/SDeLaVida/testing-for-bdsa>

---

### Docker commands

#### Persitent database

To make your instance of postgres save data somewhere you need to make a volume that the docker container is bound to
This will make a volume visable on your docker desktop application. Should you run a new container bound to the same volume with the -v command it will then get all previously written data. 

``` rust
docker run -d -v bogosmemory:/var/lib/postgresql/data -e POSTGRES_DB=bogosdb -e POSTGRES_PASSWORD=mypassword --name bogosDB -p 5430:5432 postgres
```

#### Volatile database

This is run the database without binding, meaning that killing the container will delete the data. Use this for testing new database features only

``` rust
docker run -d -e POSTGRES_DB=bogosdb -e POSTGRES_PASSWORD=mypassword --name bogosDB -p 5430:5432 postgres``
```

### testing user tokens for access

Set up access token.

``` rust
dotnet user-jwts create
```

example of call, remember to use a tool like curl to access, this is with an example token

``` rust
curl -i -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im5pY29sIiwic3ViIjoibmljb2wiLCJqdGkiOiIyZTYwZWNlYSIsImF1ZCI6WyJodHRwOi8vbG9jYWxob3N0OjIzMDUzIiwiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNzYiLCJodHRwOi8vbG9jYWxob3N0OjUyNDMiLCJodHRwczovL2xvY2FsaG9zdDo3MDI0Il0sIm5iZiI6MTY2OTM3NjA4MCwiZXhwIjoxNjc3MzI0ODgwLCJpYXQiOjE2NjkzNzYwODMsImlzcyI6ImRvdG5ldC11c2VyLWp3dHMifQ.LkEjAWeeVTOSK5cy5wNR3oy3ET4zOmLBlnCUy8YHcN0" http://localhost:5243/frequency/SDeLaVida/assignment-01
```
