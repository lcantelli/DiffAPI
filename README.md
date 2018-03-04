# DiffAPI


[![Build Status](https://travis-ci.org/lcantelli/DiffAPI.svg?branch=master)](https://travis-ci.org/lcantelli/DiffAPI)


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
    
    #Access API folder -->
    cd DiffAPI

    #Create/Update database schema-->
    dotnet ef database update

    #Run API
    dotnet run

    #Access through address:
    http://localhost:5000/swagger
```
