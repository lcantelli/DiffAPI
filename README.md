# DiffAPI


[![Build Status](https://travis-ci.org/lcantelli/DiffAPI.svg?branch=master)](https://travis-ci.org/lcantelli/DiffAPI)


## Getting Started

### Prerequisites

### Running

```bash
    <!-- Access project folder -->
    <!-- Start SQLServerExpress instance -->
    docker-compose up -d 
    
    <!-- Access API folder -->
    cd DiffAPI

    <!-- Create/Update database schema-->
    dotnet ef database update

    <!-- Run API -->
    dotnet run

    <!-- Access through address: localhost:5000/swagger -->
```
