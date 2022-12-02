# Welcome to bogos

Fluentassertions and libgit2sharp have to be added.
Libgit is needed in both test and program, fluentassertions is only needed in tests.

## Import them using

Important to do in the correct folders.

``dotnet add package LibGit2Sharp --version 0.27.0-preview-0182``

``dotnet add package FluentAssertions --version 6.8.0``

---

### Docker commands

#### Persitent database

To make your instance of postgres save data somewhere you need to make a volume that the docker container is bound to
This will make a volume visable on your docker desktop application. Should you run a new container bound to the same volume with the -v command it will then get all previously written data.

``docker run -d -v bogosmemory:/var/lib/postgresql/data -e POSTGRES_DB=bogosdb -e POSTGRES_PASSWORD=mypassword --name bogosDB -p 5430:5432 postgres``

#### Volatile database

This is run the database without binding, meaning that killing the container will delete the data. Use this for testing new database features only

``docker run -d -e POSTGRES_DB=bogosdb -e POSTGRES_PASSWORD=mypassword --name bogosDB -p 5430:5432 postgres``
