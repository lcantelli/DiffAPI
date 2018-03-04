# DiffAPI


[![Build Status](https://travis-ci.org/lcantelli/DiffAPI.svg?branch=master)](https://travis-ci.org/lcantelli/DiffAPI)

<img src="https://docs.microsoft.com/pt-br/dotnet/images/hub/net.svg" width="48"> <img src="https://cdn.worldvectorlogo.com/logos/cucumber.svg" width="40"> <img src="https://upload.wikimedia.org/wikipedia/commons/7/73/Ruby_logo.svg" width="40">

## Getting Started

### Prerequisites

### Running

```bash
    #Clone Git Repository
    git clone git@github.com:lcantelli/DiffAPI.git

    #Access Project Root Folder
    cd DiffAPI

    #Start SQLServerExpress instance
    docker-compose up -d 
    
    #Access API folder
    cd DiffAPI

    #Create/Update database schema
    dotnet ef database update

    #Run API
    dotnet run

    #Access through address:
    http://localhost:5000/swagger

```

### Acceptance Tests with Cucumber + Ruby

Run the API using the commands described above, then:

```bash
    #Access project root folder
    docker build -t cucumbermachine . && docker run --net=host -it cucumbermachine cucumber features
```