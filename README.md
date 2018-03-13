# DiffAPI


[![Build Status](https://travis-ci.org/lcantelli/DiffAPI.svg?branch=master)](https://travis-ci.org/lcantelli/DiffAPI)

<img src="https://docs.microsoft.com/pt-br/dotnet/images/hub/net.svg" width="48"> <img src="https://cdn.worldvectorlogo.com/logos/cucumber.svg" width="40"> <img src="https://upload.wikimedia.org/wikipedia/commons/7/73/Ruby_logo.svg" width="40">

## Getting Started

DiffAPI is a .NET Core rest API capable of compare base64 encoded JSONs and present differences between both objects.

This project has:
- swagger
- unit tests
- integrated tests
- acceptance tests using cucumber + ruby (reference: https://github.com/lcantelli/cucumber_machine)
- continuous integration build (http://travis-ci.org)

Feel free to copy and improve this project.

### Prerequisites

- .NET Core SDK 2.1.3
- docker (for SQL Server container and Acceptance Tests)

### Intalling and Running

```bash
    #Clone Git Repository
    git clone git@github.com:lcantelli/DiffAPI.git

    #Access Project Root Folder
    cd DiffAPI

    #Start SQLServerExpress instance
    docker-compose up -d 
    
    #Access API folder
    cd DiffAPI

    #Build and Restore packages
    dotnet restore

    #Create/Update database schema
    dotnet ef database update

    #Run API
    dotnet run

    #Access through address:
    http://localhost:5000/swagger

```

[![asciicast](https://asciinema.org/a/HsdRjuaABlgUNLg3JTM43QvYj.png)](https://asciinema.org/a/HsdRjuaABlgUNLg3JTM43QvYj)

### Acceptance Tests with Cucumber + Ruby

Run the API using the commands described above, then:

```bash
    #Access project root folder
    docker build -t cucumbermachine . && docker run --net=host -it cucumbermachine cucumber features
```

[![asciicast](https://asciinema.org/a/cTNwvSO4Evf0Mf16bYV8fcSgA.png)](https://asciinema.org/a/cTNwvSO4Evf0Mf16bYV8fcSgA)

### Code Coverage

Minimum required for the service classes is 80%. Check below:

<img src="https://i.imgur.com/BgJlug2.png" width="500">

### Usage

Three endpoints are available:

- /v1/diff/{id}/right
- /v1/diff/{id}/left
- /v1/diff/{id}

To encode and decode JSON: https://www.browserling.com/tools/json-to-base64

1. Access http://localhost:5000/swagger
2. Encode a JSON to base64 format
    
    (for {"Name": "Lucas"}, use: eyJOYW1lIjogIkx1Y2FzIn0=)

    (for {"Name": "Robert"}, use: eyJOYW1lIjogIlJvYmVydCJ9)

3. Use the "right" endpoint, saving the first encoded string using ID 1
4. Use the "left" endpoint, saving the second encoded string using ID 1
5. Use the "diff" endpoint and ID 1. The result should be:
    ```
    {
        "id": "1",
        "message": "Found 1 inconsistencies between jsons",
        "inconsistencies": [
          "Property 'Name' changed! From: Lucas - To: Robert"
        ]
    }
    ```

For more examples, check /features/Diff.feature

### Authors

- Lucas Cantelli

### License

- [MIT LICENSE](LICENSE.md)
